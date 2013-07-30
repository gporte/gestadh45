using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using LinqDataMap = System.Data.Linq.Mapping;

namespace gestadh45.model
{
	public class Modele : BaseModel
	{
		#region EF
		[LinqDataMap.Column(DbType="nvarchar(4000)")]
		public string Nom { get; set; }
		
		[LinqDataMap.Column(DbType="nvarchar(4000)")]
		public string Couleur1 { get; set; }
		
		[LinqDataMap.Column(DbType="nvarchar(4000)")]
		public string Couleur2 { get; set; }
		
		[LinqDataMap.Column(DbType="nvarchar(4000)")]
		public string Couleur3 { get; set; }

		public virtual Categorie Categorie { get; set; }
		public virtual ICollection<Equipement> Equipements { get; set; }
		public virtual Marque Marque { get; set; }
		#endregion

		/// <summary>
		/// [Categorie] [Marque] [Nom] [Couleurs]
		/// </summary>
		/// <returns></returns>
		public override string ToString() {
			return string.Format(CultureInfo.CurrentCulture, "{0} {1} {2} {3}", this.Categorie.ToString(), this.Marque.ToString(), this.Nom, this.DescriptionCouleur);
		}

		/// <summary>
		/// [Nom] [Couleurs]
		/// </summary>
		[NotMapped]
		public string LibelleCourt {
			get { return string.Format(CultureInfo.CurrentCulture, "{0} {1}", this.Nom, this.DescriptionCouleur); }
		}

		/// <summary>
		/// Obtient la liste des couleurs concaténées
		/// </summary>
		[NotMapped]
		public string DescriptionCouleur {
			get { return this.ConcatColors(); }
		}

		private string ConcatColors() {
			var couleurs = new List<string>();

			if (!string.IsNullOrWhiteSpace(this.Couleur1)) {
				couleurs.Add(this.Couleur1);
			}

			if (!string.IsNullOrWhiteSpace(this.Couleur2)) {
				couleurs.Add(this.Couleur2);
			}

			if (!string.IsNullOrWhiteSpace(this.Couleur3)) {
				couleurs.Add(this.Couleur3);
			}

			return string.Join("/", couleurs);
		}
	}
}
