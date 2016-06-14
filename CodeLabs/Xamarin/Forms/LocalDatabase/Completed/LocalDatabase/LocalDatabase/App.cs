using LocalDatabase.Db;
using LocalDatabase.Views;
using Xamarin.Forms;

namespace LocalDatabase
{
    public class App : Application {
        private static Repository _database;
        public static Repository Database => _database ?? (_database = new Repository());

        public App()
        {
            Resources = new ResourceDictionary();
            Resources.Add("primaryGreen", Color.FromHex("91CA47"));
            Resources.Add("primaryDarkGreen", Color.FromHex("6FA22E"));

            var nav = new NavigationPage(new TodoItemList());
            nav.BarBackgroundColor = (Color)Current.Resources["primaryGreen"];
            nav.BarTextColor = Color.White;

            MainPage = nav;
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
