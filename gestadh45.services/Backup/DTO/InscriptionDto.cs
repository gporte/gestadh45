using System;

namespace gestadh45.services.Backup.DTO
{
	[Serializable()]
	public class InscriptionDto
	{
		public string Adherent { get; set; }
		public string Groupe { get; set; }
		public decimal Cotisation { get; set; }
		public string Commentaire { get; set; }
		public string StatutInscription { get; set; }
		public string CertificatMedicalRemis { get; set; }
		public string NumLicence { get; set; }
		public decimal MontantLicence { get; set; }
	}
}
