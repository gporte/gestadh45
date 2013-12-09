using System;
using System.Configuration;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

using GalaSoft.MvvmLight.Messaging;
using gestadh45.business.Enums;
using gestadh45.business.PersonalizedMsg;
using gestadh45.business.ViewModel;
using gestadh45.model;
using gestadh45.wpf.UserControls.AdherentsUC;
using gestadh45.wpf.UserControls.CampagnesVerificationUC;
using gestadh45.wpf.UserControls.CategoriesUC;
using gestadh45.wpf.UserControls.DureesDeVieUC;
using gestadh45.wpf.UserControls.EquipementsUC;
using gestadh45.wpf.UserControls.GroupesUC;
using gestadh45.wpf.UserControls.InfosClubUC;
using gestadh45.wpf.UserControls.InscriptionsUC;
using gestadh45.wpf.UserControls.LocalisationUC;
using gestadh45.wpf.UserControls.MainScreenUC;
using gestadh45.wpf.UserControls.MarquesUC;
using gestadh45.wpf.UserControls.ModelesUC;
using gestadh45.wpf.UserControls.OutilsUC;
using gestadh45.wpf.UserControls.SaisonsUC;
using gestadh45.wpf.UserControls.SectionsUC;
using gestadh45.wpf.UserControls.TranchesAgeUC;
using gestadh45.wpf.UserControls.VillesUC;
using Microsoft.Win32;
using Forms = System.Windows.Forms;

namespace gestadh45.wpf
{
	/// <summary>
	/// Logique d'interaction pour MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{		
		public MainWindow() {
			// Ensure the current culture passed into bindings 
			// is the OS culture. By default, WPF uses en-US 
			// as the culture, regardless of the system settings.
			FrameworkElement.LanguageProperty.OverrideMetadata(
				typeof(FrameworkElement), 
				new FrameworkPropertyMetadata(
					XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag)
				)
			);			

			InitializeComponent();
			//this.DataContext = new MainViewModel();

			// Abonnement aux messages
			Messenger.Default.Register<NMCloseApplication>(this, (msg) => this.Exit());
			Messenger.Default.Register<NMShowUC>(this, (msg) => this.ShowUC(msg.CodeUC));
			Messenger.Default.Register<NMOpenWindow>(this, (msg) => this.OpenWindowUC(msg.CodeUC, msg.ParentGuid));
			Messenger.Default.Register<NMShowUC<Adherent>>(this, (msg) => this.ShowUCWithParameters(msg.CodeUC, msg.Content));
			Messenger.Default.Register<NMShowUC<Inscription>>(this, (msg) => this.ShowUCWithParameters(msg.CodeUC, msg.Content));
			Messenger.Default.Register<NMShowUC<Equipement>>(this, (msg) => this.ShowUCWithParameters(msg.CodeUC, msg.Content));
			Messenger.Default.Register<NMShowUC<DureeDeVie>>(this, (msg) => this.ShowUCWithParameters(msg.CodeUC, msg.Content));
			Messenger.Default.Register<NMShowUC<CampagneVerification>>(this, (msg) => this.ShowUCWithParameters(msg.CodeUC, msg.Content));
			Messenger.Default.Register<NMShowAboutBox>(this, (msg) => this.ShowAboutBox());
			Messenger.Default.Register<NMAskConfirmationDialog<bool>>(this, msg => this.ShowConfirmationDialog(msg.Execute, msg.Text));
			Messenger.Default.Register<NMGetUserSetting<string>>(this, msg => this.GetUserSettingValue(msg.Execute, msg.SettingName));
			Messenger.Default.Register<NMSetUserSetting<bool>>(this, msg => this.SetUserSettingValue(msg.Execute, msg.SettingName, msg.SettingValue));
			Messenger.Default.Register<NMCopyToClipboard>(this, msg => this.CopyTextToClipBoard(msg.TextToCopy));
			
			// Abonnement aux messages pour les dialogues(
			Messenger.Default.Register<NMActionFileDialog<string>>(
				this, 
				(msg) => this.ShowFileDialog(msg.DialogType, msg.ExtensionFichier, msg.NomFichier, msg.Execute)
			);

			Messenger.Default.Register<NMActionFolderDialog<string>>(
				this,
				(msg) => this.ShowFolderBrowserDialog(msg.Execute)
			);

