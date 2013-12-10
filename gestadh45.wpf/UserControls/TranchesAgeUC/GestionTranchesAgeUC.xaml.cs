using GalaSoft.MvvmLight.Messaging;
using gestadh45.business.PersonalizedMsg;
using gestadh45.model;
using System.Windows.Controls;

namespace gestadh45.wpf.UserControls.TranchesAgeUC
{
	/// <summary>
	/// Interaction logic for GestionTranchesAgeUC.xaml
	/// </summary>
	public partial class GestionTranchesAgeUC : UserControl
	{
		public GestionTranchesAgeUC()
		{
			InitializeComponent();				

			Messenger.Default.Register<NMClearFilter>(this, msg => this.ClearFilter());
			Messenger.Default.Register<NMSelectionElement<TrancheAge>>(this, msg => this.ScrollToItem(msg.Content));
		}
		
		private void ClearFilter() {
			this.tbxFiltre.Clear();
		}
		
		private void ScrollToItem(TrancheAge item) {
			this.lbxItems.ScrollIntoView(item);
		}
	}
}