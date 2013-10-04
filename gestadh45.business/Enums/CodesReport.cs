/*
 * Crée par SharpDevelop.
 * Utilisateur: gp
 * Date: 05/04/2013
 * Heure: 13:51
 * 
 * Pour changer ce modèle utiliser Outils | Options | Codage | Editer les en-têtes standards.
 */
namespace gestadh45.business.Enums
{
	/// <summary>
	/// Codes rapports.
	/// </summary>
	public enum CodesReport
	{
		/// <summary>
		/// Inventaire simple (Excel).
		/// </summary>
		InventaireSimpleEquipementExcel,
		
		/// <summary>
		/// Inventaire complet (Excel).
		/// </summary>
		InventaireCompletEquipementExcel,
		
		/// <summary>
		/// Rapport de campagne de vérification (Excel).
		/// </summary>
		VerificationEquipementExcel,
		
		/// <summary>
		/// Liste des adhérents.
		/// </summary>
		ListeAdherents,
		
		/// <summary>
		/// Répartition des adhérents par tranches d'âge.
		/// </summary>
		RepartitionAdherentsAge,
		
		/// <summary>
		/// Vie d'un équipement.
		/// </summary>
		VieEquipement,
		
		/// <summary>
		/// Certificats médicaux manquants.
		/// </summary>
		CertificatsManquants,
		
		/// <summary>
		/// Répartition des adhérents par âge/licence/ville.
		/// </summary>
		RepartitionAdherentsAgeLicenceVille,
		
		/// <summary>
		/// Répartition des adhérents par groupe.
		/// </summary>
		RepartitionAdherentsGroupes,
		
		/// <summary>
		/// Inscriptions à suivre
		/// </summary>
		InscriptionsASuivre
	}
}
