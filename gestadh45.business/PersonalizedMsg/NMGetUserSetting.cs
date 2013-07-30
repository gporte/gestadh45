/*
 * Crée par SharpDevelop.
 * Utilisateur: gp
 * Date: 14/02/2013
 * Heure: 13:53
 * 
 * Pour changer ce modèle utiliser Outils | Options | Codage | Editer les en-têtes standards.
 */
using System;
using GalaSoft.MvvmLight.Messaging;

namespace gestadh45.business.PersonalizedMsg
{
	/// <summary>
	/// NotificationMessage demandant la récupération d'un user setting.
	/// </summary>
	/// <typeparam name="TCallBackParameter">Type de paramètre de résultat.</typeparam>
	public class NMGetUserSetting<TCallBackParameter> : NotificationMessageAction<TCallBackParameter>
	{
		/// <summary>
		/// Obtient/Définit le nom du setting.
		/// </summary>
		/// <value>Le nom du setting.</value>
		public string SettingName { get; set; }
		
		/// <summary>
		/// Initialise une nouvelle instance de NMGetUserSetting.
		/// </summary>
		/// <param name="settingName">Nom du setting.</param>
		/// <param name="callback">Callback traitant le résultat.</param>
		public NMGetUserSetting(string settingName, Action<TCallBackParameter> callback) 
			: base(NMType.NMGetUserSetting, callback) {
			this.SettingName = settingName;
		}
	}
}
