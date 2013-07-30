
namespace gestadh45.services.Documents
{
	public abstract class GenerateurDocumentBase
	{
		#region Donnees
		protected DonneesDocument Donnees { get; private set; }
		#endregion
		
		#region SavePath
		protected string SavePath { get; private set; }
		#endregion

		protected GenerateurDocumentBase(DonneesDocument donnees, string savePath) {
			this.Donnees = donnees;
			this.SavePath = savePath;
		}

		public abstract void CreerDocumentAttestation();
		public abstract void CreerDocumentInscription();
	}
}
