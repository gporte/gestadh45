﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.18034
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace gestadh45.business.ViewModel.GroupesVM {
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
	internal class ResGroupes {
		
		private static global::System.Resources.ResourceManager resourceMan;
		
		private static global::System.Globalization.CultureInfo resourceCulture;
		
		[global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		internal ResGroupes() {
		}
		
		/// <summary>
		///   Retourne l'instance ResourceManager mise en cache utilisée par cette classe.
		/// </summary>
		[global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
		internal static global::System.Resources.ResourceManager ResourceManager {
			get {
				if (object.ReferenceEquals(resourceMan, null)) {
					global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("gestadh45.business.ViewModel.GroupesVM.ResGroupes", typeof(ResGroupes).Assembly);
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
		///   Recherche une chaîne localisée semblable à NOUVEAU.
		/// </summary>
		internal static string DefaultLibelle {
			get {
				return ResourceManager.GetString("DefaultLibelle", resourceCulture);
			}
		}
		
		/// <summary>
		///   Recherche une chaîne localisée semblable à Aucun document n&apos;a été généré - Code document inconnu.
		/// </summary>
		internal static string ErrCodeDocumentInconnu {
			get {
				return ResourceManager.GetString("ErrCodeDocumentInconnu", resourceCulture);
			}
		}
		
		/// <summary>
		///   Recherche une chaîne localisée semblable à Un groupe portant le même libellé est déjà présent pour la saison courante.
		/// </summary>
		internal static string ErrGroupeExiste {
			get {
				return ResourceManager.GetString("ErrGroupeExiste", resourceCulture);
			}
		}
		
		/// <summary>
		///   Recherche une chaîne localisée semblable à L&apos;heure de début est obligatoire.
		/// </summary>
		internal static string ErrHeureDebutObligatoire {
			get {
				return ResourceManager.GetString("ErrHeureDebutObligatoire", resourceCulture);
			}
		}
		
		/// <summary>
		///   Recherche une chaîne localisée semblable à L&apos;heure de fin doit être strictement supérieure à l&apos;heure de début.
		/// </summary>
		internal static string ErrHeureFinInfHeureSup {
			get {
				return ResourceManager.GetString("ErrHeureFinInfHeureSup", resourceCulture);
			}
		}
		
		/// <summary>
		///   Recherche une chaîne localisée semblable à L&apos;heure de fin est obligatoire.
		/// </summary>
		internal static string ErrHeureFinObligatoire {
			get {
				return ResourceManager.GetString("ErrHeureFinObligatoire", resourceCulture);
			}
		}
		
		/// <summary>
		///   Recherche une chaîne localisée semblable à Le libellé est obligatoire.
		/// </summary>
		internal static string ErrLibelleObligatoire {
			get {
				return ResourceManager.GetString("ErrLibelleObligatoire", resourceCulture);
			}
		}
		
		/// <summary>
		///   Recherche une chaîne localisée semblable à {0} - Effectif.
		/// </summary>
		internal static string NomFichierRapportListeAdherents {
			get {
				return ResourceManager.GetString("NomFichierRapportListeAdherents", resourceCulture);
			}
		}
		
		/// <summary>
		///   Recherche une chaîne localisée semblable à &quot;({0} adhérents)&quot;.
		/// </summary>
		internal static string SousTitreRapportListeAdherents {
			get {
				return ResourceManager.GetString("SousTitreRapportListeAdherents", resourceCulture);
			}
		}
		
		/// <summary>
		///   Recherche une chaîne localisée semblable à Effectif.
		/// </summary>
		internal static string TitreRapportListeAdherents {
			get {
				return ResourceManager.GetString("TitreRapportListeAdherents", resourceCulture);
			}
		}
	}
}
