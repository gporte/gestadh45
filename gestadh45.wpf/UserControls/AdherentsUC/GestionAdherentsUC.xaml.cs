/*
 * Crée par SharpDevelop.
 * Utilisateur: gp
 * Date: 22/02/2013
 * Heure: 09:56
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
using gestadh45.business.ViewModel.AdherentsVM;
using gestadh45.model;

namespace gestadh45.wpf.UserControls.AdherentsUC
{
	/// <summary>
	/// Interaction logic for GestionAdherentsUC.xaml
	/// </summary>
	public partial class GestionAdherentsUC : UserControl
	{
		public GestionAdherentsUC()	{
			InitializeComponent();
			this.DataContext = new GestionAdherentsVM(UserSettings.Default.UserConnectionString);

			Messenger.Default.Register<NMClearFilter>(this, msg => this.ClearFilter());
			Messenger.Default.Register<NMSelectionElement<Adherent>>(this, msg => this.ScrollToItem(msg.Content));
		}
		
		public GestionAdherentsUC(Adherent adherent) {
			InitializeComponent();
			this.DataContext = new GestionAdherentsVM(UserSettings.Default.UserConnectionString, adherent.ID);

			Messenger.Default.Register<NMClearFilter>(this, msg => this.ClearFilter());
			Messenger.Default.Register<NMSelectionElement<Adherent>>(this, msg => this.ScrollToItem(msg.Content));
		}
		
		private void ClearFilter() {
			this.tbxFiltre.Clear();
		}
		
		private void ScrollToItem(Adherent item) {
			this.lbxItems.ScrollIntoView(item);
		}
	}
}