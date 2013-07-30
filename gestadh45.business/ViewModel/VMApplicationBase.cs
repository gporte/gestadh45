using System.Globalization;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using gestadh45.business.Enums;
using gestadh45.business.PersonalizedMsg;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace gestadh45.business.ViewModel
{
	public abstract class VMApplicationBase : ViewModelBase
	{
		protected VMApplicationBase() {
			this.CreateShowUCCommand();
			this.CreateCopyValueCommand();
		}

		#region ShowUCCommand
		public ICommand ShowUCCommand { get; internal set; }

		private void CreateShowUCCommand() {
			this.ShowUCCommand = new RelayCommand<CodesUC>(
				this.ExecuteShowUCCommand,
				this.CanExecuteShowUCCommand
			);
		}

		public bool CanExecuteShowUCCommand(CodesUC codeUC) {
			return true;
		}

		public void ExecuteShowUCCommand(CodesUC codeUC) {
			this.ClearUserNotifications();
			this.ShowUC(codeUC);
		}
		#endregion

		#region Affichage des UC
		protected void ShowUC(CodesUC codeUC) {
			Messenger.Default.Send(new NMShowUC(codeUC));
		}
		#endregion
		
		#region CopyValueCommand
		public ICommand CopyValueCommand { get; set; }
		
		private void CreateCopyValueCommand() {
			this.CopyValueCommand = new RelayCommand<string>(
				this.ExecuteCopyValueCommand,
				this.CanExecuteCopyValueCommand
			);
		}
		
		public void ExecuteCopyValueCommand(string valueToCopy) {
			Messenger.Default.Send(new NMCopyToClipboard(valueToCopy));			
			
			this.ShowUserNotification(
				string.Format(CultureInfo.CurrentCulture, "\"{0}\" copié dans le presse-papier.", valueToCopy)
			);
		}
		
		public bool CanExecuteCopyValueCommand(string valueToCopy) {
			return valueToCopy != null;
		}
		#endregion

		#region Affichage des notifications utilisateur
		protected void ShowUserNotification(string notification) {
			Messenger.Default.Send(new NMUserNotification(notification));
		}

		protected void ShowUserNotifications(IEnumerable<string> notifications) {
			StringBuilder sb = new StringBuilder();
			foreach (string notif in notifications) {
				sb.AppendLine(notif);
			}
			this.ShowUserNotification(sb.ToString());
		}

		protected void ClearUserNotifications() {
			this.ShowUserNotification(string.Empty);
		}
		#endregion
		
		#region gestion des settings
		/// <summary>
		/// Essaie de récupérer la valeur d'un unser setting à partir de son nom
		/// </summary>
		/// <param name="settingName">Nom du setting</param>
		/// <returns>Valeur récupérée</returns>
		public string GetUserSetting(string settingName) {
			string result = null;
			
			Messenger.Default.Send(
				new NMGetUserSetting<string>(
					settingName,
					callback => { result = callback; }
				)
			);
			
			return result;
		}
		
		/// <summary>
		/// Définit la valeur d'un user setting
		/// </summary>
		/// <param name="settingName">Nom du setting</param>
		/// <param name="settingValue">Valeur du setting</param>
		/// <returns>Booléen indiquant la réussite ou l'échec de la définition</returns>
		public bool SetUserSetting(string settingName, string settingValue) {
			bool result = false;
			
			Messenger.Default.Send(
				new NMSetUserSetting<bool>(
					settingName,
					settingValue,
					callback => { result = callback; }
				)
			);
			
			return result;
		}
		#endregion
	}
}
