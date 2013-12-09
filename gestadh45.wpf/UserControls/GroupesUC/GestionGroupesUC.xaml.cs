using GalaSoft.MvvmLight.Messaging;
using gestadh45.business.PersonalizedMsg;
using gestadh45.model;
using System.Windows.Controls;

namespace gestadh45.wpf.UserControls.GroupesUC
{
	/// <summary>
	/// Interaction logic for GestionGroupesUC.xaml
	/// </summary>
	public partial class GestionGroupesUC : UserControl
	{
		public GestionGroupesUC()
		{
			InitializeComponent();

			Messenger.Default.Register<NMClearFilter>(this, msg => this.ClearFilter());
			Messenger.Default.Register<NMSelectionElement<Groupe>>(this, msg => this.ScrollToItem(msg.Content));
		}
		
		private void ClearFilter() {
			this.tbxFiltre.Clear();
		}
		
		private void ScrollToItem(Groupe item) {
			this.lbxItems.ScrollIntoView(item);
		}
	}
}