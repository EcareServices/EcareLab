using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvvmDataBinding.Model;
using MvvmDataBinding.ViewModel;
using Xamarin.Forms;

namespace MvvmDataBinding.Pages
{
    public partial class MainPage : ContentPage {
        private MainPageViewModel _viewModel;

        public MainPage() {

            _viewModel = new MainPageViewModel();
            BindingContext = _viewModel;

            InitializeComponent();
        }


        private void PersonList_OnItemTapped(object sender, ItemTappedEventArgs e) {
            var person = (Person) e.Item;
            DisplayAlert("Selection made", $"You tapped {person.FullName}", "Ok");
        }
    }
}
