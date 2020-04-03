using escout.Helpers;
using escout.Models.Rest;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace escout.Views.GameData
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CompetitionBoardsPage : ContentPage
    {
        public CompetitionBoardsPage()
        {
            InitializeComponent();
        }

        private async void SearchExecuted(object sender, EventArgs e)
        {
            activityIndicator.IsRunning = true;
            if (string.IsNullOrEmpty(key.Text))
            {
                if (await DisplayAlert("Info", "Load all data?", "Yes", "Cancel"))
                    listView.ItemsSource = await GetCompetitionBoards("");
            }
            else
                listView.ItemsSource =
                    await GetCompetitionBoards(new SearchQuery(filter.Items[filter.SelectedIndex], "iLIKE", key.Text + "%"));

            activityIndicator.IsRunning = false;
        }

        private void ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var competitionBoard = e.SelectedItem as CompetitionBoard;
            Navigation.PushAsync(new CompetitionBoardDetailsPage(competitionBoard));
        }

        private async Task<List<CompetitionBoard>> GetCompetitionBoards(string query)
        {
            List<CompetitionBoard> competitionBoards = new List<CompetitionBoard>();
            var response = await RestConnector.GetObjectAsync(RestConnector.CompetitionBoard + query);
            if (!string.IsNullOrEmpty(response))
            {
                competitionBoards = JsonConvert.DeserializeObject<List<CompetitionBoard>>(response);
            }

            return competitionBoards;
        }

        private async Task<List<CompetitionBoard>> GetCompetitionBoards(SearchQuery query)
        {
            var q = new StringContent(JsonConvert.SerializeObject(query), Encoding.UTF8, "application/json");

            List<CompetitionBoard> competitionBoards = new List<CompetitionBoard>();
            var response = await RestConnector.GetObjectAsync(RestConnector.CompetitionBoard + "?query=" + q);
            if (!string.IsNullOrEmpty(response))
            {
                competitionBoards = JsonConvert.DeserializeObject<List<CompetitionBoard>>(response);
            }

            return competitionBoards;
        }
    }
}