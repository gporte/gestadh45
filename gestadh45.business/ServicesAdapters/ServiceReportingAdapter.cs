using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using gestadh45.dal;
using gestadh45.model;
using gestadh45.services.Reporting.Templates;

namespace gestadh45.business.ServicesAdapters
{
	public static class ServiceReportingAdapter
	{
		public static ReportInventaireEquipementSimple EquipementToReportInventaireEquipementSimple(Equipement equipement) {
			return new ReportInventaireEquipementSimple()
			{
				Numero = equipement.Numero,
				Categorie = equipement.Modele.Categorie.Libelle,
				Modele = equipement.Modele.LibelleCourt,
				Marque = equipement.Modele.Marque.Libelle,
				DateAchat = (equipement.DateAchat.HasValue ? equipement.DateAchat.Value.ToShortDateString() : equipement.DateCreation.ToShortDateString()),
				Localisation = equipement.Localisation.Libelle
			};
		}
		
		public static ICollection<ReportInventaireEquipementSimple> EquipementsToReportInventaireEquipementSimple(ICollection<Equipement> equipements) {
			var result = new List<ReportInventaireEquipementSimple>();

			foreach (var equip in equipements) {
				result.Add(EquipementToReportInventaireEquipementSimple(equip));
			}

			return result;
		}

		public static ICollection<ReportInventaireEquipementComplet> EquipementsToReportInventaireEquipementComplet(ICollection<Equipement> equipements) {
			var result = new List<ReportInventaireEquipementComplet>();

			foreach (var equip in equipements) {
				var item = new ReportInventaireEquipementComplet()
				{
					Numero = equip.Numero,
					Categorie = equip.Modele.Categorie.Libelle,
					Modele = equip.Modele.LibelleCourt,
					Marque = equip.Modele.Marque.Libelle,
					DateAchat = (equip.DateAchat.HasValue ? equip.DateAchat.Value.ToShortDateString() : equip.DateCreation.ToShortDateString()),
					Localisation = equip.Localisation.Libelle
				};

				// on se base sur la dernière vérification validée
				var lastVerif = equip.Verifications.OrderByDescending(v => v.CampagneVerification.Date).FirstOrDefault(v => v.CampagneVerification.EstValidee);

				item.DateDerniereVerification = (lastVerif != null) ? lastVerif.CampagneVerification.Date.ToShortDateString() : string.Empty;
				item.StatutDerniereVerification = (lastVerif != null) ? lastVerif.StatutVerification.ToString() : string.Empty;

				result.Add(item);
			}

			return result;
		}

		public static ICollection<ReportVerificationEquipement> CampagneVerificationToReportVerificationEquipement(CampagneVerification campagne) {
			var result = new List<ReportVerificationEquipement>();

			foreach (var verif in campagne.Verifications) {
				var item = new ReportVerificationEquipement()
				{
					Categorie = verif.Equipement.Modele.Categorie.Libelle,
					Marque = verif.Equipement.Modele.Marque.Libelle,
					Modele = verif.Equipement.Modele.LibelleCourt,
					Numero = verif.Equipement.Numero,
					StatutVerification = verif.StatutVerification.ToString(),
					RaisonStatutVerification = verif.Commentaire
				};

				result.Add(item);
			}

			return result;
		}

		public static ICollection<ReportListeAdherents> InscriptionsToListeAdherents(ICollection<Inscription> inscriptions) {
			var result = new List<ReportListeAdherents>();

			foreach (var ins in inscriptions) {
				var item = new ReportListeAdherents()
				{
					Nom = ins.Adherent.Nom,
					Prenom = ins.Adherent.Prenom,
					DateNaissance = ins.Adherent.DateNaissance.ToShortDateString(),
					Telephone = ins.Adherent.Telephone1,
					Email = ins.Adherent.Mail1,
					Section = ins.Section.Libelle,
					Groupe = ins.Groupe.Libelle
				};

				result.Add(item);
			}

			return result;
		}

		public static ICollection<ReportListeAdherents> GroupeToReportListeAdherents(Groupe groupe) {
			return InscriptionsToListeAdherents(
				groupe.Inscriptions.Where(i => i.StatutInscription != StatutInscription.Annulee).ToList()
			);
		}

