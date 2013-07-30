using System.ComponentModel.DataAnnotations;

namespace gestadh45.services.Reporting.Templates
{
	public class ReportRepartitionAdherentAgeLicenceVille : ITemplateReport
	{
		[Display(ResourceType = typeof(ResReports), Name = "HeaderLibelle")]
		public string Libelle { get; set; }

		[Display(ResourceType = typeof(ResReports), Name = "HeaderResidentNonLicencie")]
		public int NbResidentNonLicencie { get; set; }

		[Display(ResourceType = typeof(ResReports), Name = "HeaderResidentLicencie")]
		public int NbResidentLicencie { get; set; }

		[Display(ResourceType = typeof(ResReports), Name = "HeaderExterieurNonLicencie")]
		public int NbExterieurNonLicencie { get; set; }

		[Display(ResourceType = typeof(ResReports), Name = "HeaderExterieurLicencie")]
		public int NbExterieurLicencie { get; set; }

		[Display(ResourceType = typeof(ResReports), Name = "HeaderTotalNonLicencie")]
		public int NbNonLicencie {
			get { return this.NbResidentNonLicencie + this.NbExterieurNonLicencie; }
		}

		[Display(ResourceType = typeof(ResReports), Name = "HeaderTotalLicencie")]
		public int NbLicencie {
			get {
				return this.NbResidentLicencie + this.NbExterieurLicencie;
			}
		}

		[Display(ResourceType = typeof(ResReports), Name = "HeaderTotal")]
		public int NbTotal {
			get {
				return this.NbNonLicencie + this.NbLicencie;
			}
		}
	}
}
