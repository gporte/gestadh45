using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;

using LinqDataMap = System.Data.Linq.Mapping;

namespace gestadh45.model
{
	public enum Sexe
	{
		Masculin,
		Feminin
	}
	
	public class Adherent : BaseModel, ICloneable
	{
		#region EF
		[LinqDataMap.Column(DbType="nvarchar(4000)")]
		public string Nom { get; set; }
		
		[LinqDataMap.Column(DbType="nvarchar(4000)")]
		public string Prenom { get; set; }
		
		public DateTime DateNaissance { get; set; }
		
		public DateTime DateCreation { get; set; }
		
		public DateTime DateModification { get; set; }
		
		[LinqDataMap.Column(DbType="nvarchar(4000)")]
		public string Commentaire { get; set; }
		
		[LinqDataMap.Column(DbType="nvarchar(4000)")]
		public string Adresse { get; set; }
		
		[LinqDataMap.Column(DbType="nvarchar(4000)")]
		public string Telephone1 { get; set; }
		
		[LinqDataMap.Column(DbType="nvarchar(4000)")]
		public string Telephone2 { get; set; }
		
		[LinqDataMap.Column(DbType="nvarchar(4000)")]
		public string Telephone3 { get; set; }
		
		[LinqDataMap.Column(DbType="nvarchar(4000)")]
		public string Mail1 { get; set; }
		
		[LinqDataMap.Column(DbType="nvarchar(4000)")]
		public string Mail2 { get; set; }
		
		[LinqDataMap.Column(DbType="nvarchar(4000)")]
		public string Mail3 { get; set; }

		public Sexe Sexe { get; set; }

		public virtual Ville Ville { get; set; }
		public virtual ICollection<Inscription> Inscriptions { get; set; }
		#endregion

		/// <summary>
		/// Obtient l'âge de l'adhérent (calculé à partir de sa date de naissance)
		/// </summary>
		[NotMapped]
		public int Age
		{
			get { return this.CalculerAge(); }
		}

		/// <summary>
		/// Obtient le libellé du sexe
		/// </summary>
		[NotMapped]
		public string LibelleSexe {
			get {
				return GetLibelleSexe(this.Sexe);
			}
		}

		/// <summary>
		/// Affiche la description de l'adhérent
		/// </summary>
		/// <returns>Nom Prenom</returns>
		public override string ToString() {
			return string.Format(CultureInfo.CurrentCulture, "{0} {1}", this.Nom, this.Prenom);
		}

		/// <summary>
		/// Calcule l'age de l'adherent à partir de sa date de naissance
		/// </summary>
		/// <returns>Age de l'adhérent</returns>
		private int CalculerAge() {
			int num = DateTime.Now.Year - this.DateNaissance.Year;
			DateTime time = new DateTime(DateTime.Now.Year, this.DateNaissance.Month, this.DateNaissance.Day);
			if (time > DateTime.Now)
			{
				num--;
			}
			return num;
		}

		/// <summary>
		/// Renvoit le libellé associé à la valeur de l'énumération Sexe
		/// </summary>
		/// <param name="statut">Sexe</param>
		/// <returns>Libellé</returns>
		public static string GetLibelleSexe(Sexe sexe) {
			var rm = new ResourceManager("gestadh45.model.ResEnums", Assembly.GetExecutingAssembly());

			return rm.GetString("Sexe_" + sexe.ToString()) ?? ResEnums.LibelleAbsent;
		}

		/// <summary>
		/// Génère une chaîne contenant l'ensemble des emails renseignés, concaténés avec le séparateur spécifié en paramètres
		/// </summary>
		/// <param name="separator">Séparateur à utiliser</param>
		/// <returns>Chaîne des emails concaténés</returns>
		private string GetChaineMails(string separator) {
			string[] emails = {this.Mail1, this.Mail2, this.Mail3};
			
			return emails.Aggregate(
				(x, y) => string.IsNullOrWhiteSpace(y) ? x : string.Concat(x, separator, y)
			);
		}
		
		#region ICloneable Membres

		public object Clone()
		{
			return new Adherent()
			{
				ID = Guid.NewGuid(),

				Nom = string.Copy(this.Nom),
				Prenom = string.Copy(this.Prenom),
				DateNaissance = this.DateNaissance,
				Sexe = this.Sexe,
				Adresse = string.Copy(this.Adresse),
				Ville = this.Ville,

				Telephone1 = string.Copy(this.Telephone1 ?? string.Empty),
				Telephone2 = string.Copy(this.Telephone2 ?? string.Empty),
				Telephone3 = string.Copy(this.Telephone3 ?? string.Empty),

				Mail1 = string.Copy(this.Mail1 ?? string.Empty),
				Mail2 = string.Copy(this.Mail2 ?? string.Empty),
				Mail3 = string.Copy(this.Mail3 ?? string.Empty),

				Commentaire = string.Copy(this.Commentaire ?? string.Empty),

				DateCreation = DateTime.Now,
				DateModification = DateTime.Now
			};
		}

		#endregion
	}
}
