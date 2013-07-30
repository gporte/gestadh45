using System.Globalization;
using gestadh45.dal;
using gestadh45.model;
using gestadh45.services.Documents;
using System.Configuration;

namespace gestadh45.business.ServicesAdapters
{
	public static class ServiceDocumentAdapter
	{
		private const string TamponClubPathSettings = "tamponClubPath";
		
		/// <summary>
		/// Créé un objet document
		/// </summary>
		/// <param name="infosClub">Infos du club</param>
		/// <param name="inscription">Inscription</param>
		/// <returns>Données du document</returns>
		public static DonneesDocument InscriptionToDonneesDocument(InfosClub infosClub, Inscription inscription) {
			DonneesDocument donnees = new DonneesDocument()
			{
				TamponClubPath = ConfigurationManager.AppSettings[TamponClubPathSettings],
				AdresseAdherent = inscription.Adherent.Adresse,
				AdresseClub = infosClub.Adresse,
				CodePostalAdherent = inscription.Adherent.Ville.CodePostal,
				CodePostalClub = infosClub.Ville.CodePostal,
				CotisationInscription = inscription.Cotisation.ToString(CultureInfo.CurrentCulture),
				MontantLicence = inscription.MontantLicence.ToString(CultureInfo.CurrentCulture),
				MontantTotal = (inscription.Cotisation + inscription.MontantLicence).ToString(CultureInfo.CurrentCulture),
				EstLicencie = inscription.EstLicencie,
				DateNaissanceAdherent = inscription.Adherent.DateNaissance.ToShortDateString(),
				GroupeInscription = inscription.Groupe.ToString(),
				SectionInscription = inscription.Section.Libelle,
				Mail1Adherent = inscription.Adherent.Mail1,
				Mail2Adherent = inscription.Adherent.Mail2,
				Mail3Adherent = inscription.Adherent.Mail3,
				MailClub = infosClub.Mail,
				NomAdherent = inscription.Adherent.Nom,
				NomClub = infosClub.Nom,
				NumeroClub = infosClub.Numero,
				PrenomAdherent = inscription.Adherent.Prenom,
				Saison = inscription.Groupe.Saison.ToShortString(),
				SiretClub = infosClub.Siret,
				SiteWebClub = infosClub.SiteWeb,
				Telephone1Adherent = inscription.Adherent.Telephone1,
				Telephone2Adherent = inscription.Adherent.Telephone2,
				Telephone3Adherent = inscription.Adherent.Telephone3,
				TelephoneCLub = infosClub.Telephone,
				VilleAdherent = inscription.Adherent.Ville.Libelle,
				VilleClub = infosClub.Ville.Libelle
			};

			return donnees;
		}
	}
}
