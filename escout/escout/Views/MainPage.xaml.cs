using escout.Helpers;
using escout.Models.Rest;
using escout.Views.GameData;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using escout.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace escout.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : MasterDetailPage
    {
        private const string HOME = "Home";
        private const string WATCHING = "Watching";
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
                new Option(HOME, "home_icon.png"),
                new Option(WATCHING, "watching_icon.png"),
                new Option(GAMES, "games_icon.png"),
                new Option(CLUBS, "clubs_icon.png"),
                new Option(ATHLETES, "athletes_icon.png"),
                new Option(BOARDS, "boards_icon.png")
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
            var response = RestConnector.GetObjectAsync(RestConnector.User);
            var user = JsonConvert.DeserializeObject<User>(await response);

            if (user.ImageId != null)
            {
                var img = await Utils.GetImage(user.ImageId);
                if (!String.IsNullOrEmpty(img.ImageUrl))
                {
                    Img.Source = img.ImageUrl;
                    Img.Aspect = Aspect.AspectFill;
                }
            }
        }
    }
}