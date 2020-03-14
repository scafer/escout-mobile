
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
    public partial class ClubsPage : ContentPage
    {
        public ClubsPage()
        {
            InitializeComponent();
        }

        private async void SearchExecuted(object sender, EventArgs e)
        {
            activityIndicator.IsRunning = true;
            if (string.IsNullOrEmpty(key.Text))
            {
                if (await DisplayAlert("Info", "Load all data?", "Yes", "Cancel"))
                    listView.ItemsSource = await GetClubs("");
            }
            else
            {
                listView.ItemsSource = await GetClubs(new SearchQuery(filter.Items[filter.SelectedIndex], "LIKE", key.Text).ToString());
            }
            activityIndicator.IsRunning = false;
        }

        private void ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var club = e.SelectedItem as Club;
            Navigation.PushAsync(new ClubDetailsPage(club));
        }

        private async Task<List<Club>> GetClubs(string query)
        {
            List<Club> clubs = new List<Club>();
            var response = await RestConnector.GetObjectAsync(RestConnector.Clubs + query);
            if (!string.IsNullOrEmpty(response))
            {
                clubs = JsonConvert.DeserializeObject<List<Club>>(response);
            }
            return clubs;
        }

        private void ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var club = e.SelectedItem as Club;
            Navigation.PushAsync(new ClubDetailsPage(club));
        }
    }
}