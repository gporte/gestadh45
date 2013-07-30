using System.Collections.Generic;
using System.Globalization;

namespace gestadh45.model
{
	public class Saison : BaseModel
	{
		#region EF
		public bool EstSaisonCourante { get; set; }
		public int AnneeDebut { get; set; }
		public int AnneeFin { get; set; }

		public virtual ICollection<Groupe> Groupes { get; set; }
		#endregion

		/// <summary>
		/// Obtient une description courte de la saison
		/// </summary>
		/// <returns>AnneeDebut - AnneeFin</returns>
		public string ToShortString() {
			return string.Format(CultureInfo.CurrentCulture, "{0} - {1}", this.AnneeDebut, this.AnneeFin);
		}

		/// <summary>
		/// Obtient une description complète de la saison
		/// </summary>
		/// <returns>AnneeDebut - AnneeFin [(courante)]</returns>
		public override string ToString() {
			if (this.EstSaisonCourante)
			{
				return string.Format(CultureInfo.CurrentCulture, "{0} - {1} (courante)", this.AnneeDebut, this.AnneeFin);
			}
			return string.Format(CultureInfo.CurrentCulture, "{0} - {1}", this.AnneeDebut, this.AnneeFin);
		}
	}
}
