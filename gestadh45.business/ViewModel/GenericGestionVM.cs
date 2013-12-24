using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using gestadh45.business.PersonalizedMsg;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Windows.Input;

namespace gestadh45.business.ViewModel
{
	/// <summary>
	/// Description of GenericGestionVM.
	/// </summary>
	public abstract class GenericGestionVM<T> : VMUCBase where T : class, new()
	{
		#region Items
		private IOrderedEnumerable<T> _items;
		public IOrderedEnumerable<T> Items {
			get { return this._items; }
			set {
				if (this._items != value) {
					this._items = value;
					this.RaisePropertyChanged(() => this.Items);
				}
			}
		}
		#endregion
		
		#region SelectedItem
		private T _selectedItem;
		public T SelectedItem {
			get { return this._selectedItem; }
			set {
				if (this._selectedItem == null || !this._selectedItem.Equals(value)) {
					this._selectedItem = value;
					this.RaisePropertyChanged(() => this.SelectedItem);
				}
			}
		}
		#endregion
		
		#region CurrentFilter
		private string _currentFilter;
		
		/// <summary>
		/// Obtient/Définit la valeur actuelle du filtre
		/// </summary>
		public string CurrentFilter {
			get { return this._currentFilter; }
			set {
				if(this._currentFilter != value) {
					this._currentFilter = value;
					this.RaisePropertyChanged(()=>this.CurrentFilter);
				}
			}
		}
		#endregion
		
		#region Constructor
		protected GenericGestionVM()
			: base() {
			this.PopulateItems();
			this.PopulateSpecificDatas();

			this.CreateAddItemCommand();
			this.CreateCloneSelectedItemCommand();
			this.CreateDeleteItemCommand();
			this.CreateSaveItemCommand();
			this.CreateShowDetailsCommand();
			this.CreateFilterCommand();
		}
		#endregion
		
		#region AddItemCommand
		public ICommand AddItemCommand { get; set; }
		
		private void CreateAddItemCommand() {
			this.AddItemCommand = new RelayCommand(
				this.ExecuteAddItemCommand,
				this.CanExecuteAdditemCommand
			);
		}
		
		public void ExecuteAddItemCommand() { 
			var item = this.CreateDefaultItem();
			
			if(!this.ItemExists(item)) {
				this.Context.Set<T>().Add(item);
				this.Context.SaveChanges();
				
				this.ShowUserNotification(string.Format(CultureInfo.CurrentCulture, ResCommon.TemplateAddItemNotification, item));		
				
				this.PopulateItems();
				this.SelectedItem = item;
				Messenger.Default.Send(new NMSelectionElement<T>(this.SelectedItem));
			}
			else {
				this.ShowUserNotification(ResCommon.Err_ElementExists);
			}
		}
			
		public bool CanExecuteAdditemCommand() {
			return true;
		}
		#endregion
		
		#region CloneSelectedItemCommand
		public ICommand CloneSelectedItemCommand { get; set; }
		
		private void CreateCloneSelectedItemCommand() {
			this.CloneSelectedItemCommand = new RelayCommand(
				this.ExecuteCloneSelectedItemCommand,
				this.CanExecuteCloneSelectedItemCommand
			);
		}
		
		public void ExecuteCloneSelectedItemCommand() {
			if(this.SelectedItem != null) {
				var item = this.CreateCloneItem();
				
				this.Context.Set<T>().Add(item);
				this.Context.SaveChanges();
				
				this.ShowUserNotification(string.Format(CultureInfo.CurrentCulture, ResCommon.TemplateCloneNotification, this.SelectedItem, item));		
				
				this.PopulateItems();
				this.SelectedItem = item;
				Messenger.Default.Send(new NMSelectionElement<T>(this.SelectedItem));
			}
		}
		
		public bool CanExecuteCloneSelectedItemCommand() {
			return this.SelectedItem != null;
		}
		#endregion

		#region DeleteCommand
		public ICommand DeleteItemCommand { get; set; }

		private void CreateDeleteItemCommand() {
			this.DeleteItemCommand = new RelayCommand(
				this.ExecuteDeleteItemCommand,
				this.CanExecuteDeleteItemCommand
			);
		}

		public bool CanExecuteDeleteItemCommand() {
			return this.SelectedItem != null 
				&& !this.ItemIsUsed(this.SelectedItem);
		}

