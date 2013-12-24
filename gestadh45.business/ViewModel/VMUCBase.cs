using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using gestadh45.business.Enums;
using gestadh45.business.PersonalizedMsg;
using gestadh45.dal;
using System;
using System.Windows.Input;
namespace gestadh45.business.ViewModel
{
	public abstract class VMUCBase : VMApplicationBase, IDisposable
	{
		/// <summary>
		/// Obtient/Définit le code de l'UC parent
		/// </summary>
		public CodesUC UCParentCode { get; set; }

		/// <summary>
		/// Obtient/Définit le code de l'UC courant
		/// </summary>
		public CodesUC UCCode { get; set; }
		
		/// <summary>
		/// GUID de l'UC
		/// </summary>
		public Guid UCGuid { get; internal set; }

		/// <summary>
		/// GUID de l'UC parent
		/// </summary>
		public Guid UCParentGuid { get; set; }

		/// <summary>
		/// Obtient/Définit un booléen indiquant si l'UC s'affiche dans une fenêtre (True) ou dans l'écran principal
		/// </summary>
		public bool IsWindowMode { get; set; }
		
		/// <summary>
		/// Obtient/Définit le contexte de l'UC
		/// </summary>
		protected GestAdhContext Context { get; set; }

		#region constructeurs
		/// <summary>
		/// Constructeur sans paramètres. Aucun contexte n'est défini
		/// </summary>
		protected VMUCBase() : base() {
			this.UCParentCode = CodesUC.GestionInfosClub;
			this.UCCode = CodesUC.GestionInfosClub;
			this.UCGuid = Guid.NewGuid();
			this.CreateOpenWindowCommand();
			this.CreateReportCommand();

			this.Context = new GestAdhContext();
			this.Context.SetConnection(this.GetUserSetting(ResCommon.Setting_UserConnectionString));

			Messenger.Default.Send(new NMShowInfosDataSource(this.Context.Database.Connection.ConnectionString));
			Messenger.Default.Register<NMResetUC>(this, m => { if (m.CodeUC.Equals(this.UCCode)) this.ResetDatas(); });
		}
		#endregion

		#region OpenWindowCommand
		public ICommand OpenWindowCommand { get; internal set; }

		protected void CreateOpenWindowCommand() {
			this.OpenWindowCommand = new RelayCommand<CodesUC>(
				this.ExecuteOpenWindowCommand,
				this.CanExecuteOpenWindowCommand
			);
		}

		public virtual bool CanExecuteOpenWindowCommand(CodesUC codeUc) {
			return true;
		}

		public virtual void ExecuteOpenWindowCommand(CodesUC codeUc) {
			Messenger.Default.Send<NMOpenWindow>(
				new NMOpenWindow(Tuple.Create(codeUc, this.UCGuid))
			);
		}
		#endregion

		#region ReportCommand
		public ICommand ReportCommand { get; set; }

		protected void CreateReportCommand() {
			this.ReportCommand = new RelayCommand<CodesReport>(
				this.ExecuteReportCommand,
				this.CanExecuteReportCommand
			);
		}

		public virtual bool CanExecuteReportCommand(CodesReport codeReport) {
			return true;
		}

		public virtual void ExecuteReportCommand(CodesReport codeReport) { }
		#endregion

		public void Dispose() {
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}
		
		protected virtual void Dispose(bool disposing) {
			if(disposing) {
				this.Context.Dispose();
			}
		}

		protected virtual void ResetDatas() { }
	}
}
