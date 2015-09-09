/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:gestadh45.business"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using gestadh45.business.ViewModel.AdherentsVM;
using gestadh45.business.ViewModel.CampagnesVerificationVM;
using gestadh45.business.ViewModel.CategoriesVM;
using gestadh45.business.ViewModel.DureesDeVieVM;
using gestadh45.business.ViewModel.EquipementsVM;
using gestadh45.business.ViewModel.GroupesVM;
using gestadh45.business.ViewModel.InfosClubVM;
using gestadh45.business.ViewModel.InscriptionsVM;
using gestadh45.business.ViewModel.LocalisationVM;
using gestadh45.business.ViewModel.MainScreenVM;
using gestadh45.business.ViewModel.MarquesVM;
using gestadh45.business.ViewModel.ModeleVM;
using gestadh45.business.ViewModel.OutilsVM;
using gestadh45.business.ViewModel.SaisonsVM;
using gestadh45.business.ViewModel.SectionsVM;
using gestadh45.business.ViewModel.TranchesAgeVM;
using gestadh45.business.ViewModel.VillesVM;

namespace gestadh45.business.ViewModel
{
	public class ViewModelLocator
    {
		#region global
		public MainViewModel MainVM {
			get { return new MainViewModel(); }
		}

		public MainScreenCheckVM MainScreenVM {
			get { return new MainScreenCheckVM(); }
		}

		public InitialisationDatabaseVM InitDbVM {
			get { return new InitialisationDatabaseVM(); }
		}

		public GestionInfosClubVM InfosClubVM {
			get { return new GestionInfosClubVM(); }
		}
		#endregion

		#region inscriptions
		public GestionGroupesVM GroupesVM {
			get { return new GestionGroupesVM(); }
		}

		public GestionAdherentsVM AdherentsVM {
			get { return new GestionAdherentsVM(); }
		}

		public GestionInscriptionsVM InscriptionsVM {
			get { return new GestionInscriptionsVM(); }
		}
		#endregion

		#region équipements
		public GestionModelesVM ModelesVM {
			get { return new GestionModelesVM(); }
		}

		public GestionEquipementsVM EquipementsVM {
			get { return new GestionEquipementsVM(); }
		}

		public GestionLocalisationsEquipementVM LocalisationEquipementVM {
			get { return new GestionLocalisationsEquipementVM(); }
		}

		public GestionCampagnesVerificationVM CampagnesVerificationVM {
			get { return new GestionCampagnesVerificationVM(); }
		}

		public GestionSaisiesVerificationsVM SaisiesVerificationsVM {
			get { return new GestionSaisiesVerificationsVM(); }
		}
		#endregion

		#region référentiel
		public GestionVillesVM VillesVM {
			get { return new GestionVillesVM(); }
		}

		public GestionSaisonsVM SaisonsVM {
			get { return new GestionSaisonsVM(); }
		}

		public GestionTranchesAgesVM TranchesAgesVM {
			get { return new GestionTranchesAgesVM(); }
		}

		public GestionSectionsVM SectionsVM {
			get { return new GestionSectionsVM(); }
		}

		public GestionMarquesVM MarquesVM {
			get { return new GestionMarquesVM(); }
		}

		public GestionDureesDeVieVM DureesDeVieVM {
			get { return new GestionDureesDeVieVM(); }
		}

		public GestionCategoriesVM CategoriesVM {
			get { return new GestionCategoriesVM(); }
		}

		public GestionLocalisationsVM LocalisationsVM {
			get { return new GestionLocalisationsVM(); }
		}
		#endregion

		#region outils
		public ReportingVM ReportVM {
			get { return new ReportingVM(); }
		}

		public GraphiquesVM GraphsVM {
			get { return new GraphiquesVM(); }
		}

		public NettoyageCNILVM CnilVM {
			get { return new NettoyageCNILVM(); }
		}

		public BackupVM BakVM {
			get { return new BackupVM(); }
		}
		#endregion
    }
}