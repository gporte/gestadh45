﻿using GalaSoft.MvvmLight.Messaging;
using gestadh45.business.Enums;

namespace gestadh45.business.PersonalizedMsg
{
	/// <summary>
	/// Notification message demandant la réinitialisation d'un UC
	/// </summary>
	public class NMResetUC : NotificationMessage
	{
		/// <summary>
		/// Obtient/Définit le code de l'UC à rafraîchir
		/// </summary>
		public CodesUC CodeUC { get; set; }
		
		/// <summary>
		/// Initialise une nouvelle instance de NMRefreshDatas.
		/// </summary>
		public NMResetUC(CodesUC codeUc)
			: base(NMType.NMRefreshDatas) {
			this.CodeUC = codeUc;
		}
	}
}
