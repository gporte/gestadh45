/*
 * Crée par SharpDevelop.
 * Utilisateur: gp
 * Date: 14/02/2013
 * Heure: 15:40
 * 
 * Pour changer ce modèle utiliser Outils | Options | Codage | Editer les en-têtes standards.
 */
using System;
using GalaSoft.MvvmLight.Messaging;

namespace gestadh45.business.PersonalizedMsg
{
	/// <summary>
	/// Notification message demandant l'enregistrement d'un userSetting et passant le réultat à un callback.
	/// </summary>
	/// <typeparam name="TCallBackParameter">Type du paramètre de retour.</typeparam>
	public class NMSetUserSetting<TCallBackParameter> : NotificationMessageAction<TCallBackParameter>
	{
		/// <summary>
		/// Obtient/Définit le nom du setting.
		/// </summary>
		/// <value>Le nopm du setting.</value>
		public string SettingName { get; set; }
		
		/// <summary>
		/// Obtient/Définit la valeur du setting.
		/// </summary>
		/// <value>La valeur du setting.</value>
		public string SettingValue { get; set; }
		
		/// <summary>
		/// Initialise une nouvelle instance de NMSetUserSetting.
		/// </summary>
		/// <param name="settingName">Nom du setting.</param>
		/// <param name="settingValue">Valeur du setting.</param>
		/// <param name="callback">Callback traitant le résultat.</param>
		/// <typeparam name="TCallBackParameter">Type de paramètre de retour.</typeparam>
		public NMSetUserSetting(string settingName, string settingValue, Action<TCallBackParameter> callback) 
			: base(NMType.NMSetUserSetting, callback) {
			this.SettingName = settingName;
			this.SettingValue = settingValue;
		}
	}
}
