using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using gestadh45.business.Enums;
using gestadh45.business.PersonalizedMsg;
using gestadh45.business.ServicesAdapters;
using gestadh45.model;
using gestadh45.services.Reporting;
using gestadh45.services.Reporting.Templates;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Windows.Input;

namespace gestadh45.business.ViewModel.CampagnesVerificationVM
{
	/// <summary>
	/// Description of GestionCampagnesVerification.
	/// </summary>
	public class GestionCampagnesVerificationVM : GenericGestionVM<CampagneVerification>
	{
		public GestionCampagnesVerificationVM() : base() {
			this.UCCode = CodesUC.GestionCampagnesVerification;
			this.CreateSaisirCommand();
		}
		
		private void ExecuteDeleteCommandCallBack(bool deleteConfirmation) {
			if (deleteConfirmation) {
				var libelle = this.SelectedItem.ToString();
				
				this.DeleteVerifications();

				this.Context.Entry(this.SelectedItem).State = EntityState.Deleted;
				this.Context.SaveChanges();

				this.PopulateItems();
				this.SelectedItem = this.Items.FirstOrDefault();
				this.ShowUserNotification(string.Format(CultureInfo.CurrentCulture, ResCommon.InfoElementSupprime, libelle));
				Messenger.Default.Send(new NMSelectionElement<CampagneVerification>(this.SelectedItem));
			}
		}
		
		/// <summary>
		/// Supprime toutes les vérifications d'une campagne
		/// </summary>
		private  void DeleteVerifications() {
			while (this.SelectedItem.Verifications.Count > 0) {
				this.Context.Entry(
					this.SelectedItem.Verifications.FirstOrDefault()
				).State = EntityState.Deleted;
			}

			this.Context.SaveChanges();
		}
		
		#region SaisirCommand
		public ICommand SaisirCommand { get; set; }

		private void CreateSaisirCommand() {
			this.SaisirCommand = new RelayCommand(
				this.ExecuteSaisirCommand,
				this.CanExecuteSaisirCommand
			);
		}

		public bool CanExecuteSaisirCommand() {
			// la commande Saisir est activée seulement si une campagne est sélectionnée
			// ET qu'elle n'est pas encore validée
			return this.SelectedItem != null
				&& !this.SelectedItem.EstValidee;
		}

		public void ExecuteSaisirCommand() {
			Messenger.Default.Send<NMShowUC<CampagneVerification>>(
				new NMShowUC<CampagneVerification>(CodesUC.GestionSaisiesVerifications, this.SelectedItem)
			);
		}
		#endregion
		
		#region ReportCommand
		public override bool CanExecuteReportCommand(CodesReport codeReport) {
			return this.SelectedItem != null;
		}

		public override void ExecuteReportCommand(CodesReport codeReport) {
			switch (codeReport) {
				case CodesReport.VerificationEquipementExcel:
					Messenger.Default.Send(
						new NMActionFileDialog<string>(
							FileDialogType.SaveFileDialog,
							ResCommon.ExtensionExcel,
							string.Format(
								CultureInfo.CurrentCulture, 
								ResCampagnesVerification.NomFichierRapportCampagneVerification, 
								this.SelectedItem.Date.ToString("yyyyMMdd", CultureInfo.CurrentCulture)
							),
							this.GenerateReportCampagneVerification
						)
					);
					break;

				default:
					break;
			}
		}

		private void GenerateReportCampagneVerification(string nomFichier) {
			if (nomFichier != null) {
				var gen = new ReportGenerator<ReportVerificationEquipement>(
						ServiceReportingAdapter.CampagneVerificationToReportVerificationEquipement(this.SelectedItem),
						nomFichier
					);

				gen.SetTitle(string.Format(CultureInfo.CurrentCulture, ResCampagnesVerification.TitreRapportVerificationEquipement, this.SelectedItem.Date.ToShortDateString()));
				gen.SetSubTitle(string.Format(CultureInfo.CurrentCulture, ResCampagnesVerification.SousTitreRapportVerificationEquipement, this.SelectedItem.Responsable, this.SelectedItem.NbEquipements));
				gen.GenerateExcelReport();

				this.ShowUserNotification(string.Format(CultureInfo.CurrentCulture, ResCommon.InfoRapportGenere, nomFichier));
			}
		}
		#endregion
		
		#region methods override
		public override bool CanExecuteSaveItemCommand() {
			return this.SelectedItem != null && !this.SelectedItem.EstValidee;
		}
		
		public override void ExecuteDeleteItemCommand() {
			if(this.SelectedItem != null && !this.SelectedItem.EstValidee) {
				Messenger.Default.Send(
					new NMAskConfirmationDialog<bool>(
						this.ExecuteDeleteCommandCallBack, 
						ResCampagnesVerification.TexteConfirmationSuppression,
						ResCommon.TitreConfirmation
					)
				);
			}
		}
		
		protected override CampagneVerification CreateDefaultItem()	{
			var campagne = new CampagneVerification() 
							{
								Date = DateTime.Now,
								EstValidee = false,
								Responsable = ResCampagnesVerification.Defaut_Responsable,
								Verifications = new List<Verification>()
							};
			
			foreach (var equip in this.Context.Equipements.ToList().Where(e => !e.EstAuRebut)) {
				var verif = new Verification()
				{
					CampagneVerification = campagne,
					Equipement = equip,
					StatutVerification = StatutVerification.AVerifier
				};
				
				campagne.Verifications.Add(verif);
			}
			
			return campagne;
		}
		
		protected override bool ItemIsUsed(CampagneVerification item) {
			return item != null && item.EstValidee;
		}
		
		protected override void FormatValues(CampagneVerification item)	{
			item.Date = DateTime.Now;
			item.EstValidee = false;
		}
		
		protected override bool ValidateItem(CampagneVerification item, Collection<string> errors) {
			if (string.IsNullOrWhiteSpace(item.Responsable)) {
				errors.Add(ResCampagnesVerification.ErrResponsableObligatoire);
			}

			return errors.Count == 0;
		}
		#endregion
	}
}
