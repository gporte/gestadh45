using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using LinqDataMap = System.Data.Linq.Mapping;

namespace gestadh45.model
{
	public class InfosClub : BaseModel
	{
		#region EF
		[LinqDataMap.Column(DbType="nvarchar(4000)")]
		public string Nom { get; set; }
		
		[LinqDataMap.Column(DbType="nvarchar(4000)")]
		public string Numero { get; set; }
		
		[LinqDataMap.Column(DbType="nvarchar(4000)")]
		public string Siren { get; set; }
		
		[LinqDataMap.Column(DbType="nvarchar(4000)")]
		public string NIC { get; set; }
		
		[LinqDataMap.Column(DbType="nvarchar(4000)")]
		public string Adresse { get; set; }
		
		[LinqDataMap.Column(DbType="nvarchar(4000)")]
		public string Telephone { get; set; }
		
		[LinqDataMap.Column(DbType="nvarchar(4000)")]
		public string Mail { get; set; }
		
		[LinqDataMap.Column(DbType="nvarchar(4000)")]
		public string SiteWeb { get; set; }
		
		[LinqDataMap.Column(DbType="nvarchar(4000)")]
		public string NumAPS { get; set; }
		
		[LinqDataMap.Column(DbType="nvarchar(4000)")]
		public string NumFederation { get; set; }

		public virtual Ville Ville { get; set; }
		#endregion

		/// <summary>
		/// Obtient le code SIRET (SIREN - NIC)
		/// </summary>
		[NotMapped]
		public string Siret
		{
			get
			{
				return string.Format(CultureInfo.CurrentCulture, "{0} - {1}", this.Siren, this.NIC);
			}
		}
		
		public override string ToString()
		{
			return string.Format(CultureInfo.CurrentCulture, "{0}", this.Nom);
		}

	}
}
