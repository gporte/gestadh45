using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using gestadh45.business.Enums;
using gestadh45.business.PersonalizedMsg;
using gestadh45.business.ServicesAdapters;
using gestadh45.model;
using gestadh45.services.Documents;
using gestadh45.services.Documents.Templates;
/*
 * Crée par SharpDevelop.
 * Utilisateur: gp
 * Date: 26/02/2013
 * Heure: 13:20
 * 
 * Pour changer ce modèle utiliser Outils | Options | Codage | Editer les en-têtes standards.
 */
using System;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Windows.Input;

namespace gestadh45.business.ViewModel.InscriptionsVM
{
	/// <summary>
	/// Description of GestionInscriptions.
	/// </summary>
	public class GestionInscriptionsVM : GenericGestionVM<Inscription>
	{
		#region Adherents
		private IOrderedEnumerable<Adherent> _adherents;

		/// <summary>
		/// Obtient/Définit la liste des adhérents
		/// </summary>
		public IOrderedEnumerable<Adherent> Adherents {
			get { return this._adherents; }
			set {
				if (this._adherents != value) {
					this._adherents = value;
					this.RaisePropertyChanged(() => this.Adherents);
				}
			}
		}
		#endregion

		#region Groupes
		private IOrderedEnumerable<Groupe> _groupes;

		/// <summary>
		/// Obtient/Définit la liste des groupes
		/// </summary>
		public IOrderedEnumerable<Groupe> Groupes {
			get { return this._groupes; }
			set {
				if (this._groupes != value) {
					this._groupes = value;
					this.RaisePropertyChanged(() => this.Groupes);
				}
			}
		}
		#endregion

		#region Statuts
		private IOrderedEnumerable<StatutInscription> _statuts;

		/// <summary>
		/// Obtient/Définit la liste des statuts
		/// </summary>
		public IOrderedEnumerable<StatutInscription> Statuts {
			get { return this._statuts; }
			set {
				if (this._statuts != value) {
					this._statuts = value;
					this.RaisePropertyChanged(() => this.Statuts);
				}
			}
		}
		#endregion

		#region Sections
		private IOrderedEnumerable<Section> _sections;

		/// <summary>
		/// Obtient/Définit la liste des sections
		/// </summary>
		public IOrderedEnumerable<Section> Sections {
			get { return this._sections; }
			set {
				if (this._sections != value) {
					this._sections = value;
					this.RaisePropertyChanged(() => this.Sections);
				}
			}
		}
		#endregion
		
		#region SaisonCourante
		private Saison _saisonCourante;
		
		/// <summary>
		/// Obtient/Définit la saison courante
		/// </summary>
		public Saison SaisonCourante {
			get { return this._saisonCourante; }
			internal set {
				if(this._saisonCourante != value) {
					this._saisonCourante = value;
					this.RaisePropertyChanged(()=>this.SaisonCourante);
				}
			}
		}
		#endregion
		
		public GestionInscriptionsVM(string userConnectionString) : base(userConnectionString) {
			this.UCCode = CodesUC.GestionInscriptions;
			this.CreateChangeStatutCertificatCommand();
			this.CreateShowDetailsAdherentCommand();
			this.CreateGenererDocumentCommand();
		}
		
		public GestionInscriptionsVM(string userConnectionString, Guid idAdherent) : this(userConnectionString) {
			var saisonCourante = this.Context.Saisons.Where(x => x.EstSaisonCourante).FirstOrDefault();
			
			if(this.Context.Inscriptions.Where(x => x.Groupe.Saison.ID == saisonCourante.ID).Count(i => i.Adherent.ID == idAdherent) > 0) {
				this.SelectedItem = this.Context.Inscriptions
					.Where(x => x.Groupe.Saison.ID == saisonCourante.ID)
					.FirstOrDefault(i => i.Adherent.ID == idAdherent);
				
				if(this.SelectedItem != null) {
					Messenger.Default.Send(new NMSelectionElement<Inscription>(this.SelectedItem));
				}
			}
			else {
				this.InscrireAdherent(idAdherent);
			}
		}
		
