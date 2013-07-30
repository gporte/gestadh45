/*
 * Crée par SharpDevelop.
 * Utilisateur: Guillaume
 * Date: 02/03/2013
 * Heure: 11:35
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
using gestadh45.business.ViewModel.SaisonsVM;
using gestadh45.model;

namespace gestadh45.wpf.UserControls.SaisonsUC
{
	/// <summary>
	/// Interaction logic for GestionSaisonsUC.xaml
	/// </summary>
	public partial class GestionSaisonsUC : UserControl
	{
		public GestionSaisonsUC() {
			InitializeComponent();
			this.DataContext = new GestionSaisonsVM(UserSettings.Default.UserConnectionString);

			Messenger.Default.Register<NMClearFilter>(this, msg => this.ClearFilter());
			Messenger.Default.Register<NMSelectionElement<Saison>>(this, msg => this.ScrollToItem(msg.Content));
		}
		
		private void ClearFilter() {
			this.tbxFiltre.Clear();
		}
		
		private void ScrollToItem(Saison item) {
			this.lbxItems.ScrollIntoView(item);
		}
	}
}