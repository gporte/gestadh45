using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Reflection;
using System.Resources;
using LinqDataMap = System.Data.Linq.Mapping;

namespace gestadh45.model
{
	public enum StatutInscription
	{
		ASuivre,
		Validee,
		Annulee
	}
	
	public class Inscription : BaseModel
	{
		#region EF
		public decimal Cotisation { get; set; }
		
		public DateTime DateCreation { get; set; }
		
		public DateTime DateModification { get; set; }
		
		[LinqDataMap.Column(DbType="nvarchar(4000)")]
		public string Commentaire { get; set; }
		
		public bool CertificatMedicalRemis { get; set; }
		
		[LinqDataMap.Column(DbType="nvarchar(4000)")]
		public string NumLicence { get; set; }
		
		public decimal MontantLicence { get; set; }

		public StatutInscription StatutInscription { get; set; }

		public virtual Adherent Adherent { get; set; }
		public virtual Groupe Groupe { get; set; }		
		public virtual Section Section { get; set; }
		#endregion

		/// <summary>
		/// Obtient la description de l'inscription
		/// </summary>
		/// <returns>LibelleGroupe - Adherent</returns>
		public override string ToString() {
			return string.Format(CultureInfo.CurrentCulture, "{0} - {1}", this.Groupe.Libelle, this.Adherent);
		}

		/// <summary>
		/// Obtient un booléen indiquant si l'adhérent est licencié pour la saison courante
		/// </summary>
		[NotMapped]
		public bool EstLicencie
		{
			get { return !string.IsNullOrWhiteSpace(this.NumLicence); }
		}

		/// <summary>
		/// Obtient le montant total payé (cotisation + licence)
		/// </summary>
		[NotMapped]
		public decimal TotalPaiement
		{
			get { return this.Cotisation + this.MontantLicence; }
		}

		/// <summary>
		/// Obtient le libellé du statut inscription
		/// </summary>
		[NotMapped]
		public string LibelleStatut {
			get {
				return GetLibelleStatut(this.StatutInscription);
			}
		}

		/// <summary>
		/// Renvoit le libellé associé à la valeur de l'énumération StatutInscription
		/// </summary>
		/// <param name="statut">StatutInscription</param>
		/// <returns>Libellé</returns>
		public static string GetLibelleStatut(StatutInscription statut) {
			var rm = new ResourceManager("gestadh45.model.ResEnums", Assembly.GetExecutingAssembly());

			return rm.GetString("StatutInscription_" + statut.ToString()) ?? ResEnums.LibelleAbsent;
		}
	}
}
