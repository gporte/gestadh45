using System.Data.Common;
using System.Data.Entity;
using System.Data.SqlServerCe;

using gestadh45.model;

namespace gestadh45.dal
{
	public class GestAdhContext : DbContext
	{
		/// <summary>
		/// Constructeur créant un contexte à partir de la chaîne de connexion SQL CE 4.0 passée en paramètre
		/// </summary>
		/// <param name="connectionString">Chaîne de connexion SQL CE 4.0</param>
		public GestAdhContext(string connectionString)
			: base(new SqlCeConnection(connectionString), false) { }
		
		public DbSet<Adherent> Adherents { get; set; }
		public DbSet<Groupe> Groupes { get; set; }
		public DbSet<InfosClub> InfosClub { get; set; }
		public DbSet<Inscription> Inscriptions { get; set; }
		public DbSet<Saison> Saisons { get; set; }
		public DbSet<Section> Sections { get; set; }
		public DbSet<TrancheAge> TrancheAges { get; set; }
		public DbSet<Ville> Villes { get; set; }

		public DbSet<CampagneVerification> CampagneVerifications { get; set; }
		public DbSet<Categorie> Categories { get; set; }
		public DbSet<DureeDeVie> DureeDeVies { get; set; }
		public DbSet<Equipement> Equipements { get; set; }
		public DbSet<Localisation> Localisations { get; set; }
		public DbSet<Marque> Marques { get; set; }
		public DbSet<Modele> Modeles { get; set; }
		public DbSet<Verification> Verifications { get; set; }
	}
}