			this.ShowUC(CodesUC.MainScreenCheck);
		}

		private void ShowAboutBox() {
			var about = new AboutBox(this);
			about.ShowDialog();
		}

		private void ShowUC(CodesUC codeUC) {
			this.contenu.Child = this.GetUCFromCode(codeUC);
		}

		private void ShowUCWithParameters(CodesUC codeUC, object objetUC) {
			if(codeUC.Equals(CodesUC.GestionInscriptions) && objetUC is Adherent) {
				this.contenu.Child = new GestionInscriptionsUC(objetUC as Adherent);
			}
			else if(codeUC.Equals(CodesUC.GestionAdherents) && objetUC is Adherent) {
				this.contenu.Child = new GestionAdherentsUC(objetUC as Adherent);
			}
			else if (codeUC.Equals(CodesUC.GestionSaisiesVerifications) && objetUC is CampagneVerification) {
				this.contenu.Child = new GestionSaisiesVerificationsUC(objetUC as CampagneVerification);
			}
		}

		private void OpenWindowUC(CodesUC codeUC, Guid parentGuid) {
			var uc = this.GetUCFromCode(codeUC);

			((VMUCBase)uc.DataContext).IsWindowMode = true;
			((VMUCBase)uc.DataContext).UCParentGuid = parentGuid;

			UCWindow window = new UCWindow(uc);
			window.ShowDialog();
		}

		/// <summary>
		/// Renvoit une instance d'un UC à partir de son code
		/// </summary>
		/// <param name="codeUC">Code de l'UC</param>
		/// <returns>Instance de l'UC</returns>
		private UserControl GetUCFromCode(CodesUC codeUC) {
			UserControl userControl;
			
			switch (codeUC) {
				case CodesUC.GestionInfosClub:
					userControl = new GestionInfosClubUC();
					break;

				case CodesUC.GestionVilles:
					userControl = new GestionVillesUC();
					break;

				case CodesUC.GestionSaisons:
					userControl = new GestionSaisonsUC();
					break;
					
				case CodesUC.GestionAdherents:
					userControl = new GestionAdherentsUC();
					break;

				case CodesUC.GestionInscriptions:
					userControl = new GestionInscriptionsUC();
					break;

				case CodesUC.GestionGroupes:
					userControl = new GestionGroupesUC();
					break;

				case CodesUC.GestionSections:
					userControl = new GestionSectionsUC();
					break;
					
				case CodesUC.GestionTranchesAge:
					userControl = new GestionTranchesAgeUC();
					break;

				case CodesUC.GestionMarques:
					userControl = new GestionMarquesUC();
					break;
					
				case CodesUC.GestionDureesDeVie:
					userControl = new GestionDureesDeVieUC();
					break;
					
				case CodesUC.GestionCategories:
					userControl = new GestionCategoriesUC();
					break;
					
				case CodesUC.GestionLocalisations:
					userControl = new GestionLocalisationsUC();
					break;
					
				case CodesUC.GestionModeles:
					userControl = new GestionModelesUC();
					break;
					
				case CodesUC.GestionEquipements:
					userControl = new GestionEquipementsUC();
					break;
					
				case CodesUC.GestionLocalisationsEquipements:
					userControl = new GestionLocalisationsEquipementsUC();
					break;
					
				case CodesUC.GestionCampagnesVerification:
					userControl = new GestionCampagnesVerificationUC();
					break;

				case CodesUC.EcranStatistiques:
					userControl = new GraphiquesUC();
					break;

				case CodesUC.EcranReporting:
					userControl = new ReportingUC();
					break;

				case CodesUC.NettoyageCNIL:
					userControl = new NettoyageCNILUC();
					break;

				case CodesUC.Backup:
					userControl = new BackupUC();
					break;

				case CodesUC.InitialisationDatabase:
					userControl = new InitialisationDatabaseUC();
					break;
					
				case CodesUC.MainScreenCheck:
				default:
					userControl = new MainScreenCheckUC();
					break;
			}

			return userControl;
		}

		/// <summary>
		/// Quitte l'application
		/// </summary>
		private void Exit() {
			this.Close();
		}

		#region UserSettings
		/// <summary>
		/// Récupère la valeur d'un user setting à partir de son nom
		/// </summary>
		/// <param name="callback">Action a exécuter avec la valeur récupérée</param>
		/// <param name="settingName">Nom du paramètre</param>
		private void GetUserSettingValue(Action<string> callback, string settingName) {
			string result = null;
			
			try {
				result = UserSettings.Default[settingName].ToString();
			} catch (Exception) {
				result = null;
			}
			
			callback(result);
		}
		
		/// <summary>
		/// Enregistre un setting
		/// </summary>
		/// <param name="callback">Action à exécuter une fois l'enregistrement effectué</param>
		/// <param name="settingName">Nom du setting</param>
		/// <param name="settingValue">Valeur du setting</param>
		private void SetUserSettingValue(Action<bool> callback, string settingName, string settingValue) {
			var result = false;
			
			try {
				UserSettings.Default[settingName] = settingValue;
				UserSettings.Default.Save();				
				result = true;
			} catch (Exception) {
				result = false;
			}			
			
			callback(result);
		}
		#endregion
		
		#region dialogs
		public void ShowFileDialog(FileDialogType dialogType, string extensionFichier, string nomFichier, Action<string> callback) {
			switch (dialogType) {
				case FileDialogType.OpenFileDialog:
					this.ShowOpenFileDialog(callback);
					break;

				case FileDialogType.SaveFileDialog:
					this.ShowSaveFileDialog(extensionFichier, nomFichier, callback);
					break;

				default:
					callback(string.Empty);
					break;
			}
		}

		private void ShowSaveFileDialog(string extensionFichier, string nomFichier, Action<string> callback) {
			var dialog = new SaveFileDialog()
			{
				FileName = nomFichier
			};

			dialog.Filter = string.Format(Properties.Resources.FileDialogFilter, extensionFichier);
			dialog.RestoreDirectory = true;

			string filePath = (dialog.ShowDialog().Value) ? dialog.FileName : null;

			callback(filePath);
		}

		private void ShowOpenFileDialog(Action<string> callback) {
			var dialog = new OpenFileDialog();

			dialog.RestoreDirectory = true;

			string filePath = (dialog.ShowDialog().Value) ? dialog.FileName : null;

			callback(filePath);
		}

		private void ShowFolderBrowserDialog(Action<string> callback) {
			Forms.FolderBrowserDialog dialog = new Forms.FolderBrowserDialog();
			dialog.ShowNewFolderButton = true;

			string selectedFolder = (dialog.ShowDialog() == Forms.DialogResult.OK) ? dialog.SelectedPath : null;

			callback(selectedFolder);
		}

		private void ShowConfirmationDialog(Action<bool> callback, string text) {
			var response = MessageBox.Show(text, "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);

			callback(response == MessageBoxResult.Yes);
		}
		#endregion
		
		#region gestion du presse papier
		private void CopyTextToClipBoard(string textToCopy) {
			Clipboard.SetText(textToCopy);
		}
		#endregion
	}
}
