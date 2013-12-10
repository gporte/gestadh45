using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using gestadh45.business.Enums;
using gestadh45.business.PersonalizedMsg;
using gestadh45.business.ServicesAdapters;
using gestadh45.model;
using gestadh45.services.Reporting;
using gestadh45.services.Reporting.Templates;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using System.Windows.Input;

namespace gestadh45.business.ViewModel.OutilsVM
{
	public class ReportingVM : VMUCBase
	{
		#region ListeReports
		private IOrderedEnumerable<CodesReport> _listeReports;

		/// <summary>
		/// Gets or sets the liste reports.
		/// </summary>
		/// <value>
		/// The liste reports.
		/// </value>
		public IOrderedEnumerable<CodesReport> ListeReports {
			get {
				return this._listeReports;
			}

			set {
				if (this._listeReports != value) {
					this._listeReports = value;
					this.RaisePropertyChanged(() => this.ListeReports);
				}
			}
		}
		#endregion

		#region ReportDatas
		private ICollectionView _reportDatas;

		/// <summary>
		/// Gets or sets the report datas
		/// </summary>
		/// <value>
		/// The report datas
		/// </value>
		public ICollectionView ReportDatas {
			get {
				return this._reportDatas;
			}

			set {
				if (this._reportDatas != value) {
					this._reportDatas = value;
					this.RaisePropertyChanged(() => this.ReportDatas);
				}
			}
		}
		#endregion

		private const string ResourceBaseName = "gestadh45.business.ViewModel.OutilsVM.ResReporting";
		private CodesReport _currentItem;

		#region DataCache
		private ICollection<ReportInventaireEquipementComplet> _cacheInventaireComplet;
		private ICollection<ReportInventaireEquipementSimple> _cacheInventaireSimple;
		private ICollection<ReportListeAdherents> _cacheListeAdherents;
		private ICollection<ReportRepartitionAdherentsAge> _cacheRepartitionAdherentsAge;
		private ICollection<ReportRepartitionAdherentAgeLicenceVille> _cacheRepartitionAdherentsAgeLicenceVille;
		private ICollection<ReportListeAdherents> _cacheCertificatsManquants;
		private ICollection<ReportRepartitionAdherentsGroupes> _cacheRepartitionAdherentsGroupes;
		private ICollection<ReportInscriptionsASuivre> _cacheInscriptionsASuivre;
		#endregion

		#region Constructeur
		public ReportingVM() : base() {
			this.PopulateListeReports();
			this.CreateChangeReportCommand();
		}
		#endregion

		private void PopulateListeReports() {
			// on alimente la liste à partir de l'enum (en retirant les rapports qui nécessitent des paramètres)
			this.ListeReports = Enum.GetValues(typeof(CodesReport))
				.Cast<CodesReport>()
				.Where(r => !r.Equals(CodesReport.VerificationEquipementExcel) && !r.Equals(CodesReport.VieEquipement))
				.OrderBy(c => c);
		}

		#region ChangeReportCommand
		public ICommand ChangeReportCommand {
			get;
			set;
		}

		private void CreateChangeReportCommand() {
			this.ChangeReportCommand = new RelayCommand<CodesReport>(
				this.ExecuteChangeReportCommand,
				this.CanExecuteChangeReportCommand
			);
		}

		public bool CanExecuteChangeReportCommand(CodesReport codeReport) {
			return true;
		}

