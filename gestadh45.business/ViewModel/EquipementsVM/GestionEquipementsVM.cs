using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using gestadh45.business.Enums;
using gestadh45.business.PersonalizedMsg;
using gestadh45.business.ServicesAdapters;
using gestadh45.model;
using gestadh45.services.Reporting;
using gestadh45.services.Reporting.Templates;
using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Windows.Input;

namespace gestadh45.business.ViewModel.EquipementsVM
{
	/// <summary>
	/// Description of GestionEquipementsVM.
	/// </summary>
	public class GestionEquipementsVM : GenericGestionVM<Equipement>
	{
		#region Modeles
		private IOrderedEnumerable<Modele> _modeles;

		/// <summary>
		/// Obtient/Définit la liste des couleurs
		/// </summary>
		public IOrderedEnumerable<Modele> Modeles {
			get { return this._modeles; }
			set {
				if (this._modeles != value) {
					this._modeles = value;
					this.RaisePropertyChanged(() => this.Modeles);
				}
			}
		}
		#endregion				

		#region Localisations
		private IOrderedEnumerable<Localisation> _localisations;

		/// <summary>
		/// Obtient/Définit la liste des localisations
		/// </summary>
		public IOrderedEnumerable<Localisation> Localisations {
			get { return this._localisations; }
			set {
				if (this._localisations != value) {
					this._localisations = value;
					this.RaisePropertyChanged(() => this.Localisations);
				}
			}
		}
		#endregion				
		
		#region AfficherRebut
		private bool _afficherRebut;

		/// <summary>
		/// Get or sets the AfficherRebut flag
		/// </summary>
		public bool AfficherRebut {
			get { return this._afficherRebut; }
			set {
				if (this._afficherRebut != value) {
					this._afficherRebut = value;
					this.RaisePropertyChanged(() => this.AfficherRebut);
				}
			}
		}
		#endregion
		
		public GestionEquipementsVM() : base() {
			this.UCCode = CodesUC.GestionEquipements;
			this.AfficherRebut = false;
			
			this.CreateAfficherRebutCommand();
		}
		
		#region AfficherRebutCommand
		public ICommand AfficherRebutCommand { get; set; }

		private void CreateAfficherRebutCommand() {
			this.AfficherRebutCommand = new RelayCommand<object>(
				this.ExecuteAfficherRebutCommand,
				this.CanExecuteAfficherRebutCommand
			);
		}

		public bool CanExecuteAfficherRebutCommand(object afficherRebut) {
			return true;
		}

		public void ExecuteAfficherRebutCommand(object afficherRebut) {
			this.AfficherRebut = (bool)afficherRebut;
			this.CurrentFilter = string.Empty;
			this.PopulateItems();
		}
		#endregion
		
		
		private void GenerateReportVieEquipement(string nomFichier) {
			if (!string.IsNullOrWhiteSpace(nomFichier)) {
				var gen = new ReportGenerator<ReportVieEquipement>(
						ServiceReportingAdapter.EquipementToReportVieEquipement(this.SelectedItem),
						nomFichier
					);

				gen.SetTitle(string.Format(CultureInfo.CurrentCulture, ResEquipements.TitreRapportVieEquipement, this.SelectedItem.Numero));
				gen.SetSubTitle(string.Format(CultureInfo.CurrentCulture, ResEquipements.SousTitreVieEquipement, this.SelectedItem.Verifications.Count + 1));

				gen.GenerateExcelReport();
				this.ShowUserNotification(string.Format(CultureInfo.CurrentCulture, ResCommon.InfoRapportGenere, nomFichier));
			}
		}
		
		#region ReportCommand
		public override bool CanExecuteReportCommand(CodesReport codeReport) {
			return this.SelectedItem != null;
		}
		
		public override void ExecuteReportCommand(CodesReport codeReport) {
			if(codeReport == CodesReport.VieEquipement && this.SelectedItem != null) {
				Messenger.Default.Send(
					new NMActionFileDialog<string>(
						FileDialogType.SaveFileDialog,
						ResCommon.ExtensionExcel,
						string.Format(
							CultureInfo.CurrentCulture, 
							ResEquipements.NomFichierRapportVieEquipement, 
							DateTime.Now.ToString("yyyyMMdd", CultureInfo.CurrentCulture), 
							this.SelectedItem.Numero
						),
						this.GenerateReportVieEquipement
					)
				);
			}
		}
		#endregion
		
		#region methods override
		protected override void PopulateItems()	{
			base.PopulateItems();

			if (!this.AfficherRebut) {
				this.Items = this.Items.Where(e => !e.EstAuRebut).OrderBy(e => e.Numero);
			}
		}
		
		protected override void PopulateSpecificDatas()	{
			this.Modeles = this.Context.Modeles.ToList().OrderBy(c => c.ToString());
			this.Localisations = this.Context.Localisations.ToList().OrderBy(l => l.Libelle);
		}
		
		protected override Equipement CreateDefaultItem() {
			return new Equipement()
			{
				DateAchat = DateTime.Now,
				DateCreation = DateTime.Now,
				DateModification = DateTime.Now,
				Localisation = this.Localisations.FirstOrDefault(),
				Modele = this.Modeles.FirstOrDefault(),
				Numero = ResEquipements.Defaut_Numero
			};
		}
		
		protected override bool ValidateItem(Equipement item, Collection<string> errors) {
			if (string.IsNullOrWhiteSpace(item.Numero)) {
				errors.Add(ResEquipements.ErrNumeroObligatoire);
			}

			if (item.Modele == null) {
				errors.Add(ResEquipements.ErrModeleObligatoire);
			}
			
			if (item.Localisation == null) {
				errors.Add(ResEquipements.ErrLocalisationObligatoire);
			}

			if (errors.Count == 0 && this.ItemExists(item)) {
				errors.Add(string.Format(CultureInfo.CurrentCulture, ResEquipements.ErrEquipementExiste, item.Numero));
			}

			return errors.Count == 0;
		}
		
		protected override bool ItemExists(Equipement item) {
			return this.Context.Equipements.Count(e => 
			                                       e.Numero.Equals(item.Numero) 
			                                       && e.ID != item.ID
			                                      ) != 0;
		}
		
		protected override bool ItemIsUsed(Equipement item) {
			return item.Verifications != null && item.Verifications.Count != 0;
		}
		
		protected override void FormatValues(Equipement item) {
			item.Numero = (item.Numero == null) ? null : item.Numero.ToUpperInvariant();
		}
		#endregion
	}
}
