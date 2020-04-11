using escout.Helpers;
using escout.Models.Rest;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using escout.Helpers.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace escout.Views.GameData
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GamesPage : ContentPage
    {
        public GamesPage() => InitializeComponent();

        private void ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var game = e.SelectedItem as Game;
            Navigation.PushAsync(new GameDetailsPage(game));
        }

        private async Task<List<Game>> GetGames(string query)
        {
            List<Game> games = new List<Game>();
            var response = await RestConnector.GetObjectAsync(RestConnector.Games + query);
            if (!string.IsNullOrEmpty(response))
            {
                games = JsonConvert.DeserializeObject<List<Game>>(response);
            }
            return games;
        }

        private async void SearchExecuted(object sender, EventArgs e)
        {
            activityIndicator.IsRunning = true;
            var games = new List<Game>();

            if (string.IsNullOrEmpty(key.Text))
            {
                if (await DisplayAlert("Info", "Load all data?", "Yes", "Cancel"))
                    games = await GetGames("");
            }
            else
            {
                games = await GetGames(new SearchQuery(filter.Items[filter.SelectedIndex], "iLIKE", key.Text + "%").ToString());
            }

            listView.ItemsSource = games;
            activityIndicator.IsRunning = false;

            if (Device.RuntimePlatform == Device.Android)
                DependencyService.Get<IToast>().LongAlert(games.Count + " results");
        }
    }
}