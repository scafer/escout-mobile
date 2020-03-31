
using escout.Helpers;
using escout.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace escout.Views
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
            {
                listView.ItemsSource =
                    await GetCompetitionBoards(new SearchQuery(filter.Items[filter.SelectedIndex], "LIKE", key.Text)
                        .ToString());
            }

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
    }
}