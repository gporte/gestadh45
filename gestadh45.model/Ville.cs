using System.Collections.Generic;
using System.Globalization;
using LinqDataMap = System.Data.Linq.Mapping;

namespace gestadh45.model
{
	public class Ville : BaseModel
	{
		#region EF
		[LinqDataMap.Column(DbType="nvarchar(4000)")]
		public string Libelle { get; set; }
		
		[LinqDataMap.Column(DbType="nvarchar(4000)")]
		public string CodePostal { get; set; }

		public virtual ICollection<Adherent> Adherents { get; set; }
		public virtual ICollection<InfosClub> InfosClubs { get; set; }
		#endregion

		/// <summary>
		/// Obtient une description de la ville
		/// </summary>
		/// <returns>CodePostal - Libelle</returns>
		public override string ToString() {
			return string.Format(CultureInfo.CurrentCulture, "{0} - {1}", this.CodePostal, this.Libelle);
		}
	}
}
