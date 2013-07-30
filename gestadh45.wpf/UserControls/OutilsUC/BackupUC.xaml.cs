using gestadh45.business.ViewModel.OutilsVM;
using System.Windows.Controls;

namespace gestadh45.wpf.UserControls.OutilsUC
{
	/// <summary>
	/// Logique d'interaction pour BackupUC.xaml
	/// </summary>
	public partial class BackupUC : UserControl
	{
		public BackupUC() {
			InitializeComponent();
			this.DataContext = new BackupVM(UserSettings.Default.UserConnectionString);
		}
	}
}
