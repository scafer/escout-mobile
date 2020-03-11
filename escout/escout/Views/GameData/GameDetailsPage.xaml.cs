
using escout.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace escout.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GameDetailsPage : ContentPage
    {
        public GameDetailsPage(Game game)
        {
            InitializeComponent();
            BindingContext = game;
        }
    }
}