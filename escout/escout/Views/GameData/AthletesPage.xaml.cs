
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
            if(string.IsNullOrEmpty(filter.Items[filter.SelectedIndex]) || string.IsNullOrEmpty(key.Text))
                if(await DisplayAlert("Info", "Load all data?", "Yes", "Cancel"))
                    listView.ItemsSource = await GetAthletes(null);
            else
                listView.ItemsSource = await GetAthletes(new SearchQuery(filter.Items[filter.SelectedIndex], "LIKE", key.Text));
        }

        private void ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var athlete = e.SelectedItem as Athlete;
            Navigation.PushAsync(new AthleteDetailsPage(athlete));
        }

        private async Task<List<Athlete>> GetAthletes(SearchQuery query)
        {
            List<Athlete> athletes = new List<Athlete>();
            var response = await RestConnector.PostObjectAsync(RestConnector.Athletes, query);
            if (!string.IsNullOrEmpty(response))
            {
                athletes= JsonConvert.DeserializeObject<List<Athlete>>(response);
            }
            return athletes;
        }
    }
}