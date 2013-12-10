using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using gestadh45.business.Enums;
using gestadh45.business.ServicesAdapters;
using gestadh45.services.Backup;
using System;
using System.Globalization;
using System.Linq;
using System.Windows.Input;

namespace gestadh45.business.ViewModel.OutilsVM
{
	public class BackupVM : VMUCBase
	{
		#region Constructeur
		public BackupVM() : base() {
			this.CreateCommands();
		}
		#endregion

		private void CreateCommands() {
			this.CreateBackupBDDCommand();
			this.CreateExportAdherentsXMLCommand();
			this.CreateExportInscriptionsSaisonCouranteXMLCommand();
			this.CreateExportVillesXMLCommand();
		}

		#region BackupBDDCommand
		public ICommand BackupBDDCommand { get; set; }

		private void CreateBackupBDDCommand() {
			this.BackupBDDCommand = new RelayCommand(
				this.ExecuteBackupBDDCommand,
				this.CanExecuteBackupBDDCommand
			);
		}

		public bool CanExecuteBackupBDDCommand() {
			return true;
		}

		public void ExecuteBackupBDDCommand() {
			Messenger.Default.Send(
				new PersonalizedMsg.NMActionFileDialog<string>(
					FileDialogType.SaveFileDialog,					
					ResBackup.SDFExtension, 
					string.Format(
						CultureInfo.CurrentCulture, 
						ResBackup.DefaultDatabaseName, 
						DateTime.Now.ToString("yyyyMMdd", CultureInfo.CurrentCulture)
					),
					this.BackupBDD
				)
			);
		}

		private void BackupBDD(string filePath) {
			if (!string.IsNullOrWhiteSpace(filePath)) {
				var currentBdPath = this.Context.Database.Connection.DataSource;
				DatabaseExporter.Export(currentBdPath, filePath);
				this.ShowUserNotification(string.Format(CultureInfo.CurrentCulture, ResBackup.InfoBackupBdd, filePath));
			}
		}
		#endregion

		#region ExportAdherentsXMLCommand
		public ICommand ExportAdherentsXMLCommand { get; set; }

		private void CreateExportAdherentsXMLCommand() {
			this.ExportAdherentsXMLCommand = new RelayCommand(
				this.ExecuteExportAdherentsXMLCommand,
				this.CanExecuteExportAdherentsXMLCommand
			);
		}

		public bool CanExecuteExportAdherentsXMLCommand() {
			return true;
		}

		public void ExecuteExportAdherentsXMLCommand() {
			Messenger.Default.Send(
				new PersonalizedMsg.NMActionFileDialog<string>(
					FileDialogType.SaveFileDialog,
					ResBackup.XMLExtension,
					string.Format(
						CultureInfo.CurrentCulture, 
						ResBackup.DefaultAdherentXMLFileName, 
						DateTime.Now.ToString("yyyyMMdd", CultureInfo.CurrentCulture)
					),
					this.ExportAdherentsXML
				)
			);
		}

		private void ExportAdherentsXML(string filePath) {
			if (!string.IsNullOrWhiteSpace(filePath)) {
				XMLGenerator.ExportAdherentDtos(
					filePath, 
					ServiceBackupAdapter.AdherentsToAdherentDtoArray(this.Context.Adherents)
				);

				this.ShowUserNotification(string.Format(CultureInfo.CurrentCulture, ResBackup.InfoExportAdherentsXML, filePath));
			}
		}
		#endregion

		#region ExportInscriptionsSaisonCouranteXMLCommand
		public ICommand ExportInscriptionsSaisonCouranteXMLCommand { get; set; }

		private void CreateExportInscriptionsSaisonCouranteXMLCommand() {
			this.ExportInscriptionsSaisonCouranteXMLCommand = new RelayCommand(
				this.ExecuteExportInscriptionsSaisonCouranteXMLCommand,
				this.CanExecuteExportInscriptionsSaisonCouranteXMLCommand
			);
		}

		public bool CanExecuteExportInscriptionsSaisonCouranteXMLCommand() {
			return true;
		}

		public void ExecuteExportInscriptionsSaisonCouranteXMLCommand() {
			Messenger.Default.Send(
				new PersonalizedMsg.NMActionFileDialog<string>(
					FileDialogType.SaveFileDialog,
					ResBackup.XMLExtension,
					string.Format(
						CultureInfo.CurrentCulture, 
						ResBackup.DefaultInscriptionXMLFileName, 
						DateTime.Now.ToString("yyyyMMdd", CultureInfo.CurrentCulture)
					),
					this.ExportInscriptionsSaisonCouranteXML
				)
			);
		}

		private void ExportInscriptionsSaisonCouranteXML(string filePath) {
			if (!string.IsNullOrWhiteSpace(filePath)) {
				XMLGenerator.ExportInscriptionDtos(
					filePath,
					ServiceBackupAdapter.InscriptionsToInscriptionDtos(
						this.Context.Inscriptions.Where(i => i.Groupe.Saison.EstSaisonCourante)
					)
				);

				this.ShowUserNotification(string.Format(CultureInfo.CurrentCulture, ResBackup.InfoExportAdherentsXML, filePath));
			}
		}
		#endregion

		#region ExportVillesXMLCommand
		public ICommand ExportVillesXMLCommand { get; set; }

		private void CreateExportVillesXMLCommand() {
			this.ExportVillesXMLCommand = new RelayCommand(
				this.ExecuteExportVillesXMLCommand,
				this.CanExecuteExportVillesXMLCommand
			);
		}

		public bool CanExecuteExportVillesXMLCommand() {
			return true;
		}

		public void ExecuteExportVillesXMLCommand() {
			Messenger.Default.Send(
				new PersonalizedMsg.NMActionFileDialog<string>(
					FileDialogType.SaveFileDialog,
					ResBackup.XMLExtension,
					string.Format(
						CultureInfo.CurrentCulture, 
						ResBackup.DefaultVilleXMLFileName, 
						DateTime.Now.ToString("yyyyMMdd", CultureInfo.CurrentCulture)
					),
					this.ExportVillesXML
				)
			);
		}

		private void ExportVillesXML(string filePath) {
			if (!string.IsNullOrWhiteSpace(filePath)) {
				XMLGenerator.ExportVilleDtos(
					filePath,
					ServiceBackupAdapter.VillesToVilleDtos(
						this.Context.Villes
					)
				);

				this.ShowUserNotification(string.Format(CultureInfo.CurrentCulture, ResBackup.InfoExportAdherentsXML, filePath));
			}
		}
		#endregion
	}
}
