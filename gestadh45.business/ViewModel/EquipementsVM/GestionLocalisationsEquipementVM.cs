using GalaSoft.MvvmLight.Command;
using gestadh45.business.Enums;
using gestadh45.model;
/*
 * Crée par SharpDevelop.
 * Utilisateur: gp
 * Date: 08/03/2013
 * Heure: 13:34
 * 
 * Pour changer ce modèle utiliser Outils | Options | Codage | Editer les en-têtes standards.
 */
using System.Collections;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Windows.Input;

namespace gestadh45.business.ViewModel.EquipementsVM
{
	/// <summary>
	/// Description of GestionLocalisationsEquipementVM.
	/// </summary>
	public class GestionLocalisationsEquipementVM : GenericGestionVM<Equipement>
	{
		#region Localisations
		private IOrderedEnumerable<Localisation> _localisations;

		/// <summary>
		/// Gets or sets the Localisations.
		/// </summary>
		/// <value>
		/// The Localisations.
		/// </value>
		public IOrderedEnumerable<Localisation> Localisations {
			get {
				return this._localisations;
			}

			set {
				if (this._localisations != value) {
					this._localisations = value;
					this.RaisePropertyChanged(() => this.Localisations);
				}
			}
		}
		#endregion

		#region SelectedLocalisation
		private Localisation _selectedLocalisation;

		/// <summary>
		/// Gets or sets the selected localisation.
		/// </summary>
		/// <value>
		/// The selected localisation.
		/// </value>
		public Localisation SelectedLocalisation {
			get {
				return this._selectedLocalisation;
			}

			set {
				if (this._selectedLocalisation != value) {
					this._selectedLocalisation = value;
					this.RaisePropertyChanged(() => this.SelectedLocalisation);
				}
			}
		}
		#endregion
		
		public GestionLocalisationsEquipementVM(string userConnectionString) : base(userConnectionString) {
			this.UCCode = CodesUC.GestionLocalisationsEquipements;
			this.CreateDeplacerEquipementsCommand();
		}
		
		#region DeplacerEquipementsCommand
		public ICommand DeplacerEquipementsCommand { get; set; }

		private void CreateDeplacerEquipementsCommand() {
			this.DeplacerEquipementsCommand = new RelayCommand<object>(
				this.ExecuteDeplacerEquipementsCommand,
				this.CanExecuteDeplacerEquipementsCommand
			);
		}

		public bool CanExecuteDeplacerEquipementsCommand(object arg) {
			return this.SelectedLocalisation != null;
		}

		public void ExecuteDeplacerEquipementsCommand(object selectedItems) {
			// on cast correctement le résultat
			var equipements = (selectedItems as IList).Cast<Equipement>();

			foreach (var equip in equipements) {
				equip.Localisation = this.SelectedLocalisation;
				this.Context.Entry(equip).State = EntityState.Modified;
			}

			this.Context.SaveChanges();

			this.ShowUserNotification(string.Format(CultureInfo.CurrentCulture, ResEquipements.InfoEquipementsDeplaces, equipements.Count(), this.SelectedLocalisation.Libelle));

			this.PopulateItems();
		}
		#endregion
		
		#region methods override
		protected override void PopulateItems()	{
			base.PopulateItems();
			
			// les équipements au rebut ne sont jamais affichés ici
			this.Items = this.Items.Where(x => !x.EstAuRebut).OrderBy(x => x.Modele.LibelleCourt);
		}
		
		protected override void PopulateSpecificDatas()	{
			this.Localisations = this.Context.Localisations.ToList().OrderBy(l => l.Libelle);
		}
		#endregion
	}
}
