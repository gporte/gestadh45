using System;
using GalaSoft.MvvmLight.Messaging;

namespace gestadh45.business.PersonalizedMsg
{
	/// <summary>
	/// NotificationMessage demandant la fermeture d'une fenêtre.
	/// </summary>
	public class NMCloseWindow : NotificationMessage
	{
		/// <summary>
		/// Obtient/Définit le GUID de l'UC à fermer.
		/// </summary>
		/// <value>GUID de l'UC.</value>
		public Guid UCGuid { get; internal set; }
		
		/// <summary>
		/// Initialise une nouvelle instance de NMCloseWindow.
		/// </summary>
		/// <param name="userControlIdentifier">Identifiant de l'UC.</param>
		public NMCloseWindow(Guid userControlIdentifier) : base(NMType.NMCloseWindow) {
			this.UCGuid = userControlIdentifier;
		}
	}
}
