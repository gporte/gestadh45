using GalaSoft.MvvmLight.Messaging;

namespace gestadh45.business.PersonalizedMsg
{
	/// <summary>
	/// NotificationMessage demandant le changement d'état du menu principal.
	/// </summary>
	public class NMMainMenuState : NotificationMessage
	{
		/// <summary>
		/// Obtient/Définit un booléen indiquant l'état souhaité du menu principal.
		/// </summary>
		/// <value>Booléen indiquant l'état souhaité du menu principal.</value>
		public bool Enabled { get; set; }

		/// <summary>
		/// Initialisation d'une nouvelle instance de NMMainMenuState.
		/// </summary>
		/// <param name="enabled">Booléen indiquant si le menu doit être activé (true) ou désactive (false).</param>
		public NMMainMenuState(bool enabled)
			: base(NMType.NMMainMenuState) {
				this.Enabled = enabled;
		}
	}
}
