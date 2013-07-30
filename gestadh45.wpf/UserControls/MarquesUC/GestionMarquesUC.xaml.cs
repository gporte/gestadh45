/*
 * Crée par SharpDevelop.
 * Utilisateur: Guillaume
 * Date: 02/03/2013
 * Heure: 20:54
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
using gestadh45.business.ViewModel.MarquesVM;
using gestadh45.model;

namespace gestadh45.wpf.UserControls.MarquesUC
{
	/// <summary>
	/// Interaction logic for GestionMarquesUC.xaml
	/// </summary>
	public partial class GestionMarquesUC : UserControl
	{
		public GestionMarquesUC()
		{
			InitializeComponent();
			this.DataContext = new GestionMarquesVM(UserSettings.Default.UserConnectionString);

			Messenger.Default.Register<NMClearFilter>(this, msg => this.ClearFilter());
			Messenger.Default.Register<NMSelectionElement<Marque>>(this, msg => this.ScrollToItem(msg.Content));
		}
		
		private void ClearFilter() {
			this.tbxFiltre.Clear();
		}
		
		private void ScrollToItem(Marque item) {
			this.lbxItems.ScrollIntoView(item);
		}
	}
}