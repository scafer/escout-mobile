using escout.Helpers;
using escout.Models.Db;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace escout.Views.Events
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SelectAthletePage : ContentPage
    {
        private List<DbClub> dbClubs;

        public SelectAthletePage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            dbClubs = await new LocalDb().GetClubs(App.DbGame.DataExt);
            bt_home.Text = dbClubs[0].Name;
            bt_visitor.Text = dbClubs[1].Name;
        }

        private async void Bt_home_OnClicked(object sender, EventArgs e)
        {
            listView.ItemsSource = await new LocalDb().GetAthletes(App.DbGame.DataExt, dbClubs[0].Id);
        }

        private async void Bt_visitor_OnClicked(object sender, EventArgs e)
        {
            listView.ItemsSource = await new LocalDb().GetAthletes(App.DbGame.DataExt, dbClubs[1].Id);
        }

        private async void Button_OnClicked(object sender, EventArgs e)
        {
            if (listView.SelectedItem != null)
            {
                App.DbAthlete = listView.SelectedItem as DbAthlete;
                Application.Current.MainPage = new NavigationPage(new RegisterEventPage());
                await Navigation.PushAsync(new RegisterEventPage());
            }
        }
    }
}