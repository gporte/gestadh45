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

using GalaSoft.MvvmLight.Ioc;
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
using Microsoft.Practices.ServiceLocation;
using System;

namespace gestadh45.business.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
		private static string _currentKey = Guid.NewGuid().ToString();

		public static string CurrentKey {
			get {
				return _currentKey;
			}
			private set {
				_currentKey = value;
			}
		}
		
		/// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            ////if (ViewModelBase.IsInDesignModeStatic)
            ////{
            ////    // Create design time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DesignDataService>();
            ////}
            ////else
            ////{
            ////    // Create run time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DataService>();
            ////}

			// Global
            SimpleIoc.Default.Register<MainViewModel>();
			SimpleIoc.Default.Register<MainScreenCheckVM>();
			SimpleIoc.Default.Register<InitialisationDatabaseVM>();
			SimpleIoc.Default.Register<GestionInfosClubVM>();

			// inscriptions
			SimpleIoc.Default.Register<GestionGroupesVM>();
			SimpleIoc.Default.Register<GestionAdherentsVM>();
			SimpleIoc.Default.Register<GestionInscriptionsVM>();

			// équipements
			SimpleIoc.Default.Register<GestionModelesVM>();
			SimpleIoc.Default.Register<GestionEquipementsVM>();
			SimpleIoc.Default.Register<GestionLocalisationsEquipementVM>();
			SimpleIoc.Default.Register<GestionCampagnesVerificationVM>();
			SimpleIoc.Default.Register<GestionSaisiesVerificationsVM>();

			// référentiel
			SimpleIoc.Default.Register<GestionVillesVM>();
			SimpleIoc.Default.Register<GestionSaisonsVM>();
			SimpleIoc.Default.Register<GestionTranchesAgesVM>();
			SimpleIoc.Default.Register<GestionSectionsVM>();
			SimpleIoc.Default.Register<GestionMarquesVM>();
			SimpleIoc.Default.Register<GestionDureesDeVieVM>();
			SimpleIoc.Default.Register<GestionDureesDeVieVM>();
			SimpleIoc.Default.Register<GestionCategoriesVM>();
			SimpleIoc.Default.Register<GestionLocalisationsVM>();

			// outils
			SimpleIoc.Default.Register<ReportingVM>();
			SimpleIoc.Default.Register<GraphiquesVM>();
			SimpleIoc.Default.Register<NettoyageCNILVM>();
			SimpleIoc.Default.Register<BackupVM>();
        }

		#region global
		public MainViewModel MainVM {
			get { return ServiceLocator.Current.GetInstance<MainViewModel>(CurrentKey); }
        }

		public MainScreenCheckVM MainScreenVM {
			get { return ServiceLocator.Current.GetInstance<MainScreenCheckVM>(CurrentKey); }
		}

		public InitialisationDatabaseVM InitDbVM {
			get { return ServiceLocator.Current.GetInstance<InitialisationDatabaseVM>(CurrentKey); }
		}

		public GestionInfosClubVM InfosClubVM {
			get { return ServiceLocator.Current.GetInstance<GestionInfosClubVM>(CurrentKey); }
		}
		#endregion

		#region inscriptions
		public GestionGroupesVM GroupesVM {
			get { return ServiceLocator.Current.GetInstance<GestionGroupesVM>(CurrentKey); }
		}

		public GestionAdherentsVM AdherentsVM {
			get { return ServiceLocator.Current.GetInstance<GestionAdherentsVM>(CurrentKey); }
		}

		public GestionInscriptionsVM InscriptionsVM {
			get { return ServiceLocator.Current.GetInstance<GestionInscriptionsVM>(CurrentKey); }
		}
		#endregion

		#region équipements
		public GestionModelesVM ModelesVM {
			get { return ServiceLocator.Current.GetInstance<GestionModelesVM>(CurrentKey); }
		}

		public GestionEquipementsVM EquipementsVM {
			get { return ServiceLocator.Current.GetInstance<GestionEquipementsVM>(CurrentKey); }
		}

		public GestionLocalisationsEquipementVM LocalisationEquipementVM {
			get { return ServiceLocator.Current.GetInstance<GestionLocalisationsEquipementVM>(CurrentKey); }
		}

		public GestionCampagnesVerificationVM CampagnesVerificationVM {
			get { return ServiceLocator.Current.GetInstance<GestionCampagnesVerificationVM>(CurrentKey); }
		}

		public GestionSaisiesVerificationsVM SaisiesVerificationsVM {
			get { return ServiceLocator.Current.GetInstance<GestionSaisiesVerificationsVM>(CurrentKey); }
		}
		#endregion

		#region référentiel
		public GestionVillesVM VillesVM {
			get { return ServiceLocator.Current.GetInstance<GestionVillesVM>(CurrentKey); }
		}

		public GestionSaisonsVM SaisonsVM {
			get { return ServiceLocator.Current.GetInstance<GestionSaisonsVM>(CurrentKey); }
		}

		public GestionTranchesAgesVM TranchesAgesVM {
			get { return ServiceLocator.Current.GetInstance<GestionTranchesAgesVM>(CurrentKey); }
		}

		public GestionSectionsVM SectionsVM {
			get { return ServiceLocator.Current.GetInstance<GestionSectionsVM>(CurrentKey); }
		}

		public GestionMarquesVM MarquesVM {
			get { return ServiceLocator.Current.GetInstance<GestionMarquesVM>(CurrentKey); }
		}

		public GestionDureesDeVieVM DureesDeVieVM {
			get { return ServiceLocator.Current.GetInstance<GestionDureesDeVieVM>(CurrentKey); }
		}

		public GestionCategoriesVM CategoriesVM {
			get { return ServiceLocator.Current.GetInstance<GestionCategoriesVM>(CurrentKey); }
		}

		public GestionLocalisationsVM LocalisationsVM {
			get { return ServiceLocator.Current.GetInstance<GestionLocalisationsVM>(CurrentKey); }
		}
		#endregion

		#region outils
		public ReportingVM ReportVM {
			get { return ServiceLocator.Current.GetInstance<ReportingVM>(CurrentKey); }
		}

		public GraphiquesVM GraphsVM {
			get { return ServiceLocator.Current.GetInstance<GraphiquesVM>(CurrentKey); }
		}

		public NettoyageCNILVM CnilVM {
			get { return ServiceLocator.Current.GetInstance<NettoyageCNILVM>(CurrentKey); }
		}

		public BackupVM BakVM {
			get { return ServiceLocator.Current.GetInstance<BackupVM>(CurrentKey); }
		}
		#endregion

		public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}