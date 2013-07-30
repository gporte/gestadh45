/*
 * Crée par SharpDevelop.
 * Utilisateur: gp
 * Date: 26/02/2013
 * Heure: 13:41
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
using gestadh45.business.ViewModel.InscriptionsVM;
using gestadh45.model;

namespace gestadh45.wpf.UserControls.InscriptionsUC
{
	/// <summary>
	/// Interaction logic for GestionInscriptionsUC.xaml
	/// </summary>
	public partial class GestionInscriptionsUC : UserControl
	{
		public GestionInscriptionsUC()
		{
			InitializeComponent();
			this.DataContext = new GestionInscriptionsVM(UserSettings.Default.UserConnectionString);

			Messenger.Default.Register<NMClearFilter>(this, msg => this.ClearFilter());
			Messenger.Default.Register<NMSelectionElement<Inscription>>(this, msg => this.ScrollToItem(msg.Content));
		}
		
		public GestionInscriptionsUC(Adherent adherent) {
			InitializeComponent();
			this.DataContext = new GestionInscriptionsVM(UserSettings.Default.UserConnectionString, adherent.ID);

			Messenger.Default.Register<NMClearFilter>(this, msg => this.ClearFilter());
			Messenger.Default.Register<NMSelectionElement<Inscription>>(this, msg => this.ScrollToItem(msg.Content));
		}
		
		private void ClearFilter() {
			this.tbxFiltre.Clear();
		}
		
		private void ScrollToItem(Inscription item) {
			this.lbxItems.ScrollIntoView(item);
		}
	}
}