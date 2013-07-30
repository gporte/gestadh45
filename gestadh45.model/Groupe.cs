using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using LinqDataMap = System.Data.Linq.Mapping;
using System.Reflection;
using System.Resources;

namespace gestadh45.model
{
	public enum JourSemaine
	{
		Lundi,
		Mardi,
		Mercredi,
		Jeudi,
		Vendredi,
		Samedi,
		Dimanche
	}
	
	public class Groupe : BaseModel
	{
		#region EF
		[LinqDataMap.Column(DbType="nvarchar(4000)")]
		public string Libelle { get; set; }
		
		public int NbPlaces { get; set; }
		
		[LinqDataMap.Column(DbType="nvarchar(4000)")]
		public string Commentaire { get; set; }
		
		public System.DateTime HeureDebut { get; set; }
		
		public System.DateTime HeureFin { get; set; }

		public JourSemaine JourSemaine { get; set; }

		public virtual ICollection<Inscription> Inscriptions { get; set; }		
		public virtual Saison Saison { get; set; }
		#endregion

		/// <summary>
		/// Affiche la description du groupe
		/// </summary>
		/// <returns>JourSemaine (HeureDebut - HeureFin)</returns>
		public override string ToString() {
			return string.Format(
				CultureInfo.CurrentCulture,
				"{0} ({1} - {2})",
				this.JourSemaine.ToString(),
				this.HeureDebut.ToString("t", CultureInfo.CurrentCulture),
				this.HeureFin.ToString("t", CultureInfo.CurrentCulture)
			);
		}

		/// <summary>
		/// Gets the nb places libres.
		/// </summary>
		[NotMapped]
		public int NbPlacesLibres
		{
			get
			{
				return this.NbPlaces - (this.Inscriptions != null ? this.Inscriptions.Count : 0);
			}
		}

		/// <summary>
		/// Obtient le nombre d'inscrits
		/// </summary>
		[NotMapped]
		public int NbInscrits
		{
			get
			{
				return this.Inscriptions != null ? this.Inscriptions.Count : 0;
			}
		}

		/// <summary>
		/// Obtient le libellé du jour
		/// </summary>
		[NotMapped]
		public string LibelleJour {
			get {
				return GetLibelleJourSemaine(this.JourSemaine);
			}
		}

		/// <summary>
		/// Renvoit le libellé associé à la valeur de l'énumération JourSemaine
		/// </summary>
		/// <param name="statut">JourSemaine</param>
		/// <returns>Libellé</returns>
		public static string GetLibelleJourSemaine(JourSemaine jour) {
			var rm = new ResourceManager("gestadh45.model.ResEnums", Assembly.GetExecutingAssembly());

			return rm.GetString("JourSemaine_" + jour.ToString()) ?? ResEnums.LibelleAbsent;
		}
	}
}
