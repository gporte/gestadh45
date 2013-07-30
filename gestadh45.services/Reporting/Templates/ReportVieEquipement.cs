
using System.ComponentModel.DataAnnotations;
namespace gestadh45.services.Reporting.Templates
{
	public class ReportVieEquipement : ITemplateReport
	{
		[Display(ResourceType = typeof(ResReports), Name = "HeaderDate")]
		public string Date { get; set; }

		[Display(ResourceType = typeof(ResReports), Name = "HeaderEvenement")]
		public string Evenement { get; set; }
	}
}