		public static ICollection<ReportRepartitionAdherentsAge> InscriptionsToReportRepartitionAdherentsAge(ICollection<TrancheAge> tranchesAge, Ville villeResident, ICollection<Inscription> inscriptions) {
			var result = new List<ReportRepartitionAdherentsAge>();

			foreach (var tranche in tranchesAge) {
				var item = new ReportRepartitionAdherentsAge()
				{
					Libelle = tranche.ToString()
				};

				item.NbHommesResident = inscriptions.Count(i =>
					i.Adherent.Sexe == Sexe.M
					&& i.Adherent.Ville.ID == villeResident.ID
					&& i.Adherent.Age >= tranche.AgeInf
					&& i.Adherent.Age <= tranche.AgeSup
				);

				item.NbFemmesResident = inscriptions.Count(i =>
					i.Adherent.Sexe == Sexe.F
					&& i.Adherent.Ville.ID == villeResident.ID
					&& i.Adherent.Age >= tranche.AgeInf
					&& i.Adherent.Age <= tranche.AgeSup
				);

				item.NbHommesExterieur = inscriptions.Count(i =>
					i.Adherent.Sexe == Sexe.M
					&& i.Adherent.Ville.ID != villeResident.ID
					&& i.Adherent.Age >= tranche.AgeInf
					&& i.Adherent.Age <= tranche.AgeSup
				);

				item.NbFemmesExterieur = inscriptions.Count(i =>
					i.Adherent.Sexe == Sexe.F
					&& i.Adherent.Ville.ID != villeResident.ID
					&& i.Adherent.Age >= tranche.AgeInf
					&& i.Adherent.Age <= tranche.AgeSup
				);

				result.Add(item);
			}

			return result;
		}
		
		public static ICollection<ReportRepartitionAdherentsGroupes> InscriptionsToReportRepartitionAhderentsGroupes(ICollection<Groupe> groupes) {
			var result = new List<ReportRepartitionAdherentsGroupes>();
			
			foreach (var groupe in groupes.OrderBy(x => x.JourSemaine)) {
				result.Add(new ReportRepartitionAdherentsGroupes()
				           {
					           Groupe=groupe.Libelle,
					           NbPlaces = groupe.NbPlaces,
					           NbInscriptions = groupe.NbActifs
				           }
				);
			}
			
			return result;
		}

		public static ICollection<ReportRepartitionAdherentAgeLicenceVille> InscriptionsToReportRepartitionAdherentsAgeLicenceVille(ICollection<TrancheAge> tranchesAge, Ville villeResident, ICollection<Inscription> inscriptions) {
			var result = new List<ReportRepartitionAdherentAgeLicenceVille>();

			foreach (var tranche in tranchesAge) {
				var item = new ReportRepartitionAdherentAgeLicenceVille()
				{
					Libelle = tranche.ToString()
				};

				item.NbResidentNonLicencie = inscriptions.Count(i =>
					!i.EstLicencie
					&& i.Adherent.Ville.ID == villeResident.ID
					&& i.Adherent.Age >= tranche.AgeInf
					&& i.Adherent.Age <= tranche.AgeSup
				);

				item.NbResidentLicencie = inscriptions.Count(i =>
					i.EstLicencie
					&& i.Adherent.Ville.ID == villeResident.ID
					&& i.Adherent.Age >= tranche.AgeInf
					&& i.Adherent.Age <= tranche.AgeSup
				);

				item.NbExterieurNonLicencie = inscriptions.Count(i =>
					!i.EstLicencie
					&& i.Adherent.Ville.ID != villeResident.ID
					&& i.Adherent.Age >= tranche.AgeInf
					&& i.Adherent.Age <= tranche.AgeSup
				);

				item.NbExterieurLicencie = inscriptions.Count(i =>
					i.EstLicencie
					&& i.Adherent.Ville.ID != villeResident.ID
					&& i.Adherent.Age >= tranche.AgeInf
					&& i.Adherent.Age <= tranche.AgeSup
				);

				result.Add(item);
			}

			return result;
		}

		public static ICollection<ReportVieEquipement> EquipementToReportVieEquipement(Equipement equipement) {
			var result = new List<ReportVieEquipement>();

			// création de l'équipement
			result.Add(
				new ReportVieEquipement() { 
					Date = equipement.DateCreation.ToShortDateString(), 
					Evenement = ResReportingAdapter.LibelleCreationEquipement }
			);

			// vérifications (uniquement pour les campagnes validées)
			foreach (var verif in equipement.Verifications.Where(v => v.CampagneVerification.EstValidee).OrderBy(v => v.CampagneVerification.Date)) {
				result.Add(
					new ReportVieEquipement() { 
						Date = verif.CampagneVerification.Date.ToShortDateString(), 
						Evenement = string.Format(
							CultureInfo.CurrentCulture, 
							ResReportingAdapter.LibelleVerificationEquipement, 
							verif.StatutVerification.ToString()
						) 
					}
				);
			}

			return result;
		}

		public static ICollection<ReportVieEquipement> EquipementsToReportVieEquipement(ICollection<Equipement> equipements) {
			var result = new List<ReportVieEquipement>();

			foreach (var equip in equipements) {
				// ligne de séparation
				result.Add(
					new ReportVieEquipement()
					{
						Date = string.Format(CultureInfo.CurrentCulture, ResReportingAdapter.LibelleSeparationEquipement, equip.Numero),
						Evenement = string.Empty
					}
				);

				// lignes de l'équipement
				result.AddRange(EquipementToReportVieEquipement(equip));
			}

			return result;
		}
	}
}
