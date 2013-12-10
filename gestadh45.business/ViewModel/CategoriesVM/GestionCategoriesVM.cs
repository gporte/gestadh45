using gestadh45.business.Enums;
using gestadh45.model;
using System.Collections.ObjectModel;
using System.Linq;

namespace gestadh45.business.ViewModel.CategoriesVM
{
	/// <summary>
	/// Description of GestionCategoriesVM.
	/// </summary>
	public class GestionCategoriesVM : GenericGestionVM<Categorie>
	{
		#region DureesDeVie
		private IOrderedEnumerable<DureeDeVie> _dureesDeVie;

		/// <summary>
		/// Obtient/Définit la liste des durées de vie
		/// </summary>
		public IOrderedEnumerable<DureeDeVie> DureesDeVie {
			get { return this._dureesDeVie; }
			set {
				if (this._dureesDeVie != value) {
					this._dureesDeVie = value;
					this.RaisePropertyChanged(() => this.DureesDeVie);
				}
			}
		}
		#endregion
		
		public GestionCategoriesVM() : base() {
			this.UCCode = CodesUC.GestionCategories;
		}
		
		#region methods override
		protected override void PopulateSpecificDatas()	{
			this.DureesDeVie = this.Context.DureeDeVies.ToList().OrderBy(d => d.Libelle);
		}
		
		protected override Categorie CreateDefaultItem() {
			return new Categorie()
			{
				Libelle = ResCategories.Defaut_Libelle,
				DureeDeVie = this.DureesDeVie.FirstOrDefault()
			};
		}
		
		protected override bool ValidateItem(Categorie item, Collection<string> errors) {
			if (string.IsNullOrWhiteSpace(item.Libelle)) {
				errors.Add(ResCategories.ErrLibelleObligatoire);
			}
			
			if (item.DureeDeVie == null) {
				errors.Add(ResCategories.ErrDureeDeVieObligatoire);
			}

			if (errors.Count == 0 && this.ItemExists(item)) {
				errors.Add(ResCategories.ErrCategorieExiste);
			}

			return errors.Count == 0;
		}
		
		protected override bool ItemExists(Categorie item) {
			return this.Context.Categories.Where(c => 
			                                    c.Libelle.Equals(item.Libelle)
			                                    && c.ID != item.ID
			                                   ).Count() != 0;
		}
		
		/// <summary>
		/// Vérifie si une catégorie est utilisée
		/// </summary>
		/// <param name="item">catégorie à vérifier</param>
		/// <returns>True si la catégorie est utilisée, False sinon</returns>
		protected override bool ItemIsUsed(Categorie item) {
			return item.Modeles != null && item.Modeles.Count > 0;
		}
		
		protected override void FormatValues(Categorie item) {
			item.Libelle = (item.Libelle == null) ? null : item.Libelle.ToUpperInvariant();
		}
		#endregion
	}
}
