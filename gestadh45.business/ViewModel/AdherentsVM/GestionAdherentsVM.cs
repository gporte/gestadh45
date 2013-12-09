using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using gestadh45.business.Enums;
using gestadh45.business.PersonalizedMsg;
using gestadh45.model;
using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Windows.Input;

namespace gestadh45.business.ViewModel.AdherentsVM
{
	/// <summary>
	/// Description of GestionAdherentsVM.
	/// </summary>
	public class GestionAdherentsVM : GenericGestionVM<Adherent>
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
		
		#region Sexes
		private IOrderedEnumerable<Sexe> _sexes;

		/// <summary>
		/// Obtient/Définit la liste des sexes
		/// </summary>
		public IOrderedEnumerable<Sexe> Sexes {
			get { return this._sexes; }
			set {
				if (this._sexes != value) {
					this._sexes = value;
					this.RaisePropertyChanged(() => this.Sexes);
				}
			}
		}
		#endregion

		public GestionAdherentsVM()
			: base() {
			this.UCCode = CodesUC.GestionAdherents;
			this.CreateInscriptionCommand();

			Messenger.Default.Register<NMLoadItem<Adherent>>(this, msg => this.LoadAdherent(msg.Content.ID));
		}

		private void LoadAdherent(Guid adhId) {
			this.SelectedItem = this.Context.Adherents.Find(adhId);

			if (this.SelectedItem != null) {
				Messenger.Default.Send(new NMSelectionElement<Adherent>(this.SelectedItem));
			}
		}
		
		#region InscriptionCommand
		public ICommand InscriptionCommand { get; set; }
		
		private void CreateInscriptionCommand() {
			this.InscriptionCommand = new RelayCommand(
				this.ExecuteInscriptionCommand,
				this.CanExecuteInscriptionCommand
			);
		}
		
		public void ExecuteInscriptionCommand() {
			if(this.SelectedItem != null) {
				Messenger.Default.Send(new NMShowUC<Adherent>(CodesUC.GestionInscriptions, this.SelectedItem));
			}
		}
		
		public bool CanExecuteInscriptionCommand() {
			return this.SelectedItem != null;
		}
		#endregion
		
		#region methods override
		protected override void PopulateSpecificDatas() {
			this.Villes = this.Context.Villes.ToList().OrderBy(v => v.ToString());
			this.Sexes = Enum.GetValues(typeof(Sexe)).Cast<Sexe>().OrderBy(s => s);
		}
		
		protected override Adherent CreateDefaultItem() {
			return new Adherent()
			{
				Nom = ResAdherents.DefautNom,
				Prenom = ResAdherents.DefautPrenom,
				DateNaissance = DateTime.Now,
				DateCreation = DateTime.Now,
				DateModification = DateTime.Now,
				Adresse = ResAdherents.DefautAdresse,
				Ville = this.Context.InfosClub.FirstOrDefault().Ville,
				Sexe = Sexe.M
			};
		}
		
		protected override Adherent CreateCloneItem() {
			Adherent result;
			
			if(this.SelectedItem != null) {
				result = this.SelectedItem.Clone() as Adherent;
				result.Prenom = string.Format(CultureInfo.CurrentCulture, ResCommon.TemplateCloneItemName, result.Prenom);
				result.Commentaire = string.Format(CultureInfo.CurrentCulture, ResCommon.TemplateCloneItemComment, this.SelectedItem.ToString());
			}
			else {
				result = this.CreateDefaultItem();
			}
			
			return result;
		}
		
		protected override bool ValidateItem(Adherent item, Collection<string> errors)	{
			if (string.IsNullOrWhiteSpace(item.Nom)) {
				errors.Add(ResAdherents.ErrNomObligatoire);
			}

			if (string.IsNullOrWhiteSpace(item.Prenom)) {
				errors.Add(ResAdherents.ErrPrenomObligatoire);
			}

			if (item.DateNaissance == DateTime.MinValue) {
				errors.Add(ResAdherents.ErrDateNaissanceObligatoire);
			}

			if (string.IsNullOrWhiteSpace(item.Adresse)) {
				errors.Add(ResAdherents.ErrAdresseObligatoire);
			}

			if (item.Ville == null) {
				errors.Add(ResAdherents.ErrVilleObligatoire);
			}

			if (errors.Count == 0 && this.ItemExists(item)) {
				errors.Add(ResAdherents.ErrAdherentExiste);
			}

			return errors.Count == 0;
		}
		
		/// <summary>
		/// Vérifie si un autre adhérent avec les mêmes noms et prénoms existe déjà
		/// </summary>
		/// <param name="item">Adhérent à tester</param>
		/// <returns>True si un adhérent existe déjà, False sinon</returns>
		protected override bool ItemExists(Adherent item) {
			return this.Context.Adherents.Count(a => 
			                                     a.Nom.Equals(item.Nom)
			                                     && a.Prenom.Equals(item.Prenom)
												 && a.ID != item.ID
												) != 0;
		}
		
		/// <summary>
		/// Vérifie si un adhérent est utilisé par au moins une inscription.
		/// </summary>
		/// <param name="item">Adhérent à vérifier</param>
		/// <returns>True si l'adhérent possède au moins une inscription, False sinon</returns>
		protected override bool ItemIsUsed(Adherent item) {
			return item != null
				&& item.Inscriptions != null
				&& item.Inscriptions.Count > 0;
		}
		
		protected override void FormatValues(Adherent item) {
			item.Nom = (item.Nom == null) ? null : item.Nom.ToUpperInvariant();
			item.Prenom = (item.Prenom == null) ? null : CultureInfo.CurrentCulture.TextInfo.ToTitleCase(item.Prenom);
			item.DateModification = DateTime.Now;
		}
		#endregion
	}
}
