
using escout.Models;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace escout.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GamesPage : ContentPage
    {
        public GamesPage()
        {
            InitializeComponent();
        }

        private void SearchExecuted(object sender, EventArgs e)
        {
            activityIndicator.IsRunning = true;
        }
        private void ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var game = e.SelectedItem as Game;
            Navigation.PushAsync(new GameDetailsPage(game));
        }
    }
}