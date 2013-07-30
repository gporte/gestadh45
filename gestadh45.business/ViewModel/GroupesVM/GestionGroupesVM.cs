/*
 * Crée par SharpDevelop.
 * Utilisateur: gp
 * Date: 01/03/2013
 * Heure: 12:00
 * 
 * Pour changer ce modèle utiliser Outils | Options | Codage | Editer les en-têtes standards.
 */
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using gestadh45.business.Enums;
using gestadh45.business.PersonalizedMsg;
using gestadh45.business.ServicesAdapters;
using gestadh45.model;
using gestadh45.services.Documents;
using gestadh45.services.Documents.Templates;
using gestadh45.services.Reporting;
using gestadh45.services.Reporting.Templates;

namespace gestadh45.business.ViewModel.GroupesVM
{
	/// <summary>
	/// Description of GestionGroupes.
	/// </summary>
	public class GestionGroupesVM : GenericGestionVM<Groupe>
	{
		#region JoursSemaine
		private IOrderedEnumerable<JourSemaine> _joursSemaine;

		/// <summary>
		/// Obtient/Définit la liste des jours de la semaine
		/// </summary>
		public IOrderedEnumerable<JourSemaine> JoursSemaine {
			get { return this._joursSemaine; }
			set {
				if (this._joursSemaine != value) {
					this._joursSemaine = value;
						this.RaisePropertyChanged(()=>this.JoursSemaine);
				}
			}
		}
		#endregion
		
		public GestionGroupesVM(string userConnectionString) : base(userConnectionString) {
			this.UCCode = CodesUC.GestionGroupes;
			this.CreateGenererDocumentsGroupeCommand();
			this.CreateExtraireEmailsCommand();
			this.CreateExtraireEmailsGroupeCommand();
			this.PopulateSpecificDatas();
		}
		
		private string GetDocumentFileName(CodesDocument codeDocument, Inscription ins) {
			if (codeDocument.Equals(CodesDocument.AttestationPDF)) {
				return string.Format(CultureInfo.CurrentCulture, ResDocuments.AttestationPDFFileName, ins.Adherent.ToString());
			}
			else {
				return string.Format(CultureInfo.CurrentCulture, ResDocuments.InscriptionPDFFileName, ins.Adherent.ToString());
			}
		}
		
		#region GenererDocumentGroupesCommand
		public ICommand GenererDocumentsGroupeCommand { get; set; }

		private void CreateGenererDocumentsGroupeCommand() {
			this.GenererDocumentsGroupeCommand = new RelayCommand<CodesDocument>(
				this.ExecuteGenererDocumentsGroupeCommand,
				this.CanExecuteGenererDocumentsGroupeCommand
			);
		}

		public bool CanExecuteGenererDocumentsGroupeCommand(CodesDocument codeDocument) {
			return this.SelectedItem != null;
		}

		public void ExecuteGenererDocumentsGroupeCommand(CodesDocument codeDocument) {
			if (this.SelectedItem != null) {
				// recuperation du dossier d'enregistrement et passage au callback qui s'occupe de la génération a proprement parler
				Messenger.Default.Send<NMActionFolderDialog<string>>(
					new NMActionFolderDialog<string>(
						callback =>
						{
							this.GenererDocumentsCallBack(callback, codeDocument);
						}
					)
				);			
			}
		}