		private string GetDocumentFileName(CodesDocument codeDocument) {
			if (codeDocument.Equals(CodesDocument.AttestationPDF)) {
				return string.Format(CultureInfo.CurrentCulture, ResDocuments.AttestationPDFFileName, this.SelectedItem.Adherent.ToString());
			}
			else {
				return string.Format(CultureInfo.CurrentCulture, ResDocuments.InscriptionPDFFileName, this.SelectedItem.Adherent.ToString());
			}
		}
		
		private void InscrireAdherent(Guid idAdherent) {
			var item = this.CreateDefaultItem();
			item.Adherent = this.Context.Adherents.Find(idAdherent);
			
			this.Context.Inscriptions.Add(item);
			this.Context.SaveChanges();
			
			this.ShowUserNotification(string.Format(CultureInfo.CurrentCulture, ResCommon.TemplateAddItemNotification, item));		
			
			this.PopulateItems();
			this.SelectedItem = item;
			Messenger.Default.Send(new NMSelectionElement<Inscription>(this.SelectedItem));
		}
		
		#region GenererDocumentCommand
		public ICommand GenererDocumentCommand { get; set; }

		private void CreateGenererDocumentCommand() {
			this.GenererDocumentCommand = new RelayCommand<CodesDocument>(
				this.ExecuteGenererDocumentCommand,
				this.CanExecuteGenererDocumentCommand
			);
		}

		public bool CanExecuteGenererDocumentCommand(CodesDocument codeDocument) {
			return this.SelectedItem != null;
		}

		public void ExecuteGenererDocumentCommand(CodesDocument codeDocument) {
			if (this.SelectedItem != null) {
				// recuperation du chemin d'enregistrement et passage au callback qui s'occupe de la génération a proprement parler
				Messenger.Default.Send<NMActionFileDialog<string>>(
					new NMActionFileDialog<string>(
						FileDialogType.SaveFileDialog,
						ResDocuments.ExtensionPDF,
						this.GetDocumentFileName(codeDocument),
						callback =>
						{
							this.GenererDocumentCallBack(callback, codeDocument);
						}
					)
				);				
			}
		}

		private void GenererDocumentCallBack(string filePath, CodesDocument codeDocument) {
			if (!string.IsNullOrWhiteSpace(filePath)) {
				var gen = new GenerateurDocumentPDF(
						ServiceDocumentAdapter.InscriptionToDonneesDocument(
							this.Context.InfosClub.FirstOrDefault(),
							this.SelectedItem
						),
						filePath
					);

				var userInfo = string.Format(CultureInfo.CurrentCulture, ResCommon.InfosDocumentGenere, filePath);

				switch (codeDocument) {
					case CodesDocument.AttestationPDF:
						gen.CreerDocumentAttestation();
						break;

					case CodesDocument.FicheInscriptionPDF:
						gen.CreerDocumentInscription();
						break;

					default:
						userInfo = ResInscriptions.ErrCodeDocumentInconnu;
						break;
				}

				this.ShowUserNotification(userInfo);
			}
		}
		#endregion
		
		#region ChangeStatutCertificatCommand
		public ICommand ChangeStatutCertificatCommand { get; set; }

		private void CreateChangeStatutCertificatCommand() {
			this.ChangeStatutCertificatCommand = new RelayCommand(
				this.ExecuteChangeStatutCertificatCommand,
				this.CanExecuteChangeStatutCertificatCommand
			);
		}

		public bool CanExecuteChangeStatutCertificatCommand() {
			return this.SelectedItem != null;
		}

		public void ExecuteChangeStatutCertificatCommand() {
			this.SelectedItem.CertificatMedicalRemis = !this.SelectedItem.CertificatMedicalRemis;
			this.Context.Entry(this.SelectedItem).State = EntityState.Modified;
			this.Context.SaveChanges();

			this.RaisePropertyChanged(() => this.SelectedItem);

			this.ShowUserNotification(string.Format(CultureInfo.CurrentCulture, ResInscriptions.InfosChangementStatutCertificatMedical, this.SelectedItem.Adherent.ToString()));
		}
		#endregion
		
