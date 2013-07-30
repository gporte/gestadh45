/*
 * Crée par SharpDevelop.
 * Utilisateur: Guillaume
 * Date: 03/03/2013
 * Heure: 11:30
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
using gestadh45.business.ViewModel.ModeleVM;
using gestadh45.model;

namespace gestadh45.wpf.UserControls.ModelesUC
{
	/// <summary>
	/// Interaction logic for GestionModelesUC.xaml
	/// </summary>
	public partial class GestionModelesUC : UserControl
	{
		public GestionModelesUC()
		{
			InitializeComponent();
			this.DataContext = new GestionModelesVM(UserSettings.Default.UserConnectionString);

			Messenger.Default.Register<NMClearFilter>(this, msg => this.ClearFilter());
			Messenger.Default.Register<NMSelectionElement<Modele>>(this, msg => this.ScrollToItem(msg.Content));
		}
		
		private void ClearFilter() {
			this.tbxFiltre.Clear();
		}
		
		private void ScrollToItem(Modele item) {
			this.lbxItems.ScrollIntoView(item);
		}
	}
}