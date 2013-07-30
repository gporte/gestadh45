using GalaSoft.MvvmLight.Messaging;

namespace gestadh45.business.PersonalizedMsg
{
	/// <summary>
	/// Notification message demandant l'affichage de l'écran "A propos".
	/// </summary>
	public class NMShowAboutBox : NotificationMessage
	{
		/// <summary>
		/// Initialise une nouvelle instance de NMShowAboutBox.
		/// </summary>
		public NMShowAboutBox() : base(NMType.NMShowAboutBox) { 
		}
	}
}
