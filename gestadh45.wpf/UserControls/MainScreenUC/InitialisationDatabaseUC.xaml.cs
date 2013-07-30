/*
 * Crée par SharpDevelop.
 * Utilisateur: gp
 * Date: 11/03/2013
 * Heure: 11:04
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
using gestadh45.business.ViewModel.MainScreenVM;

namespace gestadh45.wpf.UserControls.MainScreenUC
{
	/// <summary>
	/// Interaction logic for InitialisationDatabaseUC.xaml
	/// </summary>
	public partial class InitialisationDatabaseUC : UserControl
	{
		public InitialisationDatabaseUC()
		{
			InitializeComponent();
			this.DataContext = new InitialisationDatabaseVM(UserSettings.Default.UserConnectionString);
		}
	}
}