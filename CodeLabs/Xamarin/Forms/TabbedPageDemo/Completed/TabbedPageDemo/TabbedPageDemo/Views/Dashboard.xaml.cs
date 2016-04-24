using System;
using Xamarin.Forms;

namespace TabbedPageDemo.Views
{
    public partial class Dashboard : ContentPage {
        private readonly MainPage _mainPage;

        public Dashboard(MainPage mainPage) {
            InitializeComponent();
            _mainPage = mainPage;

            GotoClienten.Clicked += ClientenClicked;
            GotoTeams.Clicked += TeamsClicked;
        }

        public void ClientenClicked(object sender, EventArgs e) {
            _mainPage.CurrentPage = _mainPage.Clienten;
        }

        public void TeamsClicked(object sender, EventArgs e)
        {
            _mainPage.CurrentPage = _mainPage.Teams;
        }
    }
}
