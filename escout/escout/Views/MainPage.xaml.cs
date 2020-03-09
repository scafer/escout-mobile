using escout.Models;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace escout.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : MasterDetailPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            OptionsListView.ItemsSource = GetOptions();
            Detail = new NavigationPage(new UserDetailsPage());
        }

        private void OptionsListView_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var option = e.SelectedItem as Option;

            switch (option.Name)
            {
                case "Home":
                    Detail = new NavigationPage(new UserDetailsPage());
                    break;
                case "Watching":
                    Detail = new NavigationPage(new WatchingListPage());
                    break;
                case "Games":
                    Detail = new NavigationPage(new GamesPage());
                    break;
                case "Clubs":
                    Detail = new NavigationPage(new ClubsPage());
                    break;
                case "Boards":
                    Detail = new NavigationPage(new CompetitionBoardsPage());
                    break;
                case "Athletes":
                    Detail = new NavigationPage(new AthletesPage());
                    break;
            }

            IsPresented = false;
        }

        private List<Option> GetOptions()
        {
            var option = new List<Option>
            {
                new Option{Name = "Home", ImageUrl = ""},
                new Option{Name = "Watching", ImageUrl = ""},
                new Option{Name = "Games", ImageUrl = ""},
                new Option{Name = "Clubs", ImageUrl = ""},
                new Option{Name = "Boards", ImageUrl = ""},
                new Option{Name = "Athletes", ImageUrl = ""}
            };

            return option;
        }

        private void UserSettings_OnTapped(object sender, EventArgs e)
        {
            Detail = new NavigationPage(new UserDetailsPage());
            IsPresented = false;
        }
    }
}