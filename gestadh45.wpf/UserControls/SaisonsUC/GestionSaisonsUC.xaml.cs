using GalaSoft.MvvmLight.Messaging;
using gestadh45.business.PersonalizedMsg;
using gestadh45.model;
using System.Windows.Controls;

namespace gestadh45.wpf.UserControls.SaisonsUC
{
	/// <summary>
	/// Interaction logic for GestionSaisonsUC.xaml
	/// </summary>
	public partial class GestionSaisonsUC : UserControl
	{
		public GestionSaisonsUC() {
			InitializeComponent();

			Messenger.Default.Register<NMClearFilter>(this, msg => this.ClearFilter());
			Messenger.Default.Register<NMSelectionElement<Saison>>(this, msg => this.ScrollToItem(msg.Content));
		}
		
		private void ClearFilter() {
			this.tbxFiltre.Clear();
		}
		
		private void ScrollToItem(Saison item) {
			this.lbxItems.ScrollIntoView(item);
		}
	}
}