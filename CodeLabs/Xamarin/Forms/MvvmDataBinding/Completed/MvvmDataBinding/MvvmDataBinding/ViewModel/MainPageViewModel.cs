using System.Collections.ObjectModel;
using System.Windows.Input;
using MvvmDataBinding.Model;
using MvvmDataBinding.Service;
using Xamarin.Forms;

namespace MvvmDataBinding.ViewModel
{
    public class MainPageViewModel {
        public ObservableCollection<Person> Persons { get; }
        
        public ICommand LoadPersonsCommand { get; set; }

        public MainPageViewModel() {
            Persons = new ObservableCollection<Person>();

            LoadPersonsCommand = new Command(LoadPersons);
            LoadPersons();
        }

        private void LoadPersons()
        {
            var persons = PersonService.Get(10);
            foreach (var character in persons)
            {
                Persons.Add(character);
            }
        }
        
    }
}
