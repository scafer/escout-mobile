using escout.Helpers;
using escout.Models;
using escout.Models.Rest;
using escout.Views.Events;
using escout.Views.GameData;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace escout.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : MasterDetailPage
    {
        private const string HOME = "Home";
        private const string WATCHING = "Watching";
        private const string EVENTS = "Events";
        private const string GAMES = "Games";
        private const string CLUBS = "Clubs";
        private const string BOARDS = "Boards";
        private const string ATHLETES = "Athletes";

        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            OptionsListView.ItemsSource = GetOptions();
            Detail = new NavigationPage(new HomePage());
            _ = GetData();
        }

        private void OptionsListView_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var option = e.SelectedItem as Option;

            switch (option.Name)
            {
                case HOME:
                    Detail = new NavigationPage(new HomePage());
                    break;
                case WATCHING:
                    Detail = new NavigationPage(new WatchingListPage());
                    break;
                case EVENTS:
                    Detail = new NavigationPage(new EventsPage());
                    break;
                case GAMES:
                    Detail = new NavigationPage(new GamesPage());
                    break;
                case CLUBS:
                    Detail = new NavigationPage(new ClubsPage());
                    break;
                case BOARDS:
                    Detail = new NavigationPage(new CompetitionBoardsPage());
                    break;
                case ATHLETES:
                    Detail = new NavigationPage(new AthletesPage());
                    break;
            }

            IsPresented = false;
        }

        private List<Option> GetOptions()
        {
            var option = new List<Option>
            {
                new(HOME, "home_fill.png"),
                new Option(WATCHING, "watching_fill.png"),
                new Option(GAMES, "games_fill.png"),
                new Option(CLUBS, "clubs_fill.png"),
                new Option(ATHLETES, "athletes_fill.png"),
                new Option(BOARDS, "boards_fill.png"),
                new Option(EVENTS, "sync_fill.png")
            };

            return option;
        }

        private void UserSettings_OnTapped(object sender, EventArgs e)
        {
            Detail = new NavigationPage(new UserDetailsPage());
            IsPresented = false;
        }

        private async Task GetData()
        {
            var response = await RestConnector.GetObjectAsync(RestConnector.USER);
            var user = JsonConvert.DeserializeObject<User>(await RestConnector.GetContent(response));

            if (user.ImageId != null)
            {
                var img = await RestUtils.GetImage(user.ImageId);
                if (!string.IsNullOrEmpty(img.ImageUrl))
                {
                    Img.Source = img.ImageUrl;
                    Img.Aspect = Aspect.AspectFill;
                }
            }
        }
    }
}