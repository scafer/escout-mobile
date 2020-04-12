using escout.Helpers;
using escout.Helpers.Services;
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
    public partial class AthletesPage : ContentPage
    {
        public AthletesPage() => InitializeComponent();

        protected override void OnAppearing()
        {
            base.OnAppearing();
            filter.ItemsSource = Utils.AthleteFilter;
        }

        private async void SearchExecuted(object sender, EventArgs e)
        {
            activityIndicator.IsRunning = true;
            var athletes = new List<Athlete>();

            if (string.IsNullOrEmpty(key.Text))
            {
                if (await DisplayAlert("Info", "Load all data?", "Yes", "Cancel"))
                    athletes = await GetAthletes("");
            }
            else
                athletes = await GetAthletes(new SearchQuery(filter.Items[filter.SelectedIndex], "iLIKE", key.Text + "%"));

            listView.ItemsSource = athletes;
            activityIndicator.IsRunning = false;

            if (Device.RuntimePlatform == Device.Android)
                DependencyService.Get<IToast>().LongAlert(athletes.Count + " results");
        }

        private void ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var athlete = e.SelectedItem as Athlete;
            Navigation.PushAsync(new AthleteDetailsPage(athlete));
        }

        private async Task<List<Athlete>> GetAthletes(string query)
        {
            var athletes = new List<Athlete>();
            var response = await RestConnector.GetObjectAsync(RestConnector.Athletes + query);

            if (!string.IsNullOrEmpty(response))
                athletes = JsonConvert.DeserializeObject<List<Athlete>>(response);

            return athletes;
        }

        private async Task<List<Athlete>> GetAthletes(SearchQuery query)
        {
            var athletes = new List<Athlete>();
            var response = await RestConnector.GetObjectAsync(RestConnector.Athletes + "?query=" + JsonConvert.SerializeObject(query));

            if (!string.IsNullOrEmpty(response))
                athletes = JsonConvert.DeserializeObject<List<Athlete>>(response);

            return athletes;
        }
    }
}