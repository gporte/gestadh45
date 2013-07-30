using System;

namespace gestadh45.services.Backup.DTO
{
	[Serializable()]
	public class AdherentDto
	{
		public string Nom { get; set; }
		public string Prenom { get; set; }
		public string DateNaissance { get; set; }
		public string Sexe { get; set; }
		public string Commentaire { get; set; }
		public string Adresse { get; set; }
		public string Ville { get; set; }
		public string CodePostal { get; set; }
		public string Telephone1 { get; set; }
		public string Telephone2 { get; set; }
		public string Telephone3 { get; set; }
		public string Mail1 { get; set; }
		public string Mail2 { get; set; }
		public string Mail3 { get; set; }
	}
}
