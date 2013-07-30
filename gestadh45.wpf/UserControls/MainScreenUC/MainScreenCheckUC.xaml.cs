using gestadh45.business.ViewModel.MainScreenVM;
using System.Windows;
using System.Windows.Controls;

namespace gestadh45.wpf.UserControls.MainScreenUC
{
	/// <summary>
	/// Logique d'interaction pour MainScreenUC.xaml
	/// </summary>
	public partial class MainScreenCheckUC : UserControl
	{
		public MainScreenCheckUC() {
			InitializeComponent();
			this.DataContext = new MainScreenCheckVM();
		}
		
		private void UserControl_Loaded(object sender, RoutedEventArgs e) {
			(this.DataContext as MainScreenCheckVM).TryAutoOpenDb();
		}
	}
}
