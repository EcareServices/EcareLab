using LocalDatabase.Model;
using Xamarin.Forms;

namespace LocalDatabase.Views
{
    public partial class TodoItemList : ContentPage
    {
        public TodoItemList()
        {
            InitializeComponent();

            ToolbarItem tbi = null;
            if (Device.OS == TargetPlatform.iOS)
            {
                tbi = new ToolbarItem("+", null, () =>
                {
                    var todoItem = new TodoItem();
                    var todoPage = new TodoItemPage {
                        BindingContext = todoItem
                    };
                    Navigation.PushAsync(todoPage);
                });
            }
            if (Device.OS == TargetPlatform.Android)
            { // BUG: Android doesn't support the icon being null
                tbi = new ToolbarItem("+", "plus", () =>
                {
                    var todoItem = new TodoItem();
                    var todoPage = new TodoItemPage {
                        BindingContext = todoItem
                    };
                    Navigation.PushAsync(todoPage);
                });
            }
            if (Device.OS == TargetPlatform.WinPhone || Device.OS == TargetPlatform.Windows)
            {
                tbi = new ToolbarItem("Add", "add.png", () =>
                {
                    var todoItem = new TodoItem();
                    var todoPage = new TodoItemPage {
                        BindingContext = todoItem
                    };
                    Navigation.PushAsync(todoPage);
                });
            }

            ToolbarItems.Add(tbi);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            ListViewItems.ItemsSource = App.Database.GetItems();
        }

        void ListViewItemsSelected(object sender, SelectedItemChangedEventArgs e) {
            var todoItem = (TodoItem)e.SelectedItem;
            var todoPage = new TodoItemPage {
                BindingContext = todoItem
            };

            Navigation.PushAsync(todoPage);
        }
    }
}
