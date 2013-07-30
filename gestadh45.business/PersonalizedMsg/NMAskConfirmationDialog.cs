using System;
using GalaSoft.MvvmLight.Messaging;

namespace gestadh45.business.PersonalizedMsg
{
	/// <summary>
	/// NotificationMessage demandant l'affichage d'un dialogue de confirmation et traitant la réponse.
	/// </summary>
	/// <typeparam name="TCallbackParameter">Type de paramètre  à passer au callback.</typeparam>
	public class NMAskConfirmationDialog<TCallbackParameter> : NotificationMessageAction<TCallbackParameter>
	{
		/// <summary>
		/// Obtient/Définit le texte à afficher.
		/// </summary>
		/// <value>Le texte à afficher.</value>
		public string Text { get; set; }
		
		/// <summary>
		/// Obtient/Définit le titre du dialogue.
		/// </summary>
		/// <value>Le titre du dialogue.</value>
		public string Caption { get; set; }
		
		/// <summary>
		/// Initialise une nouvelle instance de NMAskConfirmationDialog.
		/// </summary>
		/// <param name="callback">Callback traitant la réponse.</param>
		/// <param name="text">Texte à afficher.</param>
		/// <param name="caption">Titre du dialogue.</param>
		public NMAskConfirmationDialog(Action<TCallbackParameter> callback, string text, string caption)
			: base(NMType.NMAskConfirmationDialog, callback) {
			this.Text = text;
			this.Caption = caption;
		}
	}
}
