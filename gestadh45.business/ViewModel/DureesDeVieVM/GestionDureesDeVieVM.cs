/*
 * Crée par SharpDevelop.
 * Utilisateur: Guillaume
 * Date: 03/03/2013
 * Heure: 09:19
 * 
 * Pour changer ce modèle utiliser Outils | Options | Codage | Editer les en-têtes standards.
 */
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Collections.Generic;
using gestadh45.business.Enums;
using gestadh45.model;

namespace gestadh45.business.ViewModel.DureesDeVieVM
{
	/// <summary>
	/// Description of GestionDureesDeVieVM.
	/// </summary>
	public class GestionDureesDeVieVM : GenericGestionVM<DureeDeVie>
	{
		public GestionDureesDeVieVM(string userConnectionString) : base(userConnectionString) {
			this.UCCode = CodesUC.GestionDureesDeVie;
		}
		
		#region methods override
		protected override DureeDeVie CreateDefaultItem() {
			return new DureeDeVie()
			{
				Libelle = ResDureesDeVie.Defaut_Libelle,
				NbAnnees = 0,
				NbMois = 0
			};
		}
		
		protected override bool ValidateItem(DureeDeVie item, Collection<string> errors) {
			if (string.IsNullOrWhiteSpace(item.Libelle)) {
				errors.Add(ResDureesDeVie.ErreurLibelleObligatoire);
			}

			if (errors.Count == 0 && this.ItemExists(item)) {
				errors.Add(ResDureesDeVie.ErreurDureeDeVieExiste);
			}

			return errors.Count == 0;
		}
		
		protected override bool ItemExists(DureeDeVie item) {
			return this.Context.DureeDeVies.Where(d => 
			                                    d.Libelle.Equals(item.Libelle)
			                                    && d.ID != item.ID
			                                   ).Count() != 0;
		}
		
		/// <summary>
		/// Vérifie si une durée de vie est utilisée
		/// </summary>
		/// <param name="item">durée de vie à vérifier</param>
		/// <returns>True si la durée de vie est utilisée, False sinon</returns>
		protected override bool ItemIsUsed(DureeDeVie item) {
			return item.Categories != null && item.Categories.Count > 0;
		}
		
		protected override void FormatValues(DureeDeVie item) {
			item.Libelle = (item.Libelle == null) ? null : item.Libelle.ToUpperInvariant();
		}
		#endregion
	}
}
