/*
 * Crée par SharpDevelop.
 * Utilisateur: Guillaume
 * Date: 02/03/2013
 * Heure: 18:56
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
using gestadh45.business.ViewModel.SectionsVM;

namespace gestadh45.wpf.UserControls.SectionsUC
{
	/// <summary>
	/// Interaction logic for GestionSectionsUC.xaml
	/// </summary>
	public partial class GestionSectionsUC : UserControl
	{
		public GestionSectionsUC()
		{
			InitializeComponent();
			
			this.DataContext = new GestionSectionsVM(UserSettings.Default.UserConnectionString);

			Messenger.Default.Register<NMClearFilter>(this, msg => this.ClearFilter());
			Messenger.Default.Register<NMSelectionElement<Section>>(this, msg => this.ScrollToItem(msg.Content));
		}
		
		private void ClearFilter() {
			this.tbxFiltre.Clear();
		}
		
		private void ScrollToItem(Section item) {
			this.lbxItems.ScrollIntoView(item);
		}
	}
}