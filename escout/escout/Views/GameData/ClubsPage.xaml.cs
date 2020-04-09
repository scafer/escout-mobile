using escout.Helpers;
using escout.Models.Rest;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace escout.Views.GameData
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
            var clubs = new List<Club>();
            if (string.IsNullOrEmpty(key.Text))
            {
                if (await DisplayAlert("Info", "Load all data?", "Yes", "Cancel"))
                    clubs = await GetClubs("");
            }
            else
                clubs = await GetClubs(new SearchQuery(filter.Items[filter.SelectedIndex], "iLIKE", key.Text + "%"));

            listView.ItemsSource = clubs;
            activityIndicator.IsRunning = false;

            if (Device.RuntimePlatform == Device.Android)
                DependencyService.Get<IToast>().LongAlert(clubs.Count + " results");
        }

        private void ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var club = e.SelectedItem as Club;
            Navigation.PushAsync(new ClubDetailsPage(club));
        }

        private async Task<List<Club>> GetClubs(string query)
        {
            var clubs = new List<Club>();
            var response = await RestConnector.GetObjectAsync(RestConnector.Clubs);

            if (!string.IsNullOrEmpty(response))
                clubs = JsonConvert.DeserializeObject<List<Club>>(response);

            return clubs;
        }

        private async Task<List<Club>> GetClubs(SearchQuery query)
        {
            var clubs = new List<Club>();
            var response = await RestConnector.GetObjectAsync(RestConnector.Clubs + "?query=" + JsonConvert.SerializeObject(query));

            if (!string.IsNullOrEmpty(response))
                clubs = JsonConvert.DeserializeObject<List<Club>>(response);

            return clubs;
        }
    }
}