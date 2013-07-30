using GalaSoft.MvvmLight.Messaging;
using gestadh45.model;

namespace gestadh45.business.PersonalizedMsg
{
	/// <summary>
	/// Notification message demandant l'affichage des infos sur la saison courante.
	/// </summary>
	public class NMShowInfosSaisonCourante : NotificationMessage<Saison>
	{
		/// <summary>
		/// Initialise une nouvelle instance de NMShowInfosSaisonCourante.
		/// </summary>
		/// <param name="saison">Infos à afficher.</param>
		public NMShowInfosSaisonCourante(Saison saison) : base(saison, NMType.NMShowInfosSaisonCourante) { 
		}
	}
}