		private void GenererDocumentsCallBack(string folderPath, CodesDocument codeDocument) {
			if (!string.IsNullOrWhiteSpace(folderPath)) {
				var nbDocs = 0;
				
				foreach (Inscription ins in this.SelectedItem.Inscriptions.Where(i => i.StatutInscription != StatutInscription.Annulee)) {
					var gen = new GenerateurDocumentPDF(
						ServiceDocumentAdapter.InscriptionToDonneesDocument(this.Context.InfosClub.FirstOrDefault(), ins),
						string.Concat(folderPath, @"\", this.GetDocumentFileName(codeDocument, ins))
					);

					switch (codeDocument) {
						case CodesDocument.AttestationPDF:
							gen.CreerDocumentAttestation();
							nbDocs++;
							break;

						case CodesDocument.FicheInscriptionPDF:
							gen.CreerDocumentInscription();
							nbDocs++;
							break;

						default:
							break;
					}
				}

				this.ShowUserNotification(string.Format(CultureInfo.CurrentCulture, ResCommon.InfoDocumentsGeneres, nbDocs, folderPath));
			}
		}
		#endregion
		
		#region ReportCommand
		public override bool CanExecuteReportCommand(CodesReport codeReport) {
			return this.SelectedItem != null;
		}

		public override void ExecuteReportCommand(CodesReport codeReport) {
			switch (codeReport) {
				case CodesReport.ListeAdherents:
					Messenger.Default.Send(
						new NMActionFileDialog<string>(
							FileDialogType.SaveFileDialog,
							ResCommon.ExtensionExcel,
							string.Format(CultureInfo.CurrentCulture, ResGroupes.NomFichierRapportListeAdherents, this.SelectedItem.Libelle), 
							this.GenerateReportListeAdherentsGroupe
						)
					);
					break;

				default:
					break;
			}
		}

		private void GenerateReportListeAdherentsGroupe(string nomFichier) {
			if (nomFichier != null) {
				var gen = new ReportGenerator<ReportListeAdherents>(
						ServiceReportingAdapter.GroupeToReportListeAdherents(this.SelectedItem),
						nomFichier
					);

				gen.SetTitle(ResGroupes.TitreRapportListeAdherents);
				gen.SetSubTitle(string.Format(CultureInfo.CurrentCulture, ResGroupes.SousTitreRapportListeAdherents, this.SelectedItem.Inscriptions.Count()));
				gen.GenerateExcelReport();

				this.ShowUserNotification(string.Format(CultureInfo.CurrentCulture, ResCommon.InfoRapportGenere, nomFichier));
			}
		}
		#endregion
		
		#region ExtraireEmailsGroupeCommand
		public ICommand ExtraireEmailsGroupeCommand { get; set; }
		
		private void CreateExtraireEmailsGroupeCommand() {
			this.ExtraireEmailsGroupeCommand = new RelayCommand(
				this.ExecuteExtraireEmailsGroupeCommand,
				this.CanExecuteExtraireEmailsGroupeCommand
			);
		}

		public bool CanExecuteExtraireEmailsGroupeCommand() {
			return  this.SelectedItem != null;
		}

		public void ExecuteExtraireEmailsGroupeCommand() {
			var listeMails = new List<string>();

			foreach (var ins in this.SelectedItem.Inscriptions.Where(i => i.StatutInscription != StatutInscription.Annulee)) {
				if (!string.IsNullOrWhiteSpace(ins.Adherent.Mail1)) { 
					listeMails.Add(ins.Adherent.Mail1); 
				}
			}

			var chaineMails = string.Join(",", listeMails);

			// copie du résultat dans le presse papier
			this.ExecuteCopyValueCommand(chaineMails);
			
			// affichage du résultat dans la zone de notification
			this.ShowUserNotification(string.Format(CultureInfo.CurrentCulture, ResCommon.InfoCopiePressePapier, chaineMails));
		}
		#endregion
		
		#region ExtraireEmailsCommand
		public ICommand ExtraireEmailsCommand { get; set; }

		private void CreateExtraireEmailsCommand() {
			this.ExtraireEmailsCommand = new RelayCommand(
				this.ExecuteExtraireEmailsCommand,
				this.CanExecuteExtraireEmailsCommand
			);
		}

		public bool CanExecuteExtraireEmailsCommand() {
			return  this.Items != null;
		}

		public void ExecuteExtraireEmailsCommand() {
			var listeMails = new List<string>();

			foreach (var groupe in this.Items) {
				foreach (var ins in groupe.Inscriptions.Where(i => i.StatutInscription != StatutInscription.Annulee)) {
					if (!string.IsNullOrWhiteSpace(ins.Adherent.Mail1)) { 
						listeMails.Add(ins.Adherent.Mail1); 
					}
				}
			}

			var chaineMails = string.Join(",", listeMails);

			// copie du résultat dans le presse papier
			this.ExecuteCopyValueCommand(chaineMails);
			
			// affichage du résultat dans la zone de notification
			this.ShowUserNotification(string.Format(CultureInfo.CurrentCulture, ResCommon.InfoCopiePressePapier, chaineMails));
		}
		#endregion
		
		#region method override
		/// <summary>
		/// Override de la méthode PopulateItems pour ne prendre que les groupes de la saison courante
		/// </summary>
		/// <param name="filter"></param>
		protected override void PopulateItems() {
			base.PopulateItems();
			this.Items = this.Items.Where(g => g.Saison.EstSaisonCourante).OrderBy(g => g.ToString());
		}
		
		protected override void PopulateSpecificDatas()	{
			this.JoursSemaine = Enum.GetValues(typeof(JourSemaine)).Cast<JourSemaine>().OrderBy(j => j);
		}
		
		protected override Groupe CreateDefaultItem() {
			return new Groupe() {
				JourSemaine = JourSemaine.Dimanche,
				Libelle = ResGroupes.DefaultLibelle,
				NbPlaces = 0,
				Saison = this.Context.Saisons.FirstOrDefault(s => s.EstSaisonCourante),
				HeureDebut = DateTime.Today,
				HeureFin = DateTime.Today.AddHours(1)
			};
		}
		
		protected override bool ValidateItem(Groupe item, Collection<string> errors) {
			if (string.IsNullOrWhiteSpace(item.Libelle)) {
				errors.Add(ResGroupes.ErrLibelleObligatoire);
			}

			if (item.HeureDebut == DateTime.MinValue) {
				errors.Add(ResGroupes.ErrHeureDebutObligatoire);
			}

			if (item.HeureFin == DateTime.MinValue) {
				errors.Add(ResGroupes.ErrHeureFinObligatoire);
			}

			if((item.HeureDebut.Hour > item.HeureFin.Hour
				|| (item.HeureDebut.Hour == item.HeureFin.Hour && item.HeureDebut.Minute >= item.HeureFin.Minute))) {

				errors.Add(ResGroupes.ErrHeureFinInfHeureSup);
			}			
			
			if (errors.Count == 0 && this.ItemExists(item)) {
				errors.Add(ResGroupes.ErrGroupeExiste);
			}
			
			return errors.Count == 0;
		}
		
		/// <summary>
		/// Vérifie si un groupe du avec le même libellé (mais un ID différent => pour le cas de la màj) existe déjà dans la saison courante
		/// </summary>
		/// <param name="item">Groupe à tester</param>
		/// <returns>True si un groupe existe déjà, False sinon</returns>
		protected override bool ItemExists(Groupe item) {
			return this.Context.Groupes.Count(g => 
                                   g.Saison.EstSaisonCourante 
                                   && g.Libelle.Equals(item.Libelle)
                                   && item.ID != g.ID
                                  ) != 0;
		}
		
		/// <summary>
		/// Vérifie si un groupe est utilisé par au moins une inscription
		/// </summary>
		/// <param name="item">Groupe à vérifier</param>
		/// <returns>True si le groupe possède au moins une inscription, False sinon</returns>
		protected override bool ItemIsUsed(Groupe item) {
			return item != null 
				&& item.Inscriptions != null 
				&& item.Inscriptions.Count > 0;
		}
		
		protected override void FormatValues(Groupe item) {
			item.Libelle = (item.Libelle == null) ? null : item.Libelle.ToUpperInvariant();
		}
		#endregion
	}
}
