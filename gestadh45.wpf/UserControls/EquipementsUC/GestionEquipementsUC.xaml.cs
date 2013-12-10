using GalaSoft.MvvmLight.Messaging;
using gestadh45.business.PersonalizedMsg;
using gestadh45.business.ViewModel.EquipementsVM;
using gestadh45.model;
using System.Windows.Controls;

namespace gestadh45.wpf.UserControls.EquipementsUC
{
	/// <summary>
	/// Interaction logic for GestionEquipementsUC.xaml
	/// </summary>
	public partial class GestionEquipementsUC : UserControl
	{
		public GestionEquipementsUC()
		{
			InitializeComponent();
			this.DataContext = new GestionEquipementsVM();

			Messenger.Default.Register<NMClearFilter>(this, msg => this.ClearFilter());
			Messenger.Default.Register<NMSelectionElement<Equipement>>(this, msg => this.ScrollToItem(msg.Content));
		}		
				
		private void ClearFilter() {
			this.tbxFiltre.Clear();
		}
		
		private void ScrollToItem(Equipement item) {
			this.lbxItems.ScrollIntoView(item);
		}
	}
}