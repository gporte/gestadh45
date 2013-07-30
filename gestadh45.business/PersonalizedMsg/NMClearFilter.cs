using GalaSoft.MvvmLight.Messaging;

namespace gestadh45.business.PersonalizedMsg
{
	/// <summary>
	/// NotificationMessage demandant l'effacement du filtre.
	/// </summary>
	public class NMClearFilter : NotificationMessage
	{
		/// <summary>
		/// Initialise une nouvelle instance de NMClearFilter.
		/// </summary>
		public NMClearFilter() : base(NMType.NMClearFilter) { 
		}
	}
}
