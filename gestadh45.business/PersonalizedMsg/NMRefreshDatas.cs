using GalaSoft.MvvmLight.Messaging;

namespace gestadh45.business.PersonalizedMsg
{
	/// <summary>
	/// Notification message demandant un rafraîchissement des données de l'écran.
	/// </summary>
	public class NMRefreshDatas : NotificationMessage
	{
		/// <summary>
		/// Initialise une nouvelle instance de NMRefreshDatas.
		/// </summary>
		public NMRefreshDatas() : base(NMType.NMRefreshDatas) {
		}
	}
}
