using System;
using LocalDatabase.Model;
using Xamarin.Forms;

namespace LocalDatabase.Views
{
    public partial class TodoItemPage : ContentPage
    {
        public TodoItemPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, true);
        }

        void SaveClicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        void DeleteClicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        void CancelClicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
    }
}
