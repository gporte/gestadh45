using GalaSoft.MvvmLight.Messaging;
using gestadh45.business.PersonalizedMsg;
using gestadh45.model;
using System.Windows.Controls;

namespace gestadh45.wpf.UserControls.InscriptionsUC
{
	/// <summary>
	/// Interaction logic for GestionInscriptionsUC.xaml
	/// </summary>
	public partial class GestionInscriptionsUC : UserControl
	{
		public GestionInscriptionsUC()
		{
			InitializeComponent();

			Messenger.Default.Register<NMClearFilter>(this, msg => this.ClearFilter());
			Messenger.Default.Register<NMSelectionElement<Inscription>>(this, msg => this.ScrollToItem(msg.Content));
		}
		
		public GestionInscriptionsUC(Adherent adherent) {
			InitializeComponent();

			Messenger.Default.Send(new NMLoadItem<Adherent>(adherent));

			Messenger.Default.Register<NMClearFilter>(this, msg => this.ClearFilter());
			Messenger.Default.Register<NMSelectionElement<Inscription>>(this, msg => this.ScrollToItem(msg.Content));
		}
		
		private void ClearFilter() {
			this.tbxFiltre.Clear();
		}
		
		private void ScrollToItem(Inscription item) {
			this.lbxItems.ScrollIntoView(item);
		}
	}
}