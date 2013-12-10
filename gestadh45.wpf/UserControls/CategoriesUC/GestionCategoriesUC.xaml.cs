using GalaSoft.MvvmLight.Messaging;
using gestadh45.business.PersonalizedMsg;
using gestadh45.model;
using System.Windows.Controls;

namespace gestadh45.wpf.UserControls.CategoriesUC
{
	/// <summary>
	/// Interaction logic for GestionCategoriesUC.xaml
	/// </summary>
	public partial class GestionCategoriesUC : UserControl
	{
		public GestionCategoriesUC()
		{
			InitializeComponent();

			Messenger.Default.Register<NMClearFilter>(this, msg => this.ClearFilter());
			Messenger.Default.Register<NMSelectionElement<Categorie>>(this, msg => this.ScrollToItem(msg.Content));
		}
		
		private void ClearFilter() {
			this.tbxFiltre.Clear();
		}
		
		private void ScrollToItem(Categorie item) {
			this.lbxItems.ScrollIntoView(item);
		}
	}
}