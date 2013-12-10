using GalaSoft.MvvmLight.Messaging;
using gestadh45.business.PersonalizedMsg;
using gestadh45.model;
using System.Windows.Controls;

namespace gestadh45.wpf.UserControls.MarquesUC
{
	/// <summary>
	/// Interaction logic for GestionMarquesUC.xaml
	/// </summary>
	public partial class GestionMarquesUC : UserControl
	{
		public GestionMarquesUC()
		{
			InitializeComponent();

			Messenger.Default.Register<NMClearFilter>(this, msg => this.ClearFilter());
			Messenger.Default.Register<NMSelectionElement<Marque>>(this, msg => this.ScrollToItem(msg.Content));
		}
		
		private void ClearFilter() {
			this.tbxFiltre.Clear();
		}
		
		private void ScrollToItem(Marque item) {
			this.lbxItems.ScrollIntoView(item);
		}
	}
}