/*
 * Crée par SharpDevelop.
 * Utilisateur: gp
 * Date: 01/03/2013
 * Heure: 13:46
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
using gestadh45.business.ViewModel.GroupesVM;
using gestadh45.model;

namespace gestadh45.wpf.UserControls.GroupesUC
{
	/// <summary>
	/// Interaction logic for GestionGroupesUC.xaml
	/// </summary>
	public partial class GestionGroupesUC : UserControl
	{
		public GestionGroupesUC()
		{
			InitializeComponent();
			this.DataContext = new GestionGroupesVM(UserSettings.Default.UserConnectionString);

			Messenger.Default.Register<NMClearFilter>(this, msg => this.ClearFilter());
			Messenger.Default.Register<NMSelectionElement<Groupe>>(this, msg => this.ScrollToItem(msg.Content));
		}
		
		private void ClearFilter() {
			this.tbxFiltre.Clear();
		}
		
		private void ScrollToItem(Groupe item) {
			this.lbxItems.ScrollIntoView(item);
		}
	}
}