using GalaSoft.MvvmLight.Messaging;
using gestadh45.model;

namespace gestadh45.business.PersonalizedMsg
{
	public class NMLoadItem<TItem> : NotificationMessage<TItem> where TItem : BaseModel
	{
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="item">Item</param>
		public NMLoadItem(TItem item)
			: base(item, NMType.NMLoadItem) { }
	}
}
