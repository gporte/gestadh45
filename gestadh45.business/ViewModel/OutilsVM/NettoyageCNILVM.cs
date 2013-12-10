using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using gestadh45.business.PersonalizedMsg;
using gestadh45.model;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Windows.Input;

namespace gestadh45.business.ViewModel.OutilsVM
{
	public class NettoyageCNILVM : VMUCBase
	{

		#region OldAdherents
		private IOrderedEnumerable<Adherent> _oldAdherents;
		public IOrderedEnumerable<Adherent> OldAdherents {
			get { return this._oldAdherents; }
			set {
				if (this._oldAdherents != value) {
					this._oldAdherents = value;
					this.RaisePropertyChanged(() => this.OldAdherents);
				}
			}
		}
		#endregion				

		#region Constructor
		public NettoyageCNILVM() : base() {
			this.CreateGetOldInscriptionsCommand();
			this.CreateCleanDatasCommand();
		}
		#endregion

		#region GetOldAdherentsCommand
		public ICommand GetOldAdherentsCommand { get; set; }

		private void CreateGetOldInscriptionsCommand() {
			this.GetOldAdherentsCommand = new RelayCommand(
				this.ExecuteGetOldAdherentsCommand,
				this.CanExecuteGetOldAdherentsCommand
			);
		}

		public bool CanExecuteGetOldAdherentsCommand() {
			return true;
		}

		public void ExecuteGetOldAdherentsCommand() {
			// on récupére tous les adhérents qui n'ont pas d'inscription sur la saison courante
			this.OldAdherents = this.Context.Adherents.ToList()
				.Where(a => a.Inscriptions.Count(i => i.Groupe.Saison.EstSaisonCourante) == 0)
				.OrderBy(a => a.Nom)
				.ThenBy(a => a.Prenom);
		}
		#endregion

		#region CleanDatasCommand
		public ICommand CleanDatasCommand { get; set; }

		private void CreateCleanDatasCommand() {
			this.CleanDatasCommand = new RelayCommand(
				this.ExecuteCleanDatasCommand,
				this.CanExecuteCleanDatasCommand
			);
		}

		public bool CanExecuteCleanDatasCommand() {
			return this.OldAdherents != null && this.OldAdherents.Count() > 0;
		}

		public void ExecuteCleanDatasCommand() {
			Messenger.Default.Send(
				new NMAskConfirmationDialog<bool>(
					this.CleanDatas, 
					string.Format(CultureInfo.CurrentCulture, ResNettoyageCNIL.ConfirmationNettoyage, this.OldAdherents.Count()),
					ResCommon.TitreConfirmation
				)
			);
		}

		private void CleanDatas(bool doClean) {
			foreach (var adh in this.OldAdherents) {
				foreach (var ins in adh.Inscriptions) {
					this.Context.Entry(ins).State = EntityState.Deleted;
				}

				this.Context.Entry(adh).State = EntityState.Deleted;
			}
			
			// puis on supprime tous les groupes qui ne sont pas de la saison courante ET qui n'ont pas d'inscriptions
			var oldGroupes = this.Context.Groupes.Where(g => !g.Saison.EstSaisonCourante && g.Inscriptions.Count == 0);
			foreach (var groupe in oldGroupes) {
				this.Context.Entry(groupe).State = EntityState.Deleted;
			}
			
			this.Context.SaveChanges();
		}
		#endregion
	}
}
