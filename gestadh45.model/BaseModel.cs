using System;

namespace gestadh45.model
{
    public abstract class BaseModel
    {
		public Guid ID { get; set; }

		protected BaseModel() {
			this.ID = Guid.NewGuid();
		}
    }
}