		public void ExecuteChangeReportCommand(CodesReport codeReport) {
			CollectionViewSource src = new CollectionViewSource();
			this._currentItem = codeReport;

			switch (codeReport) {
				case CodesReport.InventaireSimpleEquipementExcel:
					if (this._cacheInventaireSimple == null) {
						this._cacheInventaireSimple = ServiceReportingAdapter.EquipementsToReportInventaireEquipementSimple(
							this.Context.Equipements.ToList().Where(e => !e.EstAuRebut).OrderBy(e => e.Numero).ToList()
						);
					}

					src.Source = this._cacheInventaireSimple;
					break;

				case CodesReport.InventaireCompletEquipementExcel:
					if (this._cacheInventaireComplet == null) {
						this._cacheInventaireComplet = ServiceReportingAdapter.EquipementsToReportInventaireEquipementComplet(
							this.Context.Equipements.ToList().Where(e => !e.EstAuRebut).OrderBy(e => e.Numero).ToList()
						);
					}

					src.Source = this._cacheInventaireComplet;
					break;

				case CodesReport.ListeAdherents:
					if (this._cacheListeAdherents == null) {
						this._cacheListeAdherents = ServiceReportingAdapter.InscriptionsToListeAdherents(
							this.Context.Inscriptions.ToList()
							.Where(i => 
								i.Groupe.Saison.EstSaisonCourante
								&& i.StatutInscription != StatutInscription.Annulee
							)
							.OrderBy(i => i.Adherent.ToString()).ToList()
						);
					}

					src.Source = this._cacheListeAdherents;
					break;

				case CodesReport.RepartitionAdherentsAge:
					if (this._cacheRepartitionAdherentsAge == null) {
						this._cacheRepartitionAdherentsAge = ServiceReportingAdapter.InscriptionsToReportRepartitionAdherentsAge(
							this.Context.TrancheAges.OrderBy(t => t.AgeInf).ToList(),
							this.Context.InfosClub.FirstOrDefault().Ville,
							this.Context.Inscriptions.ToList().Where(i => 
								i.Groupe.Saison.EstSaisonCourante
								&& i.StatutInscription != StatutInscription.Annulee
							).ToList()
						);
					}


					src.Source = this._cacheRepartitionAdherentsAge;
					break;

				case CodesReport.RepartitionAdherentsAgeLicenceVille:
					if (this._cacheRepartitionAdherentsAgeLicenceVille == null) {
						this._cacheRepartitionAdherentsAgeLicenceVille = ServiceReportingAdapter.InscriptionsToReportRepartitionAdherentsAgeLicenceVille(
							this.Context.TrancheAges.OrderBy(t => t.AgeInf).ToList(),
							this.Context.InfosClub.FirstOrDefault().Ville,
							this.Context.Inscriptions.ToList().Where(i =>
								i.Groupe.Saison.EstSaisonCourante
								&& i.StatutInscription != StatutInscription.Annulee
							).ToList()
						);
					}


					src.Source = this._cacheRepartitionAdherentsAgeLicenceVille;
					break;

				case CodesReport.CertificatsManquants:
					if (this._cacheCertificatsManquants == null) {
						this._cacheCertificatsManquants = ServiceReportingAdapter.InscriptionsToListeAdherents(
							this.Context.Inscriptions.ToList().Where(i => 
								i.Groupe.Saison.EstSaisonCourante
								&& i.StatutInscription != StatutInscription.Annulee
								&& !i.CertificatMedicalRemis
								).ToList()
						);
					}

					src.Source = this._cacheCertificatsManquants;
					break;

				case CodesReport.RepartitionAdherentsGroupes:
					if(this._cacheRepartitionAdherentsGroupes == null) {
						this._cacheRepartitionAdherentsGroupes = ServiceReportingAdapter.InscriptionsToReportRepartitionAhderentsGroupes(
							this.Context.Groupes.Where(x => x.Saison.EstSaisonCourante).ToList()
						);
					}
					
					src.Source = this._cacheRepartitionAdherentsGroupes;
					break;
					
				case CodesReport.InscriptionsASuivre:
					if(this._cacheInscriptionsASuivre == null) {
						this._cacheInscriptionsASuivre = ServiceReportingAdapter.InscriptionsToReportInscriptionsASuivre(
							this.Context.Inscriptions.Where(x => x.Groupe.Saison.EstSaisonCourante && x.StatutInscription == StatutInscription.ASuivre).ToList()
						);
					}
					
					src.Source = this._cacheInscriptionsASuivre;
					break;
					
				default:
					src.Source = null;
					break;
			}


			this.ReportDatas = src.View;
		}
		#endregion

		#region ReportCommand
		public override bool CanExecuteReportCommand(CodesReport codeReport) {
			return this.ReportDatas != null;
		}

		public override void ExecuteReportCommand(CodesReport codeReport) {
			Messenger.Default.Send(
				new NMActionFileDialog<string>(
					FileDialogType.SaveFileDialog,
					ResCommon.ExtensionExcel,
					this._currentItem.ToString(),
					this.GenerateReport
				)
			);
		}

		private void GenerateReport(string nomFichier) {
			if (nomFichier != null) {
				var infoUser = string.Format(CultureInfo.CurrentCulture, ResCommon.InfoRapportGenere, nomFichier);

				switch (this._currentItem) {
					case CodesReport.InventaireSimpleEquipementExcel:
						var genInvSimple = new ReportGenerator<ReportInventaireEquipementSimple>(this._cacheInventaireSimple, nomFichier);
						genInvSimple.SetTitle(this._currentItem.ToString());
						genInvSimple.GenerateExcelReport();
						break;

					case CodesReport.InventaireCompletEquipementExcel:
						var genInvComplet = new ReportGenerator<ReportInventaireEquipementComplet>(this._cacheInventaireComplet, nomFichier);
						genInvComplet.SetTitle(this._currentItem.ToString());
						genInvComplet.GenerateExcelReport();
						break;

					case CodesReport.ListeAdherents:
						var genListAdh = new ReportGenerator<ReportListeAdherents>(this._cacheListeAdherents, nomFichier);
						genListAdh.SetTitle(this._currentItem.ToString());
						genListAdh.GenerateExcelReport();
						break;

					case CodesReport.RepartitionAdherentsAge:
						var genRepAdh = new ReportGenerator<ReportRepartitionAdherentsAge>(this._cacheRepartitionAdherentsAge, nomFichier);
						genRepAdh.SetTitle(this._currentItem.ToString());
						genRepAdh.GenerateExcelReport();
						break;

					case CodesReport.CertificatsManquants:
						var genCertifManq = new ReportGenerator<ReportListeAdherents>(this._cacheCertificatsManquants, nomFichier);
						genCertifManq.SetTitle(this._currentItem.ToString());
						genCertifManq.GenerateExcelReport();
						break;

					case CodesReport.RepartitionAdherentsGroupes:
						var genRepAdhGroupe = new ReportGenerator<ReportRepartitionAdherentsGroupes>(this._cacheRepartitionAdherentsGroupes, nomFichier);
						genRepAdhGroupe.SetTitle(this._currentItem.ToString());
						genRepAdhGroupe.GenerateExcelReport();
						break;
						
					case CodesReport.InscriptionsASuivre:
						var genInscriptionsASuivre = new ReportGenerator<ReportInscriptionsASuivre>(this._cacheInscriptionsASuivre, nomFichier);
						genInscriptionsASuivre.SetTitle(this._currentItem.ToString());
						genInscriptionsASuivre.GenerateExcelReport();
						break;
						
					default:
						infoUser = ResReporting.ErrGenerationCodeInconnu;
						break;
				}

				this.ShowUserNotification(infoUser);
			}
		}
		#endregion
	}
}
