using gestadh45.services.Backup.DTO;
using System.IO;
using System.Xml.Serialization;

namespace gestadh45.services.Backup
{
	public static class XMLGenerator
	{
		public static void ExportAdherentDtos(string filePath, AdherentDto[] adherentDtos) {
			if (!string.IsNullOrWhiteSpace(filePath)) {
				using (var writer = new StreamWriter(filePath, false)) {
					var serializer = new XmlSerializer(typeof(AdherentDto[]));
					serializer.Serialize(writer, adherentDtos);
				}
			}
		}

		public static void ExportInscriptionDtos(string filePath, InscriptionDto[] inscriptionDtos) {
			if (!string.IsNullOrWhiteSpace(filePath)) {
				using (var writer = new StreamWriter(filePath, false)) {
					var serializer = new XmlSerializer(typeof(InscriptionDto[]));
					serializer.Serialize(writer, inscriptionDtos);
				}
			}
		}

		public static void ExportVilleDtos(string filePath, VilleDto[] villeDtos) {
			if (!string.IsNullOrWhiteSpace(filePath)) {
				using (var writer = new StreamWriter(filePath, false)) {
					var serializer = new XmlSerializer(typeof(VilleDto[]));
					serializer.Serialize(writer, villeDtos);
				}
			}
		}
	}
}
