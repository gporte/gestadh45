using GalaSoft.MvvmLight.Messaging;

namespace gestadh45.business.PersonalizedMsg
{
	/// <summary>
	/// NotificationMessage demandant la fermeture de l'application.
	/// </summary>
	public class NMCloseApplication : NotificationMessage
	{
		/// <summary>
		/// Initialise une nouvelle instance de NMCloseApplication.
		/// </summary>
		public NMCloseApplication() : base(NMType.NMCloseApplication) {
		}
	}
}
