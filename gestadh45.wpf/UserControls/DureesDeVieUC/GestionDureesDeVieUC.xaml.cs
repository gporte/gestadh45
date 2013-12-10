using GalaSoft.MvvmLight.Messaging;
using gestadh45.business.PersonalizedMsg;
using gestadh45.model;
using System.Windows.Controls;

namespace gestadh45.wpf.UserControls.DureesDeVieUC
{
	/// <summary>
	/// Interaction logic for GestionDureesDeVieUC.xaml
	/// </summary>
	public partial class GestionDureesDeVieUC : UserControl
	{
		public GestionDureesDeVieUC()
		{
			InitializeComponent();

			Messenger.Default.Register<NMClearFilter>(this, msg => this.ClearFilter());
			Messenger.Default.Register<NMSelectionElement<DureeDeVie>>(this, msg => this.ScrollToItem(msg.Content));
		}
		
		private void ClearFilter() {
			this.tbxFiltre.Clear();
		}
		
		private void ScrollToItem(DureeDeVie item) {
			this.lbxItems.ScrollIntoView(item);
		}
	}
}