		public virtual void ExecuteDeleteItemCommand() {
			if (this.SelectedItem != null) {
				var libelle = this.SelectedItem.ToString();

				this.Context.Entry(this.SelectedItem).State = EntityState.Deleted;
				this.Context.SaveChanges();
				
				this.ShowUserNotification(string.Format(CultureInfo.CurrentCulture, ResCommon.InfoElementSupprime, libelle));
				
				this.PopulateItems();
				this.SelectedItem = this.Items.FirstOrDefault();
				Messenger.Default.Send(new NMSelectionElement<T>(this.SelectedItem));
			}
		}
		#endregion

		#region ShowDetailsCommand
		public ICommand ShowDetailsCommand { get; set; }

		private void CreateShowDetailsCommand() {
			this.ShowDetailsCommand = new RelayCommand<object>(
				this.ExecuteShowDetailsCommand,
				this.CanExecuteShowDetailsCommand
			);
		}

		public virtual bool CanExecuteShowDetailsCommand(object selectedItem) {
			return true;
		}

		public virtual void ExecuteShowDetailsCommand(object selectedItem) {
			if (selectedItem != null && selectedItem is T && selectedItem != this.SelectedItem) {
				this.SelectedItem = (T)selectedItem;
				this.Context.Entry(this.SelectedItem).Reload();
				this.RaisePropertyChanged(()=>this.SelectedItem);
			}
		}
		#endregion

		#region FilterCommand
		public ICommand FilterCommand { get; set; }

		private void CreateFilterCommand() {
			this.FilterCommand = new RelayCommand<string>(
				this.ExecuteFilterCommand,
				this.CanExecuteFilterCommand
			);
		}

		public virtual bool CanExecuteFilterCommand(string filtre) {
			return true;
		}

		public virtual void ExecuteFilterCommand(string filtre) {
			this.CurrentFilter = filtre;
			this.PopulateItems();
		}
		#endregion
		
		#region SaveItemCommand
		public ICommand SaveItemCommand { get; set; }

		private void CreateSaveItemCommand() {
			this.SaveItemCommand = new RelayCommand(
				this.ExecuteSaveItemCommand,
				this.CanExecuteSaveItemCommand
			);
		}

		public virtual bool CanExecuteSaveItemCommand() {
			return this.SelectedItem != null;
		}

		public virtual void ExecuteSaveItemCommand() {
			if(this.SelectedItem != null) {
				this.FormatValues(this.SelectedItem);
				var errors = new Collection<string>();			
				
				if (this.ValidateItem(this.SelectedItem, errors)) {
					this.Context.Entry(this.SelectedItem).State = EntityState.Modified;
					this.Context.SaveChanges();
					this.ClearUserNotifications();
					
					this.PopulateItems();
					this.ShowUserNotification(string.Format(CultureInfo.CurrentCulture, ResCommon.TemplateSaveItemNotification, this.SelectedItem.ToString()));
					
					this.RaisePropertyChanged(()=>this.SelectedItem);
				}
				else {
					this.ShowUserNotifications(errors);
				}
			}
		}
		#endregion
		
		#region protected methods
		protected virtual void PopulateSpecificDatas() { }
		
		protected virtual void PopulateItems() {
			if (!string.IsNullOrEmpty(this.CurrentFilter)) {
				this.Items = this.Context.Set<T>().ToList()
					.Where(i => i.ToString().ToUpperInvariant().Contains(this.CurrentFilter.ToUpperInvariant()))
					.OrderBy(i => i.ToString());
			}
			else {
				this.Items = this.Context.Set<T>().ToList().OrderBy(i => i.ToString());
				Messenger.Default.Send(new NMClearFilter());
			}
		}
		
		protected virtual T CreateDefaultItem() {
			return new T();
		}
		
		protected virtual T CreateCloneItem() {
			return this.CreateDefaultItem();
		}
		
		protected virtual bool ValidateItem(T item, Collection<string> errors) {
			return true;
		}
		
		protected virtual bool ItemExists(T item) {
			return false;
		}
		
		protected virtual bool ItemIsUsed(T item) {
			return false;
		}
		
		protected virtual void FormatValues(T item) { }
		#endregion
	}
}
