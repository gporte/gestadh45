using GalaSoft.MvvmLight.Messaging;
using gestadh45.business.PersonalizedMsg;
using gestadh45.model;
using System.Windows.Controls;

namespace gestadh45.wpf.UserControls.CampagnesVerificationUC
{
	/// <summary>
	/// Interaction logic for GestionSaisiesVerificationsUC.xaml
	/// </summary>
	public partial class GestionSaisiesVerificationsUC : UserControl
	{
		public GestionSaisiesVerificationsUC(CampagneVerification campagne)
		{
			InitializeComponent();

			// envoi d'un msg au VM pour charger la campagne
			Messenger.Default.Send(new NMLoadItem<CampagneVerification>(campagne));

			Messenger.Default.Register<NMClearFilter>(this, msg => this.ClearFilter());
			Messenger.Default.Register<NMSelectionElement<Verification>>(this, msg => this.ScrollToItem(msg.Content));
		}
		
		private void ClearFilter() {
			this.tbxFiltre.Clear();
		}
		
		private void ScrollToItem(Verification item) {
			this.lbxItems.ScrollIntoView(item);
		}
	}
}