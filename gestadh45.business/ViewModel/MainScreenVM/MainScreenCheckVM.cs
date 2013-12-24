using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using gestadh45.business.Enums;
using gestadh45.business.PersonalizedMsg;
using gestadh45.dal;
using gestadh45.services.Database;
using System.Data.Common;
using System.Globalization;
using System.Linq;
using System.Windows.Input;

namespace gestadh45.business.ViewModel.MainScreenVM
{
	public class MainScreenCheckVM : VMUCBase
	{
		#region LastDbConnectionString
		private string _lastDbConnectionString;
		
		/// <summary>
		/// Obtient/Définit la dernière chaîne de connexion
		/// </summary>
		public string LastDbConnectionString {
			get { return this._lastDbConnectionString; }
			set {
				if(this._lastDbConnectionString != value) {
					this._lastDbConnectionString = value;
					this.RaisePropertyChanged(()=>this.LastDbConnectionString);
				}
			}
		}
		#endregion
		
		#region AutoOpenLastDb
		private bool _autoOpenLastDb;
		
		/// <summary>
		/// Obtient/Définit un flag indiquant si il faut charger automatiquement la dernière BDD
		/// </summary>
		public bool AutoOpenLastDb {
			get { return this._autoOpenLastDb; }
			set {
				if(this._autoOpenLastDb != value) {
					this._autoOpenLastDb = value;
					this.RaisePropertyChanged(()=>this.AutoOpenLastDb);
				}
			}
		}
		#endregion
		
		#region Constructeurs
		public MainScreenCheckVM() : base() {
			this.UCCode = CodesUC.MainScreenCheck;

			this.CreateCommands();
			
			this.LastDbConnectionString = this.GetUserSetting(ResCommon.Setting_UserConnectionString);
			
			bool test;
			var resultParse = bool.TryParse(
									this.GetUserSetting(ResCommon.Setting_FlagOpenLastDb), 
									out test
								);
			this.AutoOpenLastDb = resultParse && test;
		}
		#endregion
		
		public void TryAutoOpenDb() {			
			if(this.AutoOpenLastDb && !string.IsNullOrWhiteSpace(this.LastDbConnectionString)) {
				this.CheckDatabase();
			}
		}

		private void CreateCommands() {
			this.CreateOpenLastDbCommand();
			this.CreateOpenDbCommand();
			this.CreateNewDbCommand();
			this.CreateExitCommand();
		}
		
		#region OpenLastDbCommand
		public ICommand OpenLastDbCommand { get; set; }
		
		private void CreateOpenLastDbCommand() {
			this.OpenLastDbCommand = new RelayCommand(
				this.ExecuteOpenLastDbCommand,
				this.CanExecuteOpenLastDbCommand
			);
		}
		
		public void ExecuteOpenLastDbCommand() {
			this.CheckDatabase();
		}
		
		public bool CanExecuteOpenLastDbCommand() {
			return !string.IsNullOrWhiteSpace(this.LastDbConnectionString);
		}
		#endregion
		
		#region OpenDbCommand
		public ICommand OpenDbCommand { get; set; }
		
		private void CreateOpenDbCommand() {
			this.OpenDbCommand = new RelayCommand(
				this.ExecuteOpenDbCommand,
				this.CanExecuteOpenDbCommand
			);
		}
		
		public void ExecuteOpenDbCommand() {
			// envoyer un msg OpenFileDialog
			Messenger.Default.Send(
				new NMActionFileDialog<string>(
					FileDialogType.OpenFileDialog,
					ResCommon.ExtensionBddSqlCe,
					string.Empty,
					callBack => { this.OpenOrCreateDbCommandCallBack(callBack); }
				)
			);
		}
		
		public bool CanExecuteOpenDbCommand() {
			return true;
		}
		#endregion
		
		#region NewDbCommand
		public ICommand NewDbCommand { get; set; }
		
		private void CreateNewDbCommand() {
			this.NewDbCommand = new RelayCommand(
				this.ExecuteNewDbCommand,
				this.CanExecuteNewDbCommand
			);
		}
		
