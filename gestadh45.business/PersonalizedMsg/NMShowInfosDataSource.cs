using GalaSoft.MvvmLight.Messaging;

namespace gestadh45.business.PersonalizedMsg
{
	/// <summary>
	/// Notification message demandant l'affichage des infos du datasource.
	/// </summary>
	public class NMShowInfosDataSource : NotificationMessage<string>
	{
		/// <summary>
		/// Initialise une nouvelle instance de NMShowInfosDataSource.
		/// </summary>
		/// <param name="infosDataSource">Infos à afficher.</param>
		public NMShowInfosDataSource(string infosDataSource) : base(infosDataSource, NMType.NMShowInfosDataSource) {
		}
	}
}
