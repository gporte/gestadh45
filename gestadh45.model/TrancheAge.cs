using System.Globalization;

namespace gestadh45.model
{
	public class TrancheAge : BaseModel
	{
		#region EF
		public int AgeInf { get; set; }
		public int AgeSup { get; set; }
		#endregion

		/// <summary>
		/// Obtient une description de la tranche d'âge
		/// </summary>
		/// <returns>AgeInf - AgeSup ans</returns>
		public override string ToString() {
			return string.Format(CultureInfo.CurrentCulture, "{0} - {1} ans", this.AgeInf, this.AgeSup);
		}
	}
}
