using GalaSoft.MvvmLight.Messaging;

namespace gestadh45.business.PersonalizedMsg
{
	/// <summary>
	/// Notification message demandant la sélection d'un élément.
	/// </summary>
	/// <typeparam name="TElement">Type de l'élément à sélectionner.</typeparam>
	public class NMSelectionElement<TElement> : NotificationMessage<TElement>
	{
		/// <summary>
		/// Initialise une nouvelle instance de NMSelectionElement.
		/// </summary>
		/// <param name="element">Element à sélectionner.</param>
		public NMSelectionElement(TElement element) : base(element, NMType.NMSelectionElement) {
		}
	}
}
