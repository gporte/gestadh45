/*
 * Crée par SharpDevelop.
 * Utilisateur: gp
 * Date: 04/10/2013
 * Heure: 10:28
 * 
 * Pour changer ce modèle utiliser Outils | Options | Codage | Editer les en-têtes standards.
 */
using System;
using System.ComponentModel.DataAnnotations;

namespace gestadh45.services.Reporting.Templates
{
	/// <summary>
	/// Description of ReportInscriptionsASuivre.
	/// </summary>
	public class ReportInscriptionsASuivre : ITemplateReport
	{
		[Display (ResourceType=typeof(ResReports), Name="HeaderNom")]
		public string Nom { get; set; }

		[Display(ResourceType = typeof(ResReports), Name = "HeaderPrenom")]
		public string Prenom { get; set; }
		
		[Display(ResourceType = typeof(ResReports), Name = "HeaderGroupe")]
		public string Groupe { get; set; }
		
		[Display(ResourceType = typeof(ResReports), Name = "HeaderDateCreation")]
		public string DateCreation { get; set; }
		
		[Display(ResourceType = typeof(ResReports), Name = "HeaderDateModification")]
		public string DateModification { get; set; }
		
		[Display(ResourceType = typeof(ResReports), Name = "HeaderCommentaire")]
		public string Commentaire { get; set; }
	}
}
