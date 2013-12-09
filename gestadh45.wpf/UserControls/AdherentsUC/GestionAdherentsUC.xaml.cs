using GalaSoft.MvvmLight.Messaging;
using gestadh45.business.PersonalizedMsg;
using gestadh45.model;
using System.Windows.Controls;

namespace gestadh45.wpf.UserControls.AdherentsUC
{
	/// <summary>
	/// Interaction logic for GestionAdherentsUC.xaml
	/// </summary>
	public partial class GestionAdherentsUC : UserControl
	{
		public GestionAdherentsUC()	{
			InitializeComponent();

			Messenger.Default.Register<NMClearFilter>(this, msg => this.ClearFilter());
			Messenger.Default.Register<NMSelectionElement<Adherent>>(this, msg => this.ScrollToItem(msg.Content));
		}
		
		public GestionAdherentsUC(Adherent adherent) {
			InitializeComponent();

			// envoi d'un msg au VM pour charger l'adhérent
			Messenger.Default.Send(new NMLoadItem<Adherent>(adherent));

			Messenger.Default.Register<NMClearFilter>(this, msg => this.ClearFilter());
			Messenger.Default.Register<NMSelectionElement<Adherent>>(this, msg => this.ScrollToItem(msg.Content));
		}
		
		private void ClearFilter() {
			this.tbxFiltre.Clear();
		}
		
		private void ScrollToItem(Adherent item) {
			this.lbxItems.ScrollIntoView(item);
		}
	}
}