﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.18034
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace gestadh45.business.ViewModel.CampagnesVerificationVM {
	using System;
	
	
	/// <summary>
	///   Une classe de ressource fortement typée destinée, entre autres, à la consultation des chaînes localisées.
	/// </summary>
	// Cette classe a été générée automatiquement par la classe StronglyTypedResourceBuilder
	// à l'aide d'un outil, tel que ResGen ou Visual Studio.
	// Pour ajouter ou supprimer un membre, modifiez votre fichier .ResX, puis réexécutez ResGen
	// avec l'option /str ou régénérez votre projet VS.
	[global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
	[global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
	[global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
	internal class ResCampagnesVerification {
		
		private static global::System.Resources.ResourceManager resourceMan;
		
		private static global::System.Globalization.CultureInfo resourceCulture;
		
		[global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		internal ResCampagnesVerification() {
		}
		
		/// <summary>
		///   Retourne l'instance ResourceManager mise en cache utilisée par cette classe.
		/// </summary>
		[global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
		internal static global::System.Resources.ResourceManager ResourceManager {
			get {
				if (object.ReferenceEquals(resourceMan, null)) {
					global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("gestadh45.business.ViewModel.CampagnesVerificationVM.ResCampagnesVerification", typeof(ResCampagnesVerification).Assembly);
					resourceMan = temp;
				}
				return resourceMan;
			}
		}
		
		/// <summary>
		///   Remplace la propriété CurrentUICulture du thread actuel pour toutes
		///   les recherches de ressources à l'aide de cette classe de ressource fortement typée.
		/// </summary>
		[global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
		internal static global::System.Globalization.CultureInfo Culture {
			get {
				return resourceCulture;
			}
			set {
				resourceCulture = value;
			}
		}
		
		/// <summary>
		///   Recherche une chaîne localisée semblable à Responsable.
		/// </summary>
		internal static string Defaut_Responsable {
			get {
				return ResourceManager.GetString("Defaut_Responsable", resourceCulture);
			}
		}
		
		/// <summary>
		///   Recherche une chaîne localisée semblable à Commentaire obligatoire pour l&apos;équipement &quot;{0}&quot;.
		/// </summary>
		internal static string ErrCommentaireObligatoire {
			get {
				return ResourceManager.GetString("ErrCommentaireObligatoire", resourceCulture);
			}
		}
		
		/// <summary>
		///   Recherche une chaîne localisée semblable à Le responsable est obligatoire.
		/// </summary>
		internal static string ErrResponsableObligatoire {
			get {
				return ResourceManager.GetString("ErrResponsableObligatoire", resourceCulture);
			}
		}
		
		/// <summary>
		///   Recherche une chaîne localisée semblable à Les {0} vérifications ont été passées au statut {1}.
		/// </summary>
		internal static string InfoChangementStatuts {
			get {
				return ResourceManager.GetString("InfoChangementStatuts", resourceCulture);
			}
		}
		
		/// <summary>
		///   Recherche une chaîne localisée semblable à {0} - Campagne de vérification.
		/// </summary>
		internal static string NomFichierRapportCampagneVerification {
			get {
				return ResourceManager.GetString("NomFichierRapportCampagneVerification", resourceCulture);
			}
		}
		
		/// <summary>
		///   Recherche une chaîne localisée semblable à {0} ({1} équipements).
		/// </summary>
		internal static string SousTitreRapportVerificationEquipement {
			get {
				return ResourceManager.GetString("SousTitreRapportVerificationEquipement", resourceCulture);
			}
		}
		
		/// <summary>
		///   Recherche une chaîne localisée semblable à Etes-vous sûr de vouloir supprimer cette campagne?.
		/// </summary>
		internal static string TexteConfirmationSuppression {
			get {
				return ResourceManager.GetString("TexteConfirmationSuppression", resourceCulture);
			}
		}
		
		/// <summary>
		///   Recherche une chaîne localisée semblable à Etes-vous sûr de vouloir valider cette campagne? (elle ne sera plus modifiable et les équipements marqués au rebut n&apos;apparaîtront plus dans l&apos;inventaire).
		/// </summary>
		internal static string TexteConfirmationValidation {
			get {
				return ResourceManager.GetString("TexteConfirmationValidation", resourceCulture);
			}
		}
		
		/// <summary>
		///   Recherche une chaîne localisée semblable à Campagne de vérification du {0}.
		/// </summary>
		internal static string TitreRapportVerificationEquipement {
			get {
				return ResourceManager.GetString("TitreRapportVerificationEquipement", resourceCulture);
			}
		}
	}
}