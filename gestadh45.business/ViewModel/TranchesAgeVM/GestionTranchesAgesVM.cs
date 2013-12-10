using gestadh45.business.Enums;
using gestadh45.model;
using System.Collections.ObjectModel;
using System.Linq;

namespace gestadh45.business.ViewModel.TranchesAgeVM
{
	/// <summary>
	/// Description of GestionTranchesAgesVM.
	/// </summary>
	public class GestionTranchesAgesVM : GenericGestionVM<TrancheAge>
	{
		public GestionTranchesAgesVM() : base() {
			this.UCCode = CodesUC.GestionTranchesAge;
		}
		
		#region methods override
		protected override TrancheAge CreateDefaultItem() {
			return new TrancheAge() 
			{
				AgeInf = 0,
				AgeSup = 0
			};
		}
		
		/// <summary>
		/// Vérifie si il existe déjà une tranche d'âge identique
		/// </summary>
		/// <param name="item">Tranche d'âge à vérifier</param>
		/// <returns>True si il existe déjà une tranche d'âge identique, False sinon</returns>
		protected override bool ItemExists(TrancheAge item) {
			return this.Context.TrancheAges.Count(t => 
			                                       t.AgeInf == item.AgeInf 
			                                       && t.AgeSup == item.AgeSup
			                                       && t.ID != item.ID
			                                      ) != 0;
		}
		
		protected override bool ValidateItem(TrancheAge item, Collection<string> errors) {
			if (item.AgeInf < 0) {
				errors.Add(ResTranchesAge.TrancheAge_AgeInfPositif);
			}

			if (item.AgeSup < 0) {
				errors.Add(ResTranchesAge.TrancheAge_AgeSupPositif);
			}

			// on vérifie que l'âge de fin soit supérieur ou égal à l'âge de début
			if (errors.Count == 0 && item.AgeSup < item.AgeInf) {
				errors.Add(ResTranchesAge.TrancheAge_OrdreAges);
			}

			// on vérifie que la tranche n'existe pas déjà
			if (errors.Count == 0 && this.ItemExists(item)) {
				errors.Add(ResTranchesAge.TrancheAge_Existe);
			}

			return errors.Count == 0;
		}
		#endregion
	}
}
