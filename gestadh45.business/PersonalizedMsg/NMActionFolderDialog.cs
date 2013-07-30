using System;
using GalaSoft.MvvmLight.Messaging;

namespace gestadh45.business.PersonalizedMsg
{
	/// <summary>
	/// NotificationMessage pour demander l'ouverture d'une FolderDialog et récupérer son résultat.
	/// </summary>
	/// <typeparam name="TCallbackParameter">Type de paramètre à traiter par le callback.</typeparam>
	public class NMActionFolderDialog<TCallbackParameter> : NotificationMessageAction<TCallbackParameter>
	{
		/// <summary>
		/// Initialise une nouvelle instance de NMActionFolderDialog.
		/// </summary>
		/// <param name="callback">Callback traitant le résultat.</param>
		public NMActionFolderDialog(Action<TCallbackParameter> callback)
			: base(NMType.NMActionFolderDialog, callback) {
		}
	}
}
