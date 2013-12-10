using GalaSoft.MvvmLight.Messaging;
using gestadh45.business.PersonalizedMsg;
using gestadh45.model;
using System.Windows.Controls;

namespace gestadh45.wpf.UserControls.CampagnesVerificationUC
{
	/// <summary>
	/// Interaction logic for GestionCampagnesVerificationUC.xaml
	/// </summary>
	public partial class GestionCampagnesVerificationUC : UserControl
	{
		public GestionCampagnesVerificationUC()
		{
			InitializeComponent();

			Messenger.Default.Register<NMClearFilter>(this, msg => this.ClearFilter());
			Messenger.Default.Register<NMSelectionElement<CampagneVerification>>(this, msg => this.ScrollToItem(msg.Content));
		}
		
		private void ClearFilter() {
			this.tbxFiltre.Clear();
		}
		
		private void ScrollToItem(CampagneVerification item) {
			this.lbxItems.ScrollIntoView(item);
		}
	}
}