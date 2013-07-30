using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using LinqDataMap = System.Data.Linq.Mapping;

namespace gestadh45.model
{
	public class CampagneVerification : BaseModel
	{
		#region EF
		public DateTime Date { get; set; }
		
		[LinqDataMap.Column(DbType="nvarchar(4000)")]
		public string Responsable { get; set; }
		
		public bool EstValidee { get; set; }

		public virtual ICollection<Verification> Verifications { get; set; }
		#endregion

		public override string ToString() {
			return string.Format(CultureInfo.CurrentCulture, "{0} - {1}", this.Date.ToShortDateString(), this.Responsable);
		}

		/// <summary>
		/// Obtient le nombre d'équipements concernés par la campagne
		/// </summary>
		[NotMapped]
		public int NbEquipements {
			get { return this.Verifications.Count; }
		}
	}
}
