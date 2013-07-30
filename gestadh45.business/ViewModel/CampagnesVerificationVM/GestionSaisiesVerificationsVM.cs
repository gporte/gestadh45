/*
 * Crée par SharpDevelop.
 * Utilisateur: gp
 * Date: 08/03/2013
 * Heure: 17:01
 * 
 * Pour changer ce modèle utiliser Outils | Options | Codage | Editer les en-têtes standards.
 */
using System;
using System.Collections;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Windows.Input;

using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using gestadh45.business.Enums;
using gestadh45.business.PersonalizedMsg;
using gestadh45.model;

namespace gestadh45.business.ViewModel.CampagnesVerificationVM
{
	/// <summary>
	/// Description of GestionSaisiesVerificationsVM.
	/// </summary>
	public class GestionSaisiesVerificationsVM : GenericGestionVM<Verification>
	{
		#region StatutsVerification
		private IOrderedEnumerable<StatutVerification> _statutsVerifications;

		/// <summary>
		/// Obtient/Définit la liste des statuts de vérification
		/// </summary>
		public IOrderedEnumerable<StatutVerification> StatutsVerification {
			get { return this._statutsVerifications; }
			set {
				if (this._statutsVerifications != value) {
					this._statutsVerifications = value;
					this.RaisePropertyChanged(() => this.StatutsVerification);
				}
			}
		}
		#endregion

		#region SelectedStatutVerification
		private StatutVerification _selectedStatutVerification;

		public StatutVerification SelectedStatutVerification {
			get { return this._selectedStatutVerification; }
			set {
				if (this._selectedStatutVerification != value) {
					this._selectedStatutVerification = value;
					this.RaisePropertyChanged(() => this.SelectedStatutVerification);
				}
			}
		}
		#endregion
		
		#region CurrentCampagne
		private CampagneVerification _currentCampagne;

		public CampagneVerification CurrentCampagne {
			get { return this._currentCampagne; }
			set {
				if (this._currentCampagne != value) {
					this._currentCampagne = value;
					this.RaisePropertyChanged(() => this.CurrentCampagne);
				}
			}
		}
		#endregion
		
		public GestionSaisiesVerificationsVM(string userConnectionString, Guid idCampagne) : base(userConnectionString) {
			this.UCCode = CodesUC.GestionSaisiesVerifications;
			this.CurrentCampagne = this.Context.CampagneVerifications.FirstOrDefault(x => x.ID == idCampagne);
			
			this.CreateChangerStatutVerificationsCommand();
			this.CreateValidateCommand();
		}
		
		#region ChangerStatutVerificationsCommand
		public ICommand ChangerStatutVerificationsCommand { get; set; }

		private void CreateChangerStatutVerificationsCommand() {
			this.ChangerStatutVerificationsCommand = new RelayCommand<object>(
				this.ExecuteChangerStatutVerificationsCommand,
				this.CanExecuteChangerStatutVerificationsCommand
			);
		}

		public bool CanExecuteChangerStatutVerificationsCommand(object arg) {
			return true;
		}

		public void ExecuteChangerStatutVerificationsCommand(object selectedItems) {
			// on cast correctement le résultat
			var verifications = (selectedItems as IList).Cast<Verification>();

			foreach (var verif in verifications) {
				verif.StatutVerification = this.SelectedStatutVerification;
				this.Context.Entry(verif).State = EntityState.Modified;
			}

			this.Context.SaveChanges();
			this.ShowUserNotification(string.Format(CultureInfo.CurrentCulture, ResCampagnesVerification.InfoChangementStatuts, verifications.Count(), this.SelectedStatutVerification.ToString()));
			this.PopulateItems();
		}
		#endregion

		#region ValidateCommand
		public ICommand ValidateCommand { get; set; }

		private void CreateValidateCommand() {
			this.ValidateCommand = new RelayCommand(
				this.ExecuteValidateCommand,
				this.CanExecuteValidateCommand
			);
		}

		public bool CanExecuteValidateCommand() {
			// On ne peut valider une campagne que si toutes ses vérifications ont été saisies (i.e. : aucune vérif n'a le statut par défaut)
			return this.CurrentCampagne.Verifications
				.Count(x => x.StatutVerification == StatutVerification.AVerifier) == 0;
			//return this.CurrentItem.Verifications.Count(v => v.StatutVerification == StatutVerification.AVerifier) == 0;
		}

		public void ExecuteValidateCommand() {
			Messenger.Default.Send(
				new NMAskConfirmationDialog<bool>(
					this.ExecuteValidateCommandCallBack, 
					ResCampagnesVerification.TexteConfirmationValidation,
					ResCommon.TitreConfirmation
				)
			);
		}

		private void ExecuteValidateCommandCallBack(bool validateConfirmation) {
			if (validateConfirmation) {
				this.CurrentCampagne.EstValidee = true;
				this.Context.Entry(this.CurrentCampagne).State = System.Data.EntityState.Modified;
				this.Context.SaveChanges();

				this.ShowUC(CodesUC.GestionCampagnesVerification);
			}
		}
		#endregion
		
		#region methods override
		protected override void PopulateItems() {
			base.PopulateItems();
			
			this.Items = this.Items
				.Where(x => x.CampagneVerification.ID == this.CurrentCampagne.ID)
				.OrderBy(x => x.Equipement.Modele.LibelleCourt)
				.ThenBy(x => x.Equipement.Numero);
		}
		
		protected override void PopulateSpecificDatas() {
			this.StatutsVerification = Enum.GetValues(typeof(StatutVerification)).Cast<StatutVerification>().OrderBy(s => s);
		}
		#endregion
	}
}
