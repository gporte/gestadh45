using gestadh45.model;
using gestadh45.services.Backup.DTO;
using System.Collections.Generic;

namespace gestadh45.business.ServicesAdapters
{
	public static class ServiceBackupAdapter
	{
		#region AdherentDto
		public static AdherentDto AdherentToAdherentDto(Adherent adh) {
			return new AdherentDto()
			{
				Nom = adh.Nom,
				Prenom = adh.Prenom,
				DateNaissance = adh.DateNaissance.ToShortDateString(),
				Sexe = adh.Sexe.ToString(),
				Adresse = adh.Adresse,
				CodePostal = adh.Ville.CodePostal,
				Ville = adh.Ville.Libelle,
				Telephone1 = adh.Telephone1,
				Telephone2 = adh.Telephone2,
				Telephone3 = adh.Telephone3,
				Mail1 = adh.Mail1,
				Mail2 = adh.Mail2,
				Mail3 = adh.Mail3,
				Commentaire = adh.Commentaire
			};
		}

		public static AdherentDto[] AdherentsToAdherentDtoArray(IEnumerable<Adherent> adherents) {
			var list = new List<AdherentDto>();

			foreach (var adh in adherents) {
				list.Add(AdherentToAdherentDto(adh));
			}

			return list.ToArray();
		}
		#endregion

		#region InscriptionDto
		public static InscriptionDto InscriptionToInscriptionDto(Inscription ins) {
			return new InscriptionDto()
			{
				Adherent = ins.Adherent.ToString(),
				Groupe = ins.Groupe.ToString(),
				CertificatMedicalRemis = (ins.CertificatMedicalRemis) ? ResBackupAdapter.LibelleOui : ResBackupAdapter.LibelleNon,
				Commentaire = ins.Commentaire,
				Cotisation = ins.Cotisation,
				StatutInscription = ins.StatutInscription.ToString(),
				NumLicence = ins.NumLicence,
				MontantLicence = ins.MontantLicence
			};
		}

		public static InscriptionDto[] InscriptionsToInscriptionDtos(IEnumerable<Inscription> inscriptions) {
			var list = new List<InscriptionDto>();

			foreach (var ins in inscriptions) {
				list.Add(InscriptionToInscriptionDto(ins));
			}

			return list.ToArray();
		}
		#endregion

		#region VilleDto
		public static VilleDto VilleToVilleDto(Ville ville) {
			return new VilleDto()
			{
				Libelle = ville.Libelle,
				CodePostal = ville.CodePostal
			};
		}

		public static VilleDto[] VillesToVilleDtos(IEnumerable<Ville> villes) {
			var list = new List<VilleDto>();

			foreach (var ville in villes) {
				list.Add(VilleToVilleDto(ville));
			}

			return list.ToArray();
		}
		#endregion
	}
}

