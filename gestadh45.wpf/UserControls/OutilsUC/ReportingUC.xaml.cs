using gestadh45.business.ViewModel.OutilsVM;
using gestadh45.services.Reporting.Templates;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Windows.Controls;

namespace gestadh45.wpf.UserControls.OutilsUC
{
	/// <summary>
	/// Logique d'interaction pour ReportingUC.xaml
	/// </summary>
	public partial class ReportingUC : UserControl
	{
		public ReportingUC() {
			InitializeComponent();
			this.DataContext = new ReportingVM(UserSettings.Default.UserConnectionString);
		}

		private void dgReport_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e) {
			var pd = e.PropertyDescriptor as PropertyDescriptor;
			var displayAttrib = pd.Attributes[typeof(DisplayAttribute)] as DisplayAttribute;

			if (displayAttrib != null) {
				e.Column.Header = ResReports.ResourceManager.GetString(displayAttrib.Name);
			}
		}
	}
}
