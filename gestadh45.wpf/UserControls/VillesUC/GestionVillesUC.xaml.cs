using GalaSoft.MvvmLight.Messaging;
using gestadh45.business.PersonalizedMsg;
using gestadh45.model;
using System.Windows.Controls;

namespace gestadh45.wpf.UserControls.VillesUC
{
	/// <summary>
	/// Interaction logic for GestionVillesUC.xaml
	/// </summary>
	public partial class GestionVillesUC : UserControl
	{
		public GestionVillesUC() {
			InitializeComponent();

			Messenger.Default.Register<NMClearFilter>(this, msg => this.ClearFilter());
			Messenger.Default.Register<NMSelectionElement<Ville>>(this, msg => this.ScrollToItem(msg.Content));
		}
		
		private void ClearFilter() {
			this.tbxFiltre.Clear();
		}
		
		private void ScrollToItem(Ville item) {
			this.lbxItems.ScrollIntoView(item);
		}
	}
}