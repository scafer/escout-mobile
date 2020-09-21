using escout.Models;
using escout.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace escout.Views.GameData
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GamesPage : ContentPage
    {
        public GamesPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            BindingContext = new GameViewModel(Navigation);
        }

        private void ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var game = e.SelectedItem as GameWithClub;
            Navigation.PushAsync(new GameDetailsPage(game.Game));
        }
    }
}