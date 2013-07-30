/*
 * Crée par SharpDevelop.
 * Utilisateur: gp
 * Date: 02/20/2013
 * Heure: 16:55
 * 
 * Pour changer ce modèle utiliser Outils | Options | Codage | Editer les en-têtes standards.
 */
using System;
using GalaSoft.MvvmLight.Messaging;

namespace gestadh45.business.PersonalizedMsg
{
	/// <summary>
	/// Description of NMCopyToClipboard.
	/// </summary>
	public class NMCopyToClipboard : NotificationMessage
	{
		/// <summary>
		/// Obtient.Définit le texte à copier dans le presse papier.
		/// </summary>
		/// <value>Texte à copier.</value>
		public string TextToCopy { get; set; }
		
		/// <summary>
		/// Initialise une nouvelle instance de NMCopyToClipboard.
		/// </summary>
		/// <param name="textToCopy">Texte à copier dans le presse-papier.</param>
		public NMCopyToClipboard(string textToCopy) : base(NMType.NMCopyToClipboard) {
			this.TextToCopy = textToCopy;
		}
	}
}
