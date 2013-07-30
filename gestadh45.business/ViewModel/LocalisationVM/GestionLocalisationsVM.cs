/*
 * Crée par SharpDevelop.
 * Utilisateur: Guillaume
 * Date: 03/03/2013
 * Heure: 10:52
 * 
 * Pour changer ce modèle utiliser Outils | Options | Codage | Editer les en-têtes standards.
 */
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using gestadh45.business.Enums;
using gestadh45.model;

namespace gestadh45.business.ViewModel.LocalisationVM
{
	/// <summary>
	/// Description of GestionLocalisationsVM.
	/// </summary>
	public class GestionLocalisationsVM : GenericGestionVM<Localisation>
	{
		public GestionLocalisationsVM(string userConnectionString) : base(userConnectionString)	{
			this.UCCode = CodesUC.GestionLocalisations;
		}
		
		#region methods override
		protected override Localisation CreateDefaultItem() {
			return new Localisation()
			{
				Libelle = ResLocalisation.Defaut_Libelle
			};
		}
		
		protected override bool ValidateItem(Localisation item, Collection<string> errors) {
			if (string.IsNullOrWhiteSpace(item.Libelle)) {
				errors.Add(ResLocalisation.ErrLibelleObligatoire);
			}

			if (errors.Count == 0 && this.ItemExists(item)) {
				errors.Add(ResLocalisation.ErrLocalisationExiste);
			}

			return errors.Count == 0;
		}
		
		protected override bool ItemExists(Localisation item) {
			return this.Context.Localisations.Where(l => 
			                                         l.Libelle.Equals(item.Libelle)
			                                    	 && l.ID != item.ID
			                                    	).Count() != 0;
		}
		
		/// <summary>
		/// Vérifie si une localisation est utilisée
		/// </summary>
		/// <param name="item">Localisation à vérifier</param>
		/// <returns>True si la localisation est utilisée, False sinon</returns>
		protected override bool ItemIsUsed(Localisation item) {
			return item.Equipements != null && item.Equipements.Count > 0;
		}
		
		protected override void FormatValues(Localisation item) {
			item.Libelle = (item.Libelle == null) ? null : item.Libelle.ToUpperInvariant();
		}
		#endregion
	}
}
