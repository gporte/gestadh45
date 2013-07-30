using gestadh45.business.ViewModel.OutilsVM;
using System.Windows.Controls;

namespace gestadh45.wpf.UserControls.OutilsUC
{
	/// <summary>
	/// Logique d'interaction pour GraphiquesUC.xaml
	/// </summary>
	public partial class GraphiquesUC : UserControl
	{
		public GraphiquesUC() {
			InitializeComponent();
			this.DataContext = new GraphiquesVM(UserSettings.Default.UserConnectionString);
		}
	}
}
