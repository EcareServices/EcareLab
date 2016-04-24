using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace TabbedPageDemo.Views
{
    public partial class Teams : ContentPage
    {
        public Teams()
        {
            InitializeComponent();

            TeamsListView.ItemsSource = new[] {
                "Team Almelo",
                "Team Enschede",
                "Team Hengelo",
                "Team Oldenzaal"
            };
        }
    }
}
