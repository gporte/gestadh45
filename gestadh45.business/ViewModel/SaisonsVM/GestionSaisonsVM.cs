using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using gestadh45.business.Enums;
using gestadh45.business.PersonalizedMsg;
using gestadh45.model;
/*
 * Crée par SharpDevelop.
 * Utilisateur: Guillaume
 * Date: 02/03/2013
 * Heure: 11:24
 * 
 * Pour changer ce modèle utiliser Outils | Options | Codage | Editer les en-têtes standards.
 */
using System;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Windows.Input;

namespace gestadh45.business.ViewModel.SaisonsVM
{
	/// <summary>
	/// Description of GestionSaisonsVM.
	/// </summary>
	public class GestionSaisonsVM : GenericGestionVM<Saison>
	{
		private const int DureeSaison = 1;
		
		public GestionSaisonsVM(string userConnectionString) : base(userConnectionString) {
			this.UCCode = CodesUC.GestionSaisons;
			this.CreateSetSaisonCouranteCommand();
		}
		
		#region SetSaisonCouranteCommand
		public ICommand SetSaisonCouranteCommand { get; set; }
		
		private void CreateSetSaisonCouranteCommand() {
			this.SetSaisonCouranteCommand = new RelayCommand<Saison>(
				this.ExecuteSetSaisonCouranteCommand,
				this.CanExecuteSetSaisonCouranteCommand
			);
		}

		public bool CanExecuteSetSaisonCouranteCommand(Saison saison) {
			return saison != null && !saison.EstSaisonCourante;
		}

		public void ExecuteSetSaisonCouranteCommand(Saison saison) {
			if (saison != null) {
				// on récupère l'ancienne saison courante et on lui retire l'attribut
				Saison oldSaisonCourante = this.Context.Saisons.FirstOrDefault((s)=>s.EstSaisonCourante);
				oldSaisonCourante.EstSaisonCourante = false;
				this.Context.Entry(oldSaisonCourante).State = EntityState.Modified;

				saison.EstSaisonCourante = true;

				this.Context.SaveChanges();
				this.ShowUserNotification(string.Format(CultureInfo.CurrentCulture, ResSaisons.InfoSetSaisonCourante, saison.ToShortString()));

				this.PopulateItems();
				this.SelectedItem = saison;
				
				Messenger.Default.Send(new NMSelectionElement<Saison>(this.SelectedItem));
			}
		}
		#endregion
		
		#region methods override
		protected override Saison CreateDefaultItem() {
			return new Saison() 
			{
				AnneeDebut = DateTime.Now.Year,
				AnneeFin = DateTime.Now.Year + DureeSaison,
				EstSaisonCourante = false
			};
		}
		
		protected override bool ValidateItem(Saison item, Collection<string> errors) {
			if (this.ItemExists(item)) {
				errors.Add(ResSaisons.ErrSaisonExiste);
			}

			return errors.Count == 0;
		}
		
		/// <summary>
		/// Vérifie si il existe déjà une autre saison avec la même année de début
		/// </summary>
		/// <param name="item">Saison à vérifier</param>
		/// <returns>True si il existe déjà une saison, False sinon</returns>
		protected override bool ItemExists(Saison item)	{
			return this.Context.Saisons.Where(s => 
			                                   s.AnneeDebut == item.AnneeDebut
			                                   && s.ID != item.ID
			                                  ).Count() != 0;
		}
		
		/// <summary>
		/// Vérifie si une saison est la saison courante ou si elle possède des groupes
		/// </summary>
		/// <param name="item">Saison à vérifier</param>
		/// <returns>True si la saison est utilisée, False sinon</returns>
		protected override bool ItemIsUsed(Saison item) {
			return item.EstSaisonCourante || (item.Groupes != null && item.Groupes.Count > 0);
		}
		
		protected override void FormatValues(Saison item) {
			item.AnneeFin = item.AnneeDebut + DureeSaison;
		}
		
		/// <summary>
		/// On ne peut pas modifier une saison si elle est utilisée
		/// </summary>
		/// <returns></returns>
		public override bool CanExecuteSaveItemCommand() {
			return this.SelectedItem != null && !this.ItemIsUsed(this.SelectedItem);
		}
		#endregion
	}
}
