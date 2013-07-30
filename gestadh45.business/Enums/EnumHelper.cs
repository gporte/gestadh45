/*
 * Crée par SharpDevelop.
 * Utilisateur: gp
 * Date: 05/04/2013
 * Heure: 13:51
 * 
 * Pour changer ce modèle utiliser Outils | Options | Codage | Editer les en-têtes standards.
 */
using System.Reflection;
using System.Resources;

namespace gestadh45.business.Enums
{
	/// <summary>
	/// Classe statique fournissant des méthodes utilitaires pour les enums.
	/// </summary>
	public static class EnumHelper
	{
		/// <summary>
		/// Renvoit le libellé associés au code graph.
		/// </summary>
		/// <param name="code">Code graph.</param>
		/// <returns>Libellé associé.</returns>
		public static string GetLibelle(CodesGraph code) {
			var rm = new ResourceManager("gestadh45.business.Enums.LibellesEnums", Assembly.GetExecutingAssembly());
			return rm.GetString("Graph_" + code.ToString()) ?? LibellesEnums.LibelleAbsent;
		}

		/// <summary>
		/// Renvoit le libellé associés au code report.
		/// </summary>
		/// <param name="code">Code report.</param>
		/// <returns>Libellé associé.</returns>
		public static string GetLibelle(CodesReport code) {
			var rm = new ResourceManager("gestadh45.business.Enums.LibellesEnums", Assembly.GetExecutingAssembly());
			return rm.GetString("Report_" + code.ToString()) ?? LibellesEnums.LibelleAbsent;
		}

		/// <summary>
		/// Renvoit le libellé associés au code UC.
		/// </summary>
		/// <param name="code">Code UC.</param>
		/// <returns>Libellé associé.</returns>
		public static string GetLibelle(CodesUC code) {
			var rm = new ResourceManager("gestadh45.business.Enums.LibellesEnums", Assembly.GetExecutingAssembly());
			return rm.GetString("UC_" + code.ToString()) ?? LibellesEnums.LibelleAbsent;
		}
	}
}
