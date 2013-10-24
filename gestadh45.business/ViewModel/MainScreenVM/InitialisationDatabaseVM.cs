using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using gestadh45.business.Enums;
using gestadh45.business.PersonalizedMsg;
using gestadh45.business.ViewModel.InfosClubVM;
using gestadh45.business.ViewModel.VillesVM;
using gestadh45.model;
/*
 * Crée par SharpDevelop.
 * Utilisateur: gp
 * Date: 11/03/2013
 * Heure: 09:26
 * 
 * Pour changer ce modèle utiliser Outils | Options | Codage | Editer les en-têtes standards.
 */
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Windows.Input;

namespace gestadh45.business.ViewModel.MainScreenVM
{
	/// <summary>
	/// Description of InitialisationDatabaseVm.
	/// </summary>
	public class InitialisationDatabaseVM : VMUCBase
	{
		#region Villes
		private IOrderedEnumerable<Ville> _villes;

		/// <summary>
		/// Obtient/Définit la liste des villes
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
		
		#region CurrentItem
		private InfosClub _currentItem;
		
		/// <summary>
		/// Obtient/Définit l'objet courant (InfosClub)
		/// </summary>
		public InfosClub CurrentItem {
			get { return this._currentItem; }
			set {
				if(this._currentItem != value) {
					this._currentItem = value;
					this.RaisePropertyChanged(()=>this.CurrentItem);
				}
			}
		}
		#endregion
		
		public InitialisationDatabaseVM(string userConnectionString) : base(userConnectionString) {
			this.UCCode = CodesUC.InitialisationDatabase;
			this.AddDefautItem();
			this.CurrentItem = this.Context.InfosClub.FirstOrDefault();
			
			this.CreateCancelCommand();
			this.CreateSaveCommand();

			this.Villes = this.Context.Villes.ToList().OrderBy(v => v.ToString());	
		}
		
		#region private methods
		private bool CheckFormValidity(InfosClub item, List<string> errors) {
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
		
		private void AddDefautItem() {
			// Création de la saison courante si n'existe pas
			if(this.Context.Saisons.Count(x => x.EstSaisonCourante) == 0) {
				var saison = new Saison() 
				{
					AnneeDebut = DateTime.Now.Year,
					AnneeFin = DateTime.Now.Year + 1,
					EstSaisonCourante = true
				};
				
				this.Context.Saisons.Add(saison);
			}
			
			// Création d'une ville si il n'y en a pas
			if(this.Context.Villes.Count() == 0) {
				var ville = new Ville()
				{
					CodePostal = ResVilles.DefautCodePostal,
					Libelle = ResVilles.DefautLibelle
				};
				
				this.Context.Villes.Add(ville);
			}
			
			// création de l'objet infosClub par défaut (après nettoyage)
			if (this.Context.InfosClub.Count() > 0) {
				while(this.Context.InfosClub.Count() > 0) {
					this.Context.Entry(this.Context.InfosClub.FirstOrDefault()).State = EntityState.Deleted;
				}
			}
			
			var infosClub = new InfosClub()
			{
				Nom = ResInfosClub.Defaut_Nom,
				Adresse = ResInfosClub.Defaut_Adresse,
				Ville = this.Context.Villes.FirstOrDefault()				
			};
			
			this.Context.InfosClub.Add(infosClub);
			
			// enregistrement
			this.Context.SaveChanges();
		}
		#endregion
		
		#region CancelCommand
		public ICommand CancelCommand { get; set; }
		
		private void CreateCancelCommand() {
			this.CancelCommand = new RelayCommand(this.ExecuteCancelCommand);
		}
		
		public void ExecuteCancelCommand() {
			Messenger.Default.Send(new NMCloseApplication());
		}
		#endregion
		
		#region SaveCommand
		public ICommand SaveCommand { get; set; }
		
		private void CreateSaveCommand() {
			this.SaveCommand = new RelayCommand(this.ExecuteSaveCommand);
		}
		
		public void ExecuteSaveCommand() {			
			var errors = new List<string>();

			if (this.CheckFormValidity(this.CurrentItem, errors)) {
				this.Context.Entry(this.CurrentItem).State = EntityState.Modified;
				this.Context.SaveChanges();

				Messenger.Default.Send(new NMMainMenuState(true));
				Messenger.Default.Send(new NMShowUC(CodesUC.GestionInfosClub));
			}
			else {
				this.ShowUserNotifications(errors);
			}
		}
		#endregion
	}
}
