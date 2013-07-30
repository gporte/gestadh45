using gestadh45.business.Enums;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace gestadh45.wpf.UserControls
{
	/// <summary>
	/// Logique d'interaction pour BtnAnnulerEnregistrerUC.xaml
	/// </summary>
	public partial class BtnAnnulerEnregistrerUC : UserControl
	{
		#region proprietes
		/// <summary>
		/// Obtient/Définit le code de l'UC parent
		/// </summary>
		public CodesUC CodeUCParent {
			get {
				return (CodesUC)GetValue(CodeUCParentProperty);
			}

			set {
				SetValue(CodeUCParentProperty, value);
			}
		}

		/// <summary>
		/// Obtient/Définit la commande d'annulation
		/// </summary>
		public ICommand CmdAnnuler {
			get {
				return (ICommand)GetValue(CmdAnnulerProperty);
			}

			set {
				SetValue(CmdAnnulerProperty, value);
			}
		}

		/// <summary>
		/// Obtient/Définit la commande d'enregistrement
		/// </summary>
		public ICommand CmdEnregistrer {
			get {
				return (ICommand)GetValue(CmdEnregistrerProperty);
			}

			set {
				SetValue(CmdEnregistrerProperty, value);
			}
		}

		/// <summary>
		/// Obtient/Définit la visibilité du bouton Annuler
		/// </summary>
		public Visibility BtnAnnulerVisibilite {
			get {
				return (Visibility)GetValue(CmdAnnulerProperty);
			}

			set {
				SetValue(CmdAnnulerProperty, value);
			}
		}

		/// <summary>
		/// Obtient/Définit la visibilité du bouton Enregistrer
		/// </summary>
		public Visibility BtnEnregistrerVisibilite {
			get {
				return (Visibility)GetValue(CmdEnregistrerProperty);
			}

			set {
				SetValue(CmdEnregistrerProperty, value);
			}
		}
		#endregion

		public BtnAnnulerEnregistrerUC() {
			InitializeComponent();
		}

		#region Dependency Properties
		public static DependencyProperty CodeUCParentProperty = DependencyProperty.Register(
			"CodeUCParent",
			typeof(CodesUC),
			typeof(BtnAnnulerEnregistrerUC)
		);

		public static DependencyProperty CmdAnnulerProperty = DependencyProperty.Register(
			"CmdAnnuler",
			typeof(ICommand),
			typeof(BtnAnnulerEnregistrerUC)
		);

		public static DependencyProperty CmdEnregistrerProperty = DependencyProperty.Register(
			"CmdEnregistrer",
			typeof(ICommand),
			typeof(BtnAnnulerEnregistrerUC)
		);

		public static DependencyProperty BtnAnnulerVisibiliteProperty = DependencyProperty.Register(
			"BtnAnnulerVisibilite",
			typeof(Visibility),
			typeof(BtnAnnulerEnregistrerUC)
		);

		public static DependencyProperty BtnEnregistrerVisibiliteProperty = DependencyProperty.Register(
			"BtnEnregistrerVisibilite",
			typeof(Visibility),
			typeof(BtnAnnulerEnregistrerUC)
		);
		#endregion
	}
}
