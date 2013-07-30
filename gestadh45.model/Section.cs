using System.Collections.Generic;
using System.Globalization;
using LinqDataMap = System.Data.Linq.Mapping;

namespace gestadh45.model
{
	public class Section : BaseModel
	{
		#region EF
		[LinqDataMap.Column(DbType="nvarchar(4000)")]
		public string Libelle { get; set; }
		
		public bool EstDefaut { get; set; }

		public virtual ICollection<Inscription> Inscriptions { get; set; }
		#endregion

		public override string ToString() {
			return string.Format(CultureInfo.CurrentCulture, "{0}", this.Libelle);
		}
	}
}
