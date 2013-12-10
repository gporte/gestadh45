using gestadh45.business.Enums;
using gestadh45.model;
using System.Collections.ObjectModel;
using System.Linq;

namespace gestadh45.business.ViewModel.VillesVM
{
	/// <summary>
	/// Description of GestionVillesVM.
	/// </summary>
	public class GestionVillesVM : GenericGestionVM<Ville>
	{
		public GestionVillesVM() : base() {
			this.UCCode = CodesUC.GestionVilles;
		}
		
		#region methods override
		protected override Ville CreateDefaultItem() {
			return new Ville()
			{
				Libelle = ResVilles.DefautLibelle,
				CodePostal = ResVilles.DefautCodePostal
			};
		}
		
		protected override bool ValidateItem(Ville item, Collection<string> errors) {
			if (string.IsNullOrWhiteSpace(item.Libelle)) {
				errors.Add(ResVilles.ErrLibelleObligatoire);
			}

			if (string.IsNullOrWhiteSpace(item.CodePostal)) {
				errors.Add(ResVilles.ErrCodePostalObligatoire);
			}

			if (errors.Count == 0 && this.ItemExists(item)) {
				errors.Add(ResVilles.ErrVilleExiste);
			}

			return errors.Count == 0;
		}
		
		protected override void FormatValues(Ville item) {
			item.Libelle = (item.Libelle == null) ? null : item.Libelle.ToUpperInvariant();
			item.CodePostal = (item.CodePostal == null) ? null : item.CodePostal.ToUpperInvariant();
		}
		
		/// <summary>
		/// Vérifie si une autre ville portant les mêmes libellés et code postal existe déjà
		/// </summary>
		/// <param name="item">Ville à vérifier</param>
		/// <returns>True si la ville existe déjà, False sinon</returns>
		protected override bool ItemExists(Ville item) {
			return this.Context.Villes.Count(v => 
			                                  v.Libelle.Equals(item.Libelle) 
			                                  && v.CodePostal.Equals(item.CodePostal)
			                                  && v.ID != item.ID
			                                 ) != 0;
		}
		
		/// <summary>
		/// Vérifie si une ville est utilisée par au moins un adhérent ou les infos club
		/// </summary>
		/// <param name="item">Ville à vérifier</param>
		/// <returns>True si la ville est utilisée, False sinon</returns>
		protected override bool ItemIsUsed(Ville item) {
			return item != null
				&& item.Adherents != null 
				&& item.Adherents.Count > 0 
				&& item.InfosClubs != null 
				&& item.InfosClubs.Count > 0;
		}
		#endregion
	}
}
