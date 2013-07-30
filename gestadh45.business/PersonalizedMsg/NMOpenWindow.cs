using System;
using GalaSoft.MvvmLight.Messaging;
using gestadh45.business.Enums;

namespace gestadh45.business.PersonalizedMsg
{
	/// <summary>
	/// Notification message demandant l'ouverture d'une fenêtre en spécifiant l'UC qu'elle doit contenir.
	/// </summary>
	public class NMOpenWindow : NotificationMessage
	{
		/// <summary>
		/// Obtient/Définit le code de l'UC à ouvrir en mode fenêtre.
		/// </summary>
		/// <value>Code de l'UC à ouvrir.</value>
		public CodesUC CodeUC { get; set; }

		/// <summary>
		/// Obtient/Définit le GUID de l'UC qui a demandé l'ouverture de la fenêtre.
		/// </summary>
		/// <value>GUID de l'appelant.</value>
		public Guid ParentGuid { get; set; }

		/// <summary>
		/// Initialise une nouvelle instance de NMOpenWindow.
		/// </summary>
		/// <param name="parameters">Code de l'UC à ouvrir et GUID de l'appelant.</param>
		public NMOpenWindow(Tuple<CodesUC, Guid> parameters)
			: base(NMType.NMOpenWindow) {
			this.CodeUC = parameters.Item1;
			this.ParentGuid = parameters.Item2;
		}
	}
}
