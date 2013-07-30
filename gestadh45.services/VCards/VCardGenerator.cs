using System.Collections.Generic;
using System.IO;

namespace gestadh45.services.VCards
{
	public class VCardGenerator
	{
		private ICollection<VCardBase> VCards { get; set; }

		public VCardGenerator() {
			this.VCards = new List<VCardBase>();
		}

		public void AddVCard(VCardBase vcard) {
			this.VCards.Add(vcard);
		}

		public void Write(string filePath) {
			if (!string.IsNullOrWhiteSpace(filePath)) {
				using (var sw = new StreamWriter(filePath)) {
					foreach (var vcard in this.VCards) {
						sw.Write(vcard.GenerateVCard());
					}
				}
			}
		}
	}
}
