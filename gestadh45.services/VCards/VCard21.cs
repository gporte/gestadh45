
namespace gestadh45.services.VCards
{
	public class VCard21 : VCardBase
	{
		#region specific fields templates for version 2.1
		protected override string Version {
			get { return "VERSION:2.1"; }
		}
		
		protected override string TelWorkTemplate {
			get { return "TEL;WORK:{0}"; }
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
		public VCard21(string firstName, string lastName) 
			: base(firstName, lastName) { }
	}
}
