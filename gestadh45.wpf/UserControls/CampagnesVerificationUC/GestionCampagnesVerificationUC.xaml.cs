/*
 * Crée par SharpDevelop.
 * Utilisateur: Guillaume
 * Date: 09/03/2013
 * Heure: 11:25
 * 
 * Pour changer ce modèle utiliser Outils | Options | Codage | Editer les en-têtes standards.
 */
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

using GalaSoft.MvvmLight.Messaging;
using gestadh45.business.PersonalizedMsg;
using gestadh45.business.ViewModel.CampagnesVerificationVM;
using gestadh45.model;

namespace gestadh45.wpf.UserControls.CampagnesVerificationUC
{
	/// <summary>
	/// Interaction logic for GestionCampagnesVerificationUC.xaml
	/// </summary>
	public partial class GestionCampagnesVerificationUC : UserControl
	{
		public GestionCampagnesVerificationUC()
		{
			InitializeComponent();
			
			this.DataContext = new GestionCampagnesVerificationVM(UserSettings.Default.UserConnectionString);

			Messenger.Default.Register<NMClearFilter>(this, msg => this.ClearFilter());
			Messenger.Default.Register<NMSelectionElement<CampagneVerification>>(this, msg => this.ScrollToItem(msg.Content));
		}
		
		private void ClearFilter() {
			this.tbxFiltre.Clear();
		}
		
		private void ScrollToItem(CampagneVerification item) {
			this.lbxItems.ScrollIntoView(item);
		}
	}
}