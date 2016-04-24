using Xamarin.Forms;

namespace TabbedPageDemo.Views {
    public class MainPage : TabbedPage {

        public readonly Dashboard Dashboard;
        public readonly Clienten Clienten;
        public readonly Teams Teams;

        public MainPage() {
            Children.Add(Dashboard = new Dashboard(this));
            Children.Add(Clienten = new Clienten());
            Children.Add(Teams = new Teams());
        }
    }
}
