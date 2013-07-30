using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;
using LinqDataMap = System.Data.Linq.Mapping;

namespace gestadh45.model
{
	public class Equipement : BaseModel
	{
		#region EF
		[LinqDataMap.Column(DbType="nvarchar(4000)")]
		public string Numero { get; set; }
		
		public DateTime? DateAchat { get; set; }
		
		public DateTime DateCreation { get; set; }
		
		public DateTime DateModification { get; set; }
		
		[LinqDataMap.Column(DbType="nvarchar(4000)")]
		public string Commentaire { get; set; }

		public virtual ICollection<Verification> Verifications { get; set; }
		public virtual Modele Modele { get; set; }
		public virtual Localisation Localisation { get; set; }
		#endregion

		/// <summary>
		/// Returns a <see cref="System.String"/> that represents this instance.
		/// </summary>
		/// <returns>
		/// A <see cref="System.String"/> that represents this instance.
		/// </returns>
		public override string ToString() {
			return string.Format(CultureInfo.CurrentCulture, "{0} - {1}", this.Numero, this.Modele.ToString());
		}

		/// <summary>
		/// Obtient la date de fin de vie de l'équipement en se basant (dans l'ordre) soit sur sa date d'achat, soit sur sa date demise en service, soit sur sa date de saisie dans la BDD.
		/// </summary>
		[NotMapped]
		public DateTime DateFinDeVie {
			get {
				DateTime dateFinDeVie = DateTime.Now;

				if (this.DateAchat.HasValue) {
					dateFinDeVie = this.DateAchat.Value.AddYears(this.Modele.Categorie.DureeDeVie.NbAnnees).AddMonths(this.Modele.Categorie.DureeDeVie.NbMois);
				}
				else {
					dateFinDeVie = this.DateCreation.AddYears(this.Modele.Categorie.DureeDeVie.NbAnnees).AddMonths(this.Modele.Categorie.DureeDeVie.NbMois);
				}

				return dateFinDeVie;
			}
		}

		/// <summary>
		/// Obtient un booléen indiquant si l'équipement a atteint sa fin de vie
		/// </summary>
		[NotMapped]
		public bool FinDeVieAtteinte {
			get {
				return DateTime.Now > this.DateFinDeVie;
			}
		}

		/// <summary>
		/// Obtient un booléen indiquant si l'équipement est au rebut
		/// </summary>
		/// <value>
		///   <c>true</c> si l'équipement est au rebut, <c>false</c> sinon.
		/// </value>
		/// <remarks>Un équipement est au rebut si il a au moins une vérification validée et que sa dernière vérification validée à le statut rebut</remarks>
		[NotMapped]
		public bool EstAuRebut {
			get {
				return this.Verifications.Count(v => v.CampagneVerification.EstValidee) > 0
					&& this.Verifications
					.OrderByDescending(v => v.CampagneVerification.Date)
					.First(v => v.CampagneVerification.EstValidee)
					.StatutVerification == StatutVerification.Rebut;
			}
		}

		/// <summary>
		/// Obtient un booléen indiquant si les caractéristiques de l'équipement sont éditables
		/// </summary>
		[NotMapped]
		public bool EstEditableCaracteristiques {
			get { return this.Verifications == null || this.Verifications.Count == 0; }
		}

		/// <summary>
		/// Crée un nouvel objet qui est une copie de l'instance en cours.
		/// </summary>
		/// <returns>
		/// Nouvel objet qui est une copie de cette instance.
		/// </returns>
		public object Clone() {
			return new Equipement()
			{
				ID = Guid.NewGuid(),
				Numero = string.Copy(this.Numero),
				Modele = this.Modele,
				DateCreation = DateTime.Now,
				DateModification = DateTime.Now,
				DateAchat = this.DateAchat,
				Commentaire = this.Commentaire,
			};
		}
	}
}
