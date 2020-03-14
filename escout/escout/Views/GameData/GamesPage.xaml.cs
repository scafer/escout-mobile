
using escout.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using escout.Helpers;
using Newtonsoft.Json;
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

        private async void SearchExecuted(object sender, EventArgs e)
        {
            activityIndicator.IsRunning = true;
            if (string.IsNullOrEmpty(key.Text))
            {
                if (await DisplayAlert("Info", "Load all data?", "Yes", "Cancel"))
                    listView.ItemsSource = await GetGames("");
            }
            else
            {
                listView.ItemsSource = await GetGames(new SearchQuery(filter.Items[filter.SelectedIndex], "LIKE", key.Text).ToString());
            }
            activityIndicator.IsRunning = false;
        }
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
    }
}