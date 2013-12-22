using GalaSoft.MvvmLight.Messaging;
using gestadh45.business.Enums;

namespace gestadh45.business.PersonalizedMsg
{
	/// <summary>
	/// Notification message demandant un rafraîchissement des données de l'écran.
	/// </summary>
	public class NMRefreshDatas : NotificationMessage
	{
		/// <summary>
		/// Obtient/Définit le code de l'UC à rafraîchir
		/// </summary>
		public CodesUC CodeUC { get; set; }
		
		/// <summary>
		/// Initialise une nouvelle instance de NMRefreshDatas.
		/// </summary>
		public NMRefreshDatas(CodesUC codeUc) : base(NMType.NMRefreshDatas) {
			this.CodeUC = codeUc;
		}
	}
}
