using System.Globalization;
using gestadh45.services.Documents;
using gestadh45.services.Documents.Templates;
using MigraDoc.DocumentObjectModel;

namespace gestadh45.services.Documents.Templates
{
	public class InscriptionDocument : GeneriqueDocument
	{
		public InscriptionDocument(Document document, DonneesDocument donnees)
			: base(document, donnees) {
		}

		private void CreerZoneTitre() {
			var par = this.Page.AddParagraph();

			par.AddFormattedText(
				string.Format(CultureInfo.CurrentCulture, ResDocuments.LibelleTitreFicheInscription, this.Donnees.Saison, this.Donnees.NomClub),
				TextFormat.Bold
			);
			par.Format.Alignment = ParagraphAlignment.Center;
		}

		private void CreerZoneCoordonneesAdherent() {
			var parNomPrenomAdherent = this.Page.AddParagraph();
			parNomPrenomAdherent.AddFormattedText(
				string.Format(CultureInfo.CurrentCulture, ResDocuments.LibelleNomAdherent, this.Donnees.NomAdherent, this.Donnees.PrenomAdherent)
			);

			var parDateNaissance = this.Page.AddParagraph();
			parDateNaissance.AddFormattedText(
				string.Format(CultureInfo.CurrentCulture, ResDocuments.LibelleNeLe, this.Donnees.DateNaissanceAdherent)
			);

			var parAdresse = this.Page.AddParagraph();
			parAdresse.AddFormattedText(
				string.Format(CultureInfo.CurrentCulture, ResDocuments.LibelleAdresse, this.Donnees.AdresseAdherent, this.Donnees.CodePostalAdherent, this.Donnees.VilleAdherent)
			);

			var parTelMail1 = this.Page.AddParagraph();
			parTelMail1.AddFormattedText(
				string.Format(CultureInfo.CurrentCulture, ResDocuments.LibelleTelMail, this.Donnees.Telephone1Adherent, this.Donnees.Mail1Adherent)
			);

			var parTelMail2 = this.Page.AddParagraph();
			parTelMail2.AddFormattedText(
				string.Format(CultureInfo.CurrentCulture, ResDocuments.LibelleTelMail, this.Donnees.Telephone2Adherent, this.Donnees.Mail2Adherent)
			);

			var parTelMail3 = this.Page.AddParagraph();
			parTelMail3.AddFormattedText(
				string.Format(CultureInfo.CurrentCulture, ResDocuments.LibelleTelMail, this.Donnees.Telephone3Adherent, this.Donnees.Mail3Adherent)
			);
		}

		private void CreerZoneInfosInscription() {
			var parInfosCotisation = this.Page.AddParagraph();
			parInfosCotisation.AddFormattedText(
				string.Format(CultureInfo.CurrentCulture, ResDocuments.LibelleCotisation, this.Donnees.CotisationInscription)
			);

			var parInfosSection = this.Page.AddParagraph();
			parInfosSection.AddFormattedText(
				string.Format(CultureInfo.CurrentCulture, ResDocuments.LibelleSection, this.Donnees.SectionInscription)
			);

			var parInfosGroupe = this.Page.AddParagraph();
			parInfosGroupe.AddFormattedText(
				string.Format(CultureInfo.CurrentCulture, ResDocuments.LibelleGroupe, this.Donnees.GroupeInscription)
			);
		}

		private void CreerZoneSignature() {
			var parSignature = this.Page.AddParagraph();
			parSignature.AddFormattedText(
				ResDocuments.LibelleSignatureAdherent
			);
		}

		private void CreerZoneAutorisation() {
			var parTitreAutorisation = this.Page.AddParagraph();
			parTitreAutorisation.AddFormattedText(
				ResDocuments.LibelleAutorisationParentale,
				TextFormat.Bold
			);

			var parTexteAutorisation = this.Page.AddParagraph();
			parTexteAutorisation.AddFormattedText(
				ResDocuments.TexteAutorisationParentale
			);
		}

		private void CreerZoneSignatureParents() {
			var parSignature = this.Page.AddParagraph();
			parSignature.AddFormattedText(
				ResDocuments.LibelleSignatureParents
			);
		}
		

		public void GenererContenuDocument() {
			this.CreerEntete();
			this.CreerRetourLigne();

			this.CreerSeparateur();
			this.CreerRetourLigne();

			this.CreerZoneTitre();
			this.CreerRetourLigne();

			this.CreerZoneCoordonneesAdherent();
			this.CreerRetourLigne();

			this.CreerZoneInfosInscription();
			this.CreerRetourLigne();

			this.CreerRetourLigne();
			this.CreerZoneSignature();
			this.CreerRetourLigne();

			this.CreerSeparateur();
			this.CreerRetourLigne();

			this.CreerZoneAutorisation();
			this.CreerRetourLigne();

			this.CreerZoneSignatureParents();
		}
	}
}
