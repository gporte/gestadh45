/*
 * Crée par SharpDevelop.
 * Utilisateur: Guillaume
 * Date: 03/03/2013
 * Heure: 12:42
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
using gestadh45.business.ViewModel.EquipementsVM;
using gestadh45.model;

namespace gestadh45.wpf.UserControls.EquipementsUC
{
	/// <summary>
	/// Interaction logic for GestionEquipementsUC.xaml
	/// </summary>
	public partial class GestionEquipementsUC : UserControl
	{
		public GestionEquipementsUC()
		{
			InitializeComponent();
			this.DataContext = new GestionEquipementsVM(UserSettings.Default.UserConnectionString);

			Messenger.Default.Register<NMClearFilter>(this, msg => this.ClearFilter());
			Messenger.Default.Register<NMSelectionElement<Equipement>>(this, msg => this.ScrollToItem(msg.Content));
		}		
				
		private void ClearFilter() {
			this.tbxFiltre.Clear();
		}
		
		private void ScrollToItem(Equipement item) {
			this.lbxItems.ScrollIntoView(item);
		}
	}
}