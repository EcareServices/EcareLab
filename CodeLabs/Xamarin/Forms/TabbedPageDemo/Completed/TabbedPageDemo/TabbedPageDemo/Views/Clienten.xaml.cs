using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace TabbedPageDemo.Views
{
    public partial class Clienten : ContentPage
    {
        public Clienten()
        {
            InitializeComponent();
            ClientenListView.ItemsSource = new[] {
                "Aniek van Deuveren",
                "Jelke van Zaanen",
                "Şeyma Sonnemans",
                "Willemien Bik",
                "Agnes van Workum",
                "Machteld Kommers",
                "Sabrien Kunst",
                "Enya van Binsbergen",
                "Sieger van Vuuren"
            };
        }
    }
}
