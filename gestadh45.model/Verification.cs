using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using LinqDataMap = System.Data.Linq.Mapping;
using System.Reflection;
using System.Resources;

namespace gestadh45.model
{
	public enum StatutVerification
	{
		Ok,
		AVerifier,
		Rebut
	}
	
	public class Verification : BaseModel
	{
		#region EF
		[LinqDataMap.Column(DbType="nvarchar(4000)")]
		public string Commentaire { get; set; }
		
		public StatutVerification StatutVerification { get; set; }

		public virtual CampagneVerification CampagneVerification { get; set; }
		public virtual Equipement Equipement { get; set; }
		#endregion

		public override string ToString() {
			return string.Format(CultureInfo.CurrentCulture, "{0} : {1}", this.CampagneVerification.Date.ToShortDateString(), this.Equipement.ToString());
		}

		/// <summary>
		/// Obtient le libellé du statut vérification
		/// </summary>
		[NotMapped]
		public string LibelleStatut {
			get {
				return GetLibelleStatut(this.StatutVerification);
			}
		}

		/// <summary>
		/// Renvoit le libellé associé à la valeur de l'énumération StatutVerification
		/// </summary>
		/// <param name="statut">StatutVerification</param>
		/// <returns>Libellé</returns>
		public static string GetLibelleStatut(StatutVerification statut) {
			var rm = new ResourceManager("gestadh45.model.ResEnums", Assembly.GetExecutingAssembly());

			return rm.GetString("StatutVerification_" + statut.ToString()) ?? ResEnums.LibelleAbsent;
		}
	}
}