		public void ExecuteNewDbCommand() {
			// envoyer un msg SaveFileDialog
			Messenger.Default.Send(
				new NMActionFileDialog<string>(
					FileDialogType.SaveFileDialog,
					ResCommon.ExtensionBddSqlCe,
					ResCommon.DefaultBddName,
					callBack => { this.OpenOrCreateDbCommandCallBack(callBack); }
				)
			);
		}
		
		public bool CanExecuteNewDbCommand() {
			return true;
		}
		#endregion
		
		#region ExitCommand
		public ICommand ExitCommand { get; set; }
		
		private void CreateExitCommand() {
			this.ExitCommand = new RelayCommand(
				this.ExecuteExitCommand,
				this.CanExecuteExitCommand
			);
		}
		
		public void ExecuteExitCommand() {
			Messenger.Default.Send(new NMCloseApplication());
		}
		
		public bool CanExecuteExitCommand() {
			return true;
		}
		#endregion
		
		private void OpenOrCreateDbCommandCallBack(string dbPath) {
			if(!string.IsNullOrWhiteSpace(dbPath)) {
				this.LastDbConnectionString = string.Format(CultureInfo.CurrentCulture, ResCommon.TemplateSqlCeConnectionString, dbPath);
				this.CheckDatabase();
			}
		}			
				
		#region Méthodes de vérifications de la BDD
		/// <summary>
		/// Lance les vérifications de BDD (connexion puis données) et gère les résultats
		/// </summary>
		private void CheckDatabase() {
			if(!string.IsNullOrWhiteSpace(this.LastDbConnectionString)) {
				this.ClearUserNotifications();
				
				// si le fichier de BDD n'existe pas, on le créé
				if(!this.CheckDbFile()) {
					DatabaseService.CreateSqlCeDatabase(this.LastDbConnectionString);
				}
				
				if(!this.CheckConnection()) { // connexion KO
					this.ShowUserNotification(ResMainScreen.ErrBddNonValide);
					this.SetUserSetting(ResCommon.Setting_UserConnectionString, string.Empty);
					this.LastDbConnectionString = string.Empty;
				}
				else { // connexion OK
					this.SetUserSetting(ResCommon.Setting_FlagOpenLastDb, this.AutoOpenLastDb.ToString());
					this.SetUserSetting(ResCommon.Setting_UserConnectionString, this.LastDbConnectionString);
					
					if(!this.CheckDatas()) { // les données sont KO
						this.ShowUC(CodesUC.InitialisationDatabase);
					}
					else { // données OK
						Messenger.Default.Send(new NMMainMenuState(true));
						this.ShowUC(CodesUC.GestionInfosClub);
					}
				}
			}
		}
		
		/// <summary>
		/// Vérifie si le fichier de base de données existe
		/// </summary>
		/// <returns>True si le fichier existe, False sinon</returns>
		private bool CheckDbFile() {
			return DatabaseService.SqlCeDBExists(this.LastDbConnectionString);
		}
		
		/// <summary>
		/// Vérifie la réussite de la connexion à la BDD
		/// </summary>
		/// <returns>True si la connexion a réussi, False sinon</returns>
		private bool CheckConnection() {
			var result = false;
			
			// on tente d'initialiser le contexte avec la chaîne de connexion
			try {
				this.Context = new GestAdhContext();
				this.Context.SetConnection(this.LastDbConnectionString);
				
				this.Context.Database.Connection.Open();
				this.Context.Database.Connection.Close();
				
				result = true;
			}
			catch (DbException) {
				result = false;
			}
			
			return result;
		}

		/// <summary>
		/// Vérifie la présence de infos obligatoires dans la BDD (ID infos club, Nom club et ville club)
		/// </summary>
		/// <returns>True si les infos sont présentes, False sinon</returns>
		private bool CheckDatas() {
			var result = false;
			var infosClub = this.Context.InfosClub.FirstOrDefault();

			result = infosClub != null;
			result = result && infosClub.ID != null;
			result = result && !string.IsNullOrWhiteSpace(infosClub.Nom);

			// on vérifie que les infos club ont une ville
			result = result && infosClub.Ville != null;

			return result;
		}
		#endregion
	}
}
