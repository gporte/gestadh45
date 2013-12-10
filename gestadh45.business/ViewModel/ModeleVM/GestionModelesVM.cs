using gestadh45.business.Enums;
using gestadh45.model;
using System.Collections.ObjectModel;
using System.Linq;

namespace gestadh45.business.ViewModel.ModeleVM
{
	/// <summary>
	/// Description of GestionModelesVM.
	/// </summary>
	public class GestionModelesVM : GenericGestionVM<Modele>
	{
		#region Categories
		private IOrderedEnumerable<Categorie> _categories;

		/// <summary>
		/// Obtient/Définit la liste des catégories
		/// </summary>
		public IOrderedEnumerable<Categorie> Categories {
			get { return this._categories; }
			set {
				if (this._categories != value) {
					this._categories = value;
					this.RaisePropertyChanged(() => this.Categories);
				}
			}
		}
		#endregion

		#region Marques
		private IOrderedEnumerable<Marque> _marques;

		/// <summary>
		/// Gets or sets the marques.
		/// </summary>
		/// <value>
		/// The marques.
		/// </value>
		public IOrderedEnumerable<Marque> Marques {
			get {
				return this._marques;
			}

			set {
				if (this._marques != value) {
					this._marques = value;
					this.RaisePropertyChanged(() => this.Marques);
				}
			}
		}
		#endregion
		
		public GestionModelesVM() : base() {
			this.UCCode = CodesUC.GestionModeles;
		}
		
		#region methods override
		protected override void PopulateSpecificDatas()	{
			this.Categories = this.Context.Categories.ToList().OrderBy(c => c.Libelle);
			this.Marques = this.Context.Marques.ToList().OrderBy(m => m.Libelle);
		}
		
		protected override Modele CreateDefaultItem() {
			return new Modele()
			{
				Nom = ResModeles.Defaut_Nom,
				Categorie = this.Categories.FirstOrDefault(),
				Marque = this.Marques.FirstOrDefault()
			};
		}
		
		protected override bool ValidateItem(Modele item, Collection<string> errors) {
			if (string.IsNullOrWhiteSpace(item.Nom)) {
				errors.Add(ResModeles.ErrNomObligatoire);
			}
			
			if (item.Categorie == null) {
				errors.Add(ResModeles.ErrCategorieObligatoire);
			}
			
			if (item.Marque == null) {
				errors.Add(ResModeles.ErrMarqueObligatoire);
			}

			if (errors.Count == 0 && this.ItemExists(item)) {
				errors.Add(ResModeles.ErrModeleExiste);
			}

			return errors.Count == 0;
		}
		
		protected override bool ItemExists(Modele item) {
			return this.Context.Modeles.Where(
					m => m.Nom.Equals(item.Nom)
						&& m.Marque.ID == item.Marque.ID
						&& m.Categorie.ID == item.Categorie.ID
						&& m.Couleur1.Equals(item.Couleur1)
						&& m.Couleur2.Equals(item.Couleur2)
						&& m.Couleur3.Equals(item.Couleur3)
						&& m.ID != item.ID
				).Count() != 0;
		}
		
		/// <summary>
		/// Vérifie si un modèle est utilisé
		/// </summary>
		/// <param name="item">modèle à vérifier</param>
		/// <returns>True si le modèle est utilisé, False sinon</returns>
		protected override bool ItemIsUsed(Modele item) {
			return item.Equipements != null && item.Equipements.Count > 0;
		}
		
		protected override void FormatValues(Modele item) {
			item.Nom = (item.Nom == null) ? null : item.Nom.ToUpperInvariant();
		}
		#endregion
	}
}
