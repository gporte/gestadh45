using System.Collections.Generic;
using System.Globalization;
using LinqDataMap = System.Data.Linq.Mapping;

namespace gestadh45.model
{
	public class DureeDeVie : BaseModel
	{
		#region EF
		[LinqDataMap.Column(DbType="nvarchar(4000)")]
		public string Libelle { get; set; }
		
		public int NbAnnees { get; set; }
		
		public int NbMois { get; set; }

		public virtual ICollection<Categorie> Categories { get; set; }
		#endregion

		public override string ToString() {
			return string.Format(CultureInfo.CurrentCulture, "{0} ({1} ans, {2} mois)", this.Libelle, this.NbAnnees, this.NbMois);
		}
	}
}
