using System;
using System.Globalization;
using System.IO;

using MigraDoc.DocumentObjectModel;

namespace gestadh45.services.Documents.Templates
{
	public class AttestationDocument : GeneriqueDocument
	{
		public AttestationDocument(Document document, DonneesDocument donnees)
			: base(document, donnees) {
		}

		private void CreerZoneTitre() {
			var par = this.Page.AddParagraph();
			par.AddFormattedText(
				string.Format(CultureInfo.CurrentCulture, ResDocuments.LibelleRecuPourAdhesion, this.Donnees.Saison, this.Donnees.NomClub),
				TextFormat.Bold
			);
			par.Format.Alignment = ParagraphAlignment.Center;
		}

		private void CreerZoneCoordonneesAdherent() {
			var parNomAdherent = this.Page.AddParagraph();
			parNomAdherent.AddFormattedText(
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
		}

		private void CreerZoneInfosAttestation() {
			var parCotisation = this.Page.AddParagraph();
			parCotisation.AddFormattedText(
				string.Format(CultureInfo.CurrentCulture, ResDocuments.LibelleCotisation, this.Donnees.CotisationInscription),
				TextFormat.Bold
			);

			if (this.Donnees.EstLicencie) {
				var parMontantLicence = this.Page.AddParagraph();
				parMontantLicence.AddFormattedText(
					string.Format(CultureInfo.CurrentCulture, ResDocuments.LibelleMontantLicence, this.Donnees.MontantLicence),
					TextFormat.Bold
				);

				var parMontantTotal = this.Page.AddParagraph();
				parMontantTotal.AddFormattedText(
					string.Format(CultureInfo.CurrentCulture, ResDocuments.LibelleMontantTotal, this.Donnees.MontantTotal),
					TextFormat.Bold
				);
			}
		}

		private void CreerZoneLieuDate() {
			var par = this.Page.AddParagraph();
			par.AddFormattedText(
				string.Format(CultureInfo.CurrentCulture, ResDocuments.LibelleFaitALe, this.Donnees.VilleClub, DateTime.Now.ToShortDateString())
			);
		}

		private void CreerZoneTampon() {
			if (!string.IsNullOrWhiteSpace(this.Donnees.TamponClubPath) && File.Exists(this.Donnees.TamponClubPath)) {
				this.Page.AddImage(this.Donnees.TamponClubPath);
			}			
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

			this.CreerZoneInfosAttestation();
			this.CreerRetourLigne();

			this.CreerRetourLigne();
			this.CreerZoneLieuDate();

			this.CreerRetourLigne();
			this.CreerZoneTampon();
		}
	}
}
