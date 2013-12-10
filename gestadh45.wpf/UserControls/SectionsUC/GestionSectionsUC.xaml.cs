using GalaSoft.MvvmLight.Messaging;
using gestadh45.business.PersonalizedMsg;
using System.Windows.Controls;
using System.Windows.Documents;

namespace gestadh45.wpf.UserControls.SectionsUC
{
	/// <summary>
	/// Interaction logic for GestionSectionsUC.xaml
	/// </summary>
	public partial class GestionSectionsUC : UserControl
	{
		public GestionSectionsUC()
		{
			InitializeComponent();

			Messenger.Default.Register<NMClearFilter>(this, msg => this.ClearFilter());
			Messenger.Default.Register<NMSelectionElement<Section>>(this, msg => this.ScrollToItem(msg.Content));
		}
		
		private void ClearFilter() {
			this.tbxFiltre.Clear();
		}
		
		private void ScrollToItem(Section item) {
			this.lbxItems.ScrollIntoView(item);
		}
	}
}