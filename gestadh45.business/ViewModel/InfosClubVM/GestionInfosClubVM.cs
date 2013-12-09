/*
 * Crée par SharpDevelop.
 * Utilisateur: Guillaume
 * Date: 02/03/2013
 * Heure: 08:46
 * 
 * Pour changer ce modèle utiliser Outils | Options | Codage | Editer les en-têtes standards.
 */
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using gestadh45.business.Enums;
using gestadh45.model;

namespace gestadh45.business.ViewModel.InfosClubVM
{
	/// <summary>
	/// Description of GestionInfosClub.
	/// </summary>
	public class GestionInfosClubVM : GenericGestionVM<InfosClub>
	{
		#region Villes
		private IOrderedEnumerable<Ville> _villes;

		/// <summary>
		/// Obitent/Définit la liste des villes
		/// </summary>
		public IOrderedEnumerable<Ville> Villes {
			get { return this._villes; }
			set {
				if (this._villes != value) {
					this._villes = value;
					this.RaisePropertyChanged(() => this.Villes);
				}
			}
		}
		#endregion
		
		public GestionInfosClubVM() : base() {
			this.UCCode = CodesUC.GestionInfosClub;
		}
		
		#region methods override
		/// <summary>
		/// Ici pas besoin de récupérer une liste d'infos club, on prend juste le premier résultat
		/// </summary>
		/// <param name="filter">null</param>
		protected override void PopulateItems()	{
			this.SelectedItem = this.Context.InfosClub.FirstOrDefault();
		}
		
		protected override void PopulateSpecificDatas()	{
			this.Villes = this.Context.Villes.ToList().OrderBy(v => v.Libelle);
		}
		
		protected override bool ValidateItem(InfosClub item, Collection<string> errors) {
			if (string.IsNullOrWhiteSpace(item.Nom)) {
				errors.Add(ResInfosClub.ErrNomObligatoire);
			}

			if (item.Adresse == null || string.IsNullOrWhiteSpace(item.Adresse)) {
				errors.Add(ResInfosClub.ErrAdresseObligatoire);
			}

			if (item.Adresse != null && item.Ville == null) {
				errors.Add(ResInfosClub.ErrVilleObligatoire);
			}

			return errors.Count == 0;
		}
		
		protected override void FormatValues(InfosClub item) {
			item.Nom = (item.Nom == null) ? null : item.Nom.ToUpperInvariant();
		}
		#endregion
	}
}
