/*
 * Crée par SharpDevelop.
 * Utilisateur: gp
 * Date: 05/04/2013
 * Heure: 13:51
 * 
 * Pour changer ce modèle utiliser Outils | Options | Codage | Editer les en-têtes standards.
 */
using System;
using GalaSoft.MvvmLight.Messaging;
using gestadh45.business.Enums;

namespace gestadh45.business.PersonalizedMsg
{	
	/// <summary>
	/// NotificationMessage pour demander l'ouverture d'un file dialog spécifié et récupérer son résultat.
	/// </summary>
	/// <typeparam name="TCallbackParameter">Type du paramètre de callback.</typeparam>
	public class NMActionFileDialog<TCallbackParameter> : NotificationMessageAction<TCallbackParameter>
	{
		/// <summary>
		/// Obtient/Définit l'extension du fichier.
		/// </summary>
		/// <value>L'extension de fichier.</value>
		public string ExtensionFichier {
			get;
			set;
		}

		/// <summary>
		/// Obtient/Définit le nom du fichier.
		/// </summary>
		/// <value>Le nom de fichier.</value>
		public string NomFichier {
			get;
			set;
		}

		/// <summary>
		/// Obtient/Définit le type de FileDialog.
		/// </summary>
		/// <value>Le type de FileDialog.</value>
		public FileDialogType DialogType {
			get;
			set;
		}
		
		/// <summary>
		/// Initialise une nouvelle instance de NMActionFileDialog.
		/// </summary>
		/// <param name="dialogType">Type de FileDialog.</param>
		/// <param name="extensionFichier">Extension de fichier du filtre.</param>
		/// <param name="nomFichier">Nom de fichier par défaut.</param>
		/// <param name="callback">Callback pour traiter le résultat.</param>
		public NMActionFileDialog(FileDialogType dialogType, string extensionFichier, string nomFichier, Action<TCallbackParameter> callback)
			: base(NMType.NMActionFileDialog, callback) {
				this.ExtensionFichier = extensionFichier;
				this.NomFichier = nomFichier;
				this.DialogType = dialogType;
		}
	}
}
