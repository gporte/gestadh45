using System.Data.SqlServerCe;
using System.IO;

namespace gestadh45.services.Database
{
	public class DatabaseService
	{
		/// <summary>
		/// Vérifie l'existence d'un fichier de BDD Sql Server CE
		/// </summary>
		/// <param name="sqlCeConnectionString">Chaîne de connexion</param>
		/// <returns>True si le fichier BDD existe, False sinon</returns>
		public static bool SqlCeDBExists(string sqlCeConnectionString) {
			var cs = new SqlCeConnectionStringBuilder(sqlCeConnectionString);
			return File.Exists(cs.DataSource);
		}

		/// <summary>
		/// Créé une BDD SqlServer CE à partir de sa chaîne de connexion
		/// </summary>
		/// <param name="sqlCeConnectionString">Chaîne de connexion</param>
		public static void CreateSqlCeDatabase(string sqlCeConnectionString) {
			var engine = new SqlCeEngine(sqlCeConnectionString);
			engine.CreateDatabase();
		}
	}
}
