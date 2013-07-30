using GalaSoft.MvvmLight.Messaging;
using gestadh45.business.Enums;

namespace gestadh45.business.PersonalizedMsg
{
	public class NMShowUC : NotificationMessage
	{
		/// <summary>
		/// Obtient/Définit le code de l'UC à afficher
		/// </summary>
		public CodesUC CodeUC { get; set; }

		public NMShowUC(CodesUC codeUC)
			: base(NMType.NMShowUC) {
			this.CodeUC = codeUC;
		}
	}

	public class NMShowUC<TContent> : NotificationMessage<TContent>
	{
		/// <summary>
		/// Obtient/Définit le code de l'UC à afficher
		/// </summary>
		public CodesUC CodeUC { get; set; }

		public NMShowUC(CodesUC codeUC, TContent content)
			: base(content, NMType.NMShowUC) {
				this.CodeUC = codeUC;
		}
	}
}
