using System.Collections.Generic;
using System.Globalization;
using LinqDataMap = System.Data.Linq.Mapping;

namespace gestadh45.model
{
	public class Categorie : BaseModel
	{
		#region EF
		[LinqDataMap.Column(DbType="nvarchar(4000)")]
		public string Libelle { get; set; }

		public virtual ICollection<Modele> Modeles { get; set; }
		public virtual DureeDeVie DureeDeVie { get; set; }
		#endregion

		/// <summary>
		/// Returns a <see cref="System.String"/> that represents this instance.
		/// </summary>
		/// <returns>
		/// A <see cref="System.String"/> that represents this instance.
		/// </returns>
		public override string ToString() {
			return string.Format(CultureInfo.CurrentCulture, "{0}", this.Libelle);
		}
	}
}
