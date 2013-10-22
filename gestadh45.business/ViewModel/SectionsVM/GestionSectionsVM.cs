using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using gestadh45.business.Enums;
using gestadh45.business.PersonalizedMsg;
using gestadh45.model;
/*
 * Crée par SharpDevelop.
 * Utilisateur: Guillaume
 * Date: 02/03/2013
 * Heure: 15:47
 * 
 * Pour changer ce modèle utiliser Outils | Options | Codage | Editer les en-têtes standards.
 */
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Windows.Input;

namespace gestadh45.business.ViewModel.SectionsVM
{
	/// <summary>
	/// Description of GestionSectionsVM.
	/// </summary>
	public class GestionSectionsVM : GenericGestionVM<Section>
	{
		public GestionSectionsVM(string userConnectionString) : base(userConnectionString) {
			this.UCCode = CodesUC.GestionSections;
			this.CreateSetSectionDefautCommand();
		}
		
		#region SetSectionDefautCommand
		public ICommand SetSectionDefautCommand { get; set; }

		private void CreateSetSectionDefautCommand() {
			this.SetSectionDefautCommand = new RelayCommand<Section>(
				this.ExecuteSetSectionDefautCommand,
				this.CanExecuteSetSectionDefautCommand
				);
		}

		public bool CanExecuteSetSectionDefautCommand(Section section) {
			return section != null && !section.EstDefaut;
		}

		public void ExecuteSetSectionDefautCommand(Section section) {
			if (section != null) {
				// on récupère l'ancienne section par défaut et on lui retire l'attribut
				Section oldSectionDefaut = this.Context.Sections.FirstOrDefault(s => s.EstDefaut);
				oldSectionDefaut.EstDefaut = false;
				this.Context.Entry(oldSectionDefaut).State = EntityState.Modified;

				section.EstDefaut = true;

				this.Context.SaveChanges();
				this.ShowUserNotification(string.Format(CultureInfo.CurrentCulture, ResSections.InfoSetSectionDefaut, section.ToString()));

				this.PopulateItems();

				// on rafraîchit l'affichage des détails de la saison
				this.SelectedItem = null;
				this.SelectedItem = section;

				Messenger.Default.Send(new NMSelectionElement<Section>(this.SelectedItem));
			}
		}
		#endregion
		
		#region methods override
		protected override Section CreateDefaultItem() {
			return new Section() 
			{
				Libelle = ResSections.Defaut_Libelle,
				EstDefaut = false
			};
		}
		
		protected override bool ValidateItem(Section item, Collection<string> errors) {
			if (string.IsNullOrWhiteSpace(item.Libelle)) {
				errors.Add(ResSections.ErrLibelleObligatoire);
			}

			if (errors.Count == 0 && this.ItemExists(item)) {
				errors.Add(ResSections.ErrSectionExiste);
			}

			return errors.Count == 0;
		}
		
		protected override bool ItemExists(Section item) {
			return this.Context.Sections.Where(s => 
			                                    s.Libelle.Equals(item.Libelle)
			                                    && s.ID != item.ID
			                                   ).Count() != 0;
		}
		
		/// <summary>
		/// Vérifie si une section est utilisée (ou est la section par défaut)
		/// </summary>
		/// <param name="item">Section à vérifier</param>
		/// <returns>True si la section est utilisée ou section par défaut, False sinon</returns>
		protected override bool ItemIsUsed(Section item) {
			return item.EstDefaut || (item.Inscriptions != null && item.Inscriptions.Count > 0);
		}
		
		protected override void FormatValues(Section item) {
			item.Libelle = (item.Libelle == null) ? null : item.Libelle.ToUpperInvariant();
		}
		#endregion
	}
}
