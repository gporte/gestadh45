/*
 * Crée par SharpDevelop.
 * Utilisateur: gp
 * Date: 22/07/2013
 * Heure: 09:36
 * 
 * Pour changer ce modèle utiliser Outils | Options | Codage | Editer les en-têtes standards.
 */
using System;
using System.ComponentModel.DataAnnotations;

namespace gestadh45.services.Reporting.Templates
{
	/// <summary>
	/// Description of ReportRepartitionAdherentsGroupes.
	/// </summary>
	public class ReportRepartitionAdherentsGroupes : ITemplateReport
	{
		[Display(ResourceType = typeof(ResReports), Name = "HeaderGroupe")]
		public string Groupe { get; set; }
		
		[Display(ResourceType = typeof(ResReports), Name = "HeaderNbPlaces")]
		public int NbPlaces { get; set; }
		
		[Display(ResourceType = typeof(ResReports), Name = "HeaderNbInscriptions")]
		public int NbInscriptions { get; set; }
		
		[Display(ResourceType = typeof(ResReports), Name = "HeaderNbPlacesDispo")]
		public int NbPlacesDispo { 
			get{
				return this.NbPlaces - this.NbInscriptions;
			} 
		}
	}
}
