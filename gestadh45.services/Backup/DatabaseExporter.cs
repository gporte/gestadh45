using System.IO;

namespace gestadh45.services.Backup
{
	public static class DatabaseExporter
	{
		public static void Export(string databasePath, string savePath) {
			File.Copy(databasePath, savePath, true);
		}
	}
}
