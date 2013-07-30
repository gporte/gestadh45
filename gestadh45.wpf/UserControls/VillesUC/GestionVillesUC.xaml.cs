/*
 * Crée par SharpDevelop.
 * Utilisateur: Guillaume
 * Date: 02/03/2013
 * Heure: 00:47
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
using gestadh45.business.ViewModel.VillesVM;
using gestadh45.model;

namespace gestadh45.wpf.UserControls.VillesUC
{
	/// <summary>
	/// Interaction logic for GestionVillesUC.xaml
	/// </summary>
	public partial class GestionVillesUC : UserControl
	{
		public GestionVillesUC() {
			InitializeComponent();
			this.DataContext = new GestionVillesVM(UserSettings.Default.UserConnectionString);

			Messenger.Default.Register<NMClearFilter>(this, msg => this.ClearFilter());
			Messenger.Default.Register<NMSelectionElement<Ville>>(this, msg => this.ScrollToItem(msg.Content));
		}
		
		private void ClearFilter() {
			this.tbxFiltre.Clear();
		}
		
		private void ScrollToItem(Ville item) {
			this.lbxItems.ScrollIntoView(item);
		}
	}
}