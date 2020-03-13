
using escout.Models;
using System;
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

        private void SearchExecuted(object sender, EventArgs e)
        {
            activityIndicator.IsRunning = true;
        }

        private void ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var club = e.SelectedItem as Club;
            Navigation.PushAsync(new ClubDetailsPage(club));
        }
    }
}