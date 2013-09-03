﻿/*
 * Crée par SharpDevelop.
 * Utilisateur: Guillaume
 * Date: 02/03/2013
 * Heure: 20:47
 * 
 * Pour changer ce modèle utiliser Outils | Options | Codage | Editer les en-têtes standards.
 */
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Collections.Generic;
using gestadh45.business.Enums;
using gestadh45.model;

namespace gestadh45.business.ViewModel.MarquesVM
{
	/// <summary>
	/// Description of GestionMarquesVM.
	/// </summary>
	public class GestionMarquesVM : GenericGestionVM<Marque>
	{
		public GestionMarquesVM(string userConnectionString) : base(userConnectionString) {
			this.UCCode = CodesUC.GestionMarques;
		}
		
		#region methods override
		protected override Marque CreateDefaultItem() {
			return new Marque()
			{
				Libelle = ResMarques.Defaut_Libelle
			};
		}
		
		protected override bool ValidateItem(Marque item, Collection<string> errors) {
			if (string.IsNullOrWhiteSpace(item.Libelle)) {
				errors.Add(ResMarques.ErrLibelleObligatoire);
			}

			if (errors.Count == 0 && this.ItemExists(item)) {
				errors.Add(ResMarques.ErrMarqueExiste);
			}

			return errors.Count == 0;
		}
		
		protected override bool ItemExists(Marque item) {
			return this.Context.Marques.Where(m => 
			                                    m.Libelle.Equals(item.Libelle)
			                                    && m.ID != item.ID
			                                   ).Count() != 0;
		}
		
		/// <summary>
		/// Vérifie si une marque est utilisée
		/// </summary>
		/// <param name="item">Marque à vérifier</param>
		/// <returns>True si la marque est utilisée, False sinon</returns>
		protected override bool ItemIsUsed(Marque item) {
			return item.Modeles != null && item.Modeles.Count > 0;
		}
		
		protected override void FormatValues(Marque item) {
			item.Libelle = (item.Libelle == null) ? null : item.Libelle.ToUpperInvariant();
		}
		#endregion
	}
}