using System.Globalization;
using gestadh45.services.Documents;
using gestadh45.services.Documents.Templates;
using MigraDoc.DocumentObjectModel;

namespace gestadh45.services.Documents.Templates
{
	public abstract class GeneriqueDocument
	{
		private Document _document;
		
		#region Page
		protected Section Page { get; private set; }
		#endregion
		
		#region DonneesDocument
		protected DonneesDocument Donnees { get; private set; }
		#endregion

		protected GeneriqueDocument(Document document, DonneesDocument donnees) {
			this._document = document;
			this.Page = this._document.AddSection();
			this.Donnees = donnees;
		}

		protected void CreerEntete() {
			var parNomClubSaison = this.Page.AddParagraph();
			parNomClubSaison.AddFormattedText(
				string.Format(CultureInfo.CurrentCulture, ResDocuments.LibelleNomClubSaison, this.Donnees.NomClub, this.Donnees.Saison),
				TextFormat.Bold
			);

			var parAdresseClub = this.Page.AddParagraph();
			parAdresseClub.AddFormattedText(
				string.Format(CultureInfo.CurrentCulture, ResDocuments.LibelleAdresseClub, this.Donnees.AdresseClub)
			);

			var parCodePostalVilleClub = this.Page.AddParagraph();
			parCodePostalVilleClub.AddFormattedText(
				string.Format(CultureInfo.CurrentCulture, ResDocuments.LibelleCodePostalVilleClub, this.Donnees.CodePostalClub, this.Donnees.VilleClub)
			);

			var parTelClub = this.Page.AddParagraph();
			parTelClub.AddFormattedText(
				string.Format(CultureInfo.CurrentCulture, ResDocuments.LibelleTelephone, this.Donnees.TelephoneCLub)
			);

			var parMailClub = this.Page.AddParagraph();
			parMailClub.AddFormattedText(
				string.Format(CultureInfo.CurrentCulture, ResDocuments.LibelleMailClub, this.Donnees.MailClub)
			);

			var parSiteWebClub = this.Page.AddParagraph();
			parSiteWebClub.AddFormattedText(
				string.Format(CultureInfo.CurrentCulture, ResDocuments.LibelleSiteWebClub, this.Donnees.SiteWebClub)
			);

			var parNumeroClub = this.Page.AddParagraph();
			parNumeroClub.AddFormattedText(
				string.Format(CultureInfo.CurrentCulture, ResDocuments.LibelleNumeroClub, this.Donnees.NumeroClub)
			);

			var parSiretClub = this.Page.AddParagraph();
			parSiretClub.AddFormattedText(
				string.Format(CultureInfo.CurrentCulture, ResDocuments.LibelleSiret, this.Donnees.SiretClub)
			);
		}

		protected void CreerSeparateur() {
			var parSeparateur = this.Page.AddParagraph();
			parSeparateur.AddFormattedText(ResDocuments.Separateur, TextFormat.Bold);
			parSeparateur.Format.Alignment = ParagraphAlignment.Center;
		}

		protected void CreerRetourLigne() {
			var parLigneVide = this.Page.AddParagraph();
			parLigneVide.AddLineBreak();
		}
	}
}
