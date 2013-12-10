using GalaSoft.MvvmLight.Messaging;
using gestadh45.business.PersonalizedMsg;
using gestadh45.model;
using System.Windows.Controls;

namespace gestadh45.wpf.UserControls.ModelesUC
{
	/// <summary>
	/// Interaction logic for GestionModelesUC.xaml
	/// </summary>
	public partial class GestionModelesUC : UserControl
	{
		public GestionModelesUC()
		{
			InitializeComponent();

			Messenger.Default.Register<NMClearFilter>(this, msg => this.ClearFilter());
			Messenger.Default.Register<NMSelectionElement<Modele>>(this, msg => this.ScrollToItem(msg.Content));
		}
		
		private void ClearFilter() {
			this.tbxFiltre.Clear();
		}
		
		private void ScrollToItem(Modele item) {
			this.lbxItems.ScrollIntoView(item);
		}
	}
}