		#region ShowDetailsAdherentCommand
		public ICommand ShowDetailsAdherentCommand { get; set; }

		private void CreateShowDetailsAdherentCommand() {
			this.ShowDetailsAdherentCommand = new RelayCommand(
				this.ExecuteShowDetailsAdherentCommand,
				this.CanExecuteShowDetailsAdherentCommand
			);
		}

		public bool CanExecuteShowDetailsAdherentCommand() {
			return (this.SelectedItem != null && this.SelectedItem.Adherent != null);
		}

		public void ExecuteShowDetailsAdherentCommand() {
			if (this.SelectedItem != null && this.SelectedItem.Adherent != null) {
				Messenger.Default.Send(
					new NMShowUC<Adherent>(
						CodesUC.GestionAdherents,
						this.SelectedItem.Adherent
					)
				);
			}
		}
		#endregion
		
		#region methods override
		/// <summary>
		/// Override de la méthode PopulateItems pour ne prendre que les inscriptions de la saison courante
		/// </summary>
		/// <param name="filter"></param>
		protected override void PopulateItems() {
			base.PopulateItems();
			this.Items = this.Items
				.Where(i => i.Groupe.Saison.EstSaisonCourante)
				.OrderBy(i => i.Groupe.ToString())
				.ThenBy(i => i.Adherent.ToString());
		}
		
		protected override void PopulateSpecificDatas() {
			this.Adherents = this.Context.Adherents.ToList().OrderBy(a => a.ToString());
			
			this.Groupes = this.Context.Groupes.ToList()
				.Where(g => g.Saison.EstSaisonCourante)
				.OrderBy(g => g.JourSemaine)
				.ThenBy(g => g.HeureDebut.Hour);

			this.Statuts = Enum.GetValues(typeof(StatutInscription)).Cast<StatutInscription>().OrderBy(s => s);

			this.Sections = this.Context.Sections.ToList().OrderByDescending(s => s.EstDefaut);
			
			this.SaisonCourante = this.Context.Saisons.Where(s => s.EstSaisonCourante).FirstOrDefault();
		}
		
		protected override Inscription CreateDefaultItem() {
			return new Inscription() {
				Adherent = this.Adherents.FirstOrDefault(),
				CertificatMedicalRemis = false,
				Commentaire = string.Empty,
				Cotisation = decimal.Zero,
				DateCreation = DateTime.Now,
				DateModification = DateTime.Now,
				Groupe = this.Groupes.FirstOrDefault(),
				MontantLicence = decimal.Zero,
				NumLicence = string.Empty,
				Section = this.Sections.Where(s => s.EstDefaut).FirstOrDefault(),
				StatutInscription = StatutInscription.ASuivre
			};
		}
		
		protected override bool ValidateItem(Inscription item, Collection<string> errors) {
			if (item.Adherent == null) {
				errors.Add(ResInscriptions.ErrAdherentObligatoire);
			}

			if (item.Section == null) {
				errors.Add(ResInscriptions.ErrSectionObligatoire);
			}

			if (item.Groupe == null) {
				errors.Add(ResInscriptions.ErrGroupeObligatoire);
			}

			if (errors.Count == 0 && this.ItemExists(item)) {
				errors.Add(ResInscriptions.ErrInscriptionExiste);
			}
			
			return errors.Count == 0;
		}
		
		protected override void FormatValues(Inscription item) {
			item.DateModification = DateTime.Now;
		}
		
		/// <summary>
		/// Vérifie si une autre inscription avec le même couple adhérent/groupe existe déjà
		/// </summary>
		/// <param name="item">Inscription à vérifier</param>
		/// <returns>True si l'inscription existe déjà, False sinon</returns>
		protected override bool ItemExists(Inscription item) {
			return this.Context.Inscriptions.Count(i => 
			                                        i.Adherent.ID == item.Adherent.ID 
			                                        && i.Groupe.ID == item.Groupe.ID
			                                       	&& i.ID != item.ID
			                                       ) != 0;
		}
		#endregion
	}
}
