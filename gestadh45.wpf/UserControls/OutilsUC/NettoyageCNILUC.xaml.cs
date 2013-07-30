using gestadh45.business.ViewModel.OutilsVM;
using System.Windows.Controls;

namespace gestadh45.wpf.UserControls.OutilsUC
{
	/// <summary>
	/// Logique d'interaction pour NettoyageCNILUC.xaml
	/// </summary>
	public partial class NettoyageCNILUC : UserControl
	{
		public NettoyageCNILUC() {
			InitializeComponent();
			this.DataContext = new NettoyageCNILVM(UserSettings.Default.UserConnectionString);
		}
	}
}
