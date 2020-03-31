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
    public partial class AthletesPage : ContentPage
    {
        public AthletesPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            filter.ItemsSource = Utils.AthleteFilter;
        }

        private async void SearchExecuted(object sender, EventArgs e)
        {
            activityIndicator.IsRunning = true;
            if (string.IsNullOrEmpty(key.Text))
            {
                if (await DisplayAlert("Info", "Load all data?", "Yes", "Cancel"))
                    listView.ItemsSource = await GetAthletes("");
            }
            else
            {
                listView.ItemsSource = await GetAthletes(new SearchQuery(filter.Items[filter.SelectedIndex], "LIKE", key.Text).ToString());
            }
            activityIndicator.IsRunning = false;

        }

        private void ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var athlete = e.SelectedItem as Athlete;
            Navigation.PushAsync(new AthleteDetailsPage(athlete));
        }

        private async Task<List<Athlete>> GetAthletes(string query)
        {
            List<Athlete> athletes = new List<Athlete>();
            var response = await RestConnector.GetObjectAsync(RestConnector.Athletes + query);
            if (!string.IsNullOrEmpty(response))
            {
                athletes = JsonConvert.DeserializeObject<List<Athlete>>(response);
            }
            return athletes;
        }
    }
}