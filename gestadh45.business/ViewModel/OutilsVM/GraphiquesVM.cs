using GalaSoft.MvvmLight.Command;
using gestadh45.business.Enums;
using gestadh45.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace gestadh45.business.ViewModel.OutilsVM
{
	public class GraphiquesVM : VMUCBase
	{
		#region ListeGraphs
		private IOrderedEnumerable<CodesGraph> _listeGraphs;

		/// <summary>
		/// Gets or sets the liste graphs.
		/// </summary>
		/// <value>
		/// The liste graphs.
		/// </value>
		public IOrderedEnumerable<CodesGraph> ListeGraphs {
			get {
				return this._listeGraphs;
			}

			set {
				if(this._listeGraphs != value) {
					this._listeGraphs = value;
					this.RaisePropertyChanged(()=>this.ListeGraphs);
				}
			}
		}
		#endregion

		#region ChartKeysValues
		private IEnumerable<KeyValuePair<string, int>> _chartKeysValues;

		/// <summary>
		/// Gets or sets the chart keys values.
		/// </summary>
		/// <value>
		/// The chart keys values.
		/// </value>
		public IEnumerable<KeyValuePair<string, int>> ChartKeysValues {
			get {
				return this._chartKeysValues;
			}

			set {
				if (this._chartKeysValues != value) {
					this._chartKeysValues = value;
					this.RaisePropertyChanged(() => this.ChartKeysValues);
				}
			}
		}
		#endregion

		#region TitreGraph
		private string _titreGraph;

		/// <summary>
		/// Gets or sets the titre graph.
		/// </summary>
		/// <value>
		/// The titre graph.
		/// </value>
		public string TitreGraph {
			get {
				return this._titreGraph;
			}

			set {
				if (this._titreGraph != value) {
					this._titreGraph = value;
					this.RaisePropertyChanged(() => this.TitreGraph);
				}
			}
		}
		#endregion

		#region private fields
		private List<Groupe> _groupesSaisonCourante;
		private List<Inscription> _inscriptionsSaisonCourante;
		#endregion

		private const string ResourceBaseName = "gestadh45.business.ViewModel.OutilsVM.ResGraphiques";

		public GraphiquesVM() : base() {
			this.UCCode = CodesUC.Graphiques;

			this._groupesSaisonCourante = this.Context.Groupes
				.Where(grp => grp.Saison.EstSaisonCourante)
				.OrderBy(grp => grp.JourSemaine)
				.ThenBy(grp => grp.HeureDebut)
				.ToList();

			this._inscriptionsSaisonCourante = this.Context.Inscriptions.ToList()
				.Where(ins => ins.Groupe.Saison.EstSaisonCourante && ins.StatutInscription != StatutInscription.Annulee)
				.ToList();

			this.PopulateListeGraphs();
			this.CreateChangeChartCommand();
		}

		#region ChangeChartCommand
		public ICommand ChangeChartCommand {
			get;
			set;
		}

		private void CreateChangeChartCommand() {
			this.ChangeChartCommand = new RelayCommand<CodesGraph>(
				this.ExecuteChangeChartCommand,
				this.CanExecuteChangeChartCommand
			);
		}

		public bool CanExecuteChangeChartCommand(CodesGraph codeGraph) {
			return true;
		}

		public void ExecuteChangeChartCommand(CodesGraph codeGraph) {
			this.TitreGraph = codeGraph.ToString();

			switch (codeGraph) {
				case CodesGraph.RemplissageGroupes:
					this.ChartKeysValues = this.GetRemplissageGroupes();
					break;

				case CodesGraph.RepartitionHommesFemmes:
					this.ChartKeysValues = this.GetRepartitionHommeFemmes();
					break;

				case CodesGraph.RepartitionMajeursMineurs:
					this.ChartKeysValues = this.GetRepartitionMajeursMineurs();
					break;

				case CodesGraph.RepartitionResidentsExterieurs:
					this.ChartKeysValues = this.GetRepartitionResidentsExterieurs();
					break;

				case CodesGraph.RepartitionAdherentsVilles:
					this.ChartKeysValues = this.GetRepartitionAdherentsVilles();
					break;

				case CodesGraph.RepartitionLicencies:
					this.ChartKeysValues = this.GetRepartitionLicencies();
					break;

				case CodesGraph.RepartitionSections:
					this.ChartKeysValues = this.GetRepartitionSections();
					break;

				default:
					this.ChartKeysValues = null;
					break;
			}
		}
		#endregion

		private void PopulateListeGraphs() {
			this.ListeGraphs = Enum.GetValues(typeof(CodesGraph)).Cast<CodesGraph>().OrderBy(c => c);
		}

		#region Alimentation des graphs
		private List<KeyValuePair<string, int>> GetRemplissageGroupes() {
			List<KeyValuePair<string, int>> keyValues = new List<KeyValuePair<string, int>>();

			foreach (Groupe grp in this._groupesSaisonCourante) {
				int nb = this._inscriptionsSaisonCourante.Count(ins => ins.Groupe.ID == grp.ID);

				keyValues.Add(new KeyValuePair<string, int>(grp.ToString(), nb));
			}

			return keyValues;
		}

		private List<KeyValuePair<string, int>> GetRepartitionHommeFemmes() {
			List<KeyValuePair<string, int>> keyValues = new List<KeyValuePair<string, int>>();

			int nbM = this._inscriptionsSaisonCourante.Count(ins => ins.Adherent.Sexe == Sexe.Masculin);
			int nbF = this._inscriptionsSaisonCourante.Count(ins => ins.Adherent.Sexe == Sexe.Feminin);

			keyValues.Add(new KeyValuePair<string, int>(Sexe.Masculin.ToString(), nbM));
			keyValues.Add(new KeyValuePair<string, int>(Sexe.Feminin.ToString(), nbF));

			return keyValues;
		}

		private List<KeyValuePair<string, int>> GetRepartitionMajeursMineurs() {
			List<KeyValuePair<string, int>> keyValues = new List<KeyValuePair<string, int>>();

			int nbMineurs = this._inscriptionsSaisonCourante.Count(ins => ins.Adherent.Age < 18);
			int nbMajeurs = this._inscriptionsSaisonCourante.Count(ins => ins.Adherent.Age >= 18);

			keyValues.Add(new KeyValuePair<string, int>(ResGraphiques.LibelleMineurs, nbMineurs));
			keyValues.Add(new KeyValuePair<string, int>(ResGraphiques.LibelleMajeurs, nbMajeurs));

			return keyValues;
		}

		private List<KeyValuePair<string, int>> GetRepartitionResidentsExterieurs() {
			List<KeyValuePair<string, int>> keyValues = new List<KeyValuePair<string, int>>();

			Ville villeClub = this.Context.InfosClub.FirstOrDefault().Ville;

			int nbResidents = this._inscriptionsSaisonCourante.Count(ins => ins.Adherent.Ville.ID == villeClub.ID);
			int nbExterieurs = this._inscriptionsSaisonCourante.Count(ins => ins.Adherent.Ville.ID != villeClub.ID);

			keyValues.Add(new KeyValuePair<string, int>(ResGraphiques.LibelleResidents, nbResidents));
			keyValues.Add(new KeyValuePair<string, int>(ResGraphiques.LibelleExtérieurs, nbExterieurs));

			return keyValues;
		}

		private List<KeyValuePair<string, int>> GetRepartitionAdherentsVilles() {
			List<KeyValuePair<string, int>> keyValues = new List<KeyValuePair<string, int>>();

			foreach (Ville ville in this.Context.Villes) {
				var nbAdh = this._inscriptionsSaisonCourante.Count(i => i.Adherent.Ville.ID == ville.ID);
				keyValues.Add(new KeyValuePair<string, int>(ville.Libelle, nbAdh));
			}

			return keyValues;
		}

		private List<KeyValuePair<string, int>> GetRepartitionLicencies() {
			var keyValues = new List<KeyValuePair<string, int>>();

			int nbLicencies = this._inscriptionsSaisonCourante.Count(i => i.Groupe.Saison.EstSaisonCourante && i.EstLicencie);
			int nbNonLicencies = this._inscriptionsSaisonCourante.Count(i => i.Groupe.Saison.EstSaisonCourante && !i.EstLicencie);

			keyValues.Add(new KeyValuePair<string,int>(ResGraphiques.LibelleLicencies, nbLicencies));
			keyValues.Add(new KeyValuePair<string, int>(ResGraphiques.LibelleNonLicencies, nbNonLicencies));

			return keyValues;
		}

		private List<KeyValuePair<string, int>> GetRepartitionSections() {
			var keyValues = new List<KeyValuePair<string, int>>();

			foreach (var section in this.Context.Sections) {
				keyValues.Add(
					new KeyValuePair<string, int>(
						section.Libelle, 
						this._inscriptionsSaisonCourante.Count(i => i.Section.ID == section.ID))
				);
			}

			return keyValues;
		}
		#endregion
	}
}
