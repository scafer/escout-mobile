using escout.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace escout.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ClubDetailsPage : ContentPage
    {
        public ClubDetailsPage(Club club)
        {
            InitializeComponent();
        }
    }
}