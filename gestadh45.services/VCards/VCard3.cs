
namespace gestadh45.services.VCards
{
	public class VCard3 : VCardBase
	{
		#region specific fields templates for version 3.0
		protected override string Version {
			get { return "VERSION:3.0"; }
		}
		
		protected override string TelWorkTemplate {
			get { return "TEL;TYPE=WORK,VOICE:{0}"; }
		}

		protected override string EmailInternetTemplate {
			get { return "EMAIL;INTERNET:{0}"; }
		}

		protected override string OrganizationTemplate {
			get { return "ORG:{0}"; }
		}
		#endregion
		
		/// <summary>
		/// Initialize a new instance of VcardGenerator21
		/// </summary>
		public VCard3(string firstName, string lastName) 
			: base(firstName, lastName) { }
	}
}
