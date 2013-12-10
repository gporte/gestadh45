using GalaSoft.MvvmLight.Messaging;
using gestadh45.business.PersonalizedMsg;
using gestadh45.model;
using System.Windows.Controls;

namespace gestadh45.wpf.UserControls.LocalisationUC
{
	/// <summary>
	/// Interaction logic for GestionLocalisationsUC.xaml
	/// </summary>
	public partial class GestionLocalisationsUC : UserControl
	{
		public GestionLocalisationsUC()
		{
			InitializeComponent();

			Messenger.Default.Register<NMClearFilter>(this, msg => this.ClearFilter());
			Messenger.Default.Register<NMSelectionElement<Localisation>>(this, msg => this.ScrollToItem(msg.Content));
		}
		
		private void ClearFilter() {
			this.tbxFiltre.Clear();
		}
		
		private void ScrollToItem(Localisation item) {
			this.lbxItems.ScrollIntoView(item);
		}
	}
}