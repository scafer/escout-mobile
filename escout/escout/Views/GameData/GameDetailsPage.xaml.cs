using escout.Helpers;
using escout.Models.Db;
using escout.Models.Rest;
using escout.Views.Events;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace escout.Views.GameData
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GameDetailsPage : ContentPage
    {
        private readonly Game game;
        private readonly DbGame dbGame;

        public GameDetailsPage(Game game)
        {
            InitializeComponent();
            BindingContext = game;
            this.game = game;
            _ = StartGame();
        }

        public GameDetailsPage(DbGame dbGame)
        {
            InitializeComponent();
            BindingContext = dbGame;
            AddItem.Text = "";
            AddItem.IsEnabled = false;
            startbt.IsVisible = true;
            this.dbGame = dbGame;
            _ = StartDbGame();
        }

        private async Task StartGame()
        {
            var homeTeam = await RestUtils.GetClub(game.HomeId);
            var visitorTeam = await RestUtils.GetClub(game.VisitorId);

            lbl_home.Text = homeTeam.Name;
            lbl_visitor.Text = visitorTeam.Name;
            lbl_home_result.Text = game.HomeScore.ToString();
            lbl_visitor_result.Text = game.VisitorScore.ToString();

            if (homeTeam.ImageId != null)
            {
                var img1 = await RestUtils.GetImage(homeTeam.ImageId);
                if (!string.IsNullOrEmpty(img1.ImageUrl))
                    img_home.Source = img1.ImageUrl;
            }

            if (visitorTeam.ImageId != null)
            {
                var img2 = await RestUtils.GetImage(visitorTeam.ImageId);
                if (!string.IsNullOrEmpty(img2.ImageUrl))
                    img_visitor.Source = img2.ImageUrl;
            }
        }

        private async Task StartDbGame()
        {
            var clubs = await new LocalDb().GetClubs(dbGame.DataExt);
            lbl_home.Text = clubs[0].Name;
            lbl_visitor.Text = clubs[1].Name;
            lbl_home_result.Text = dbGame.HomeScore.ToString();
            lbl_visitor_result.Text = dbGame.VisitorScore.ToString();
        }

        private async void AddGameToWatchList(object sender, EventArgs e)
        {
            var response = await DisplayAlert(Message.TITLE_STATUS_INFO, Message.ADD_TO_WATCHING, Message.OPTION_YES, Message.OPTION_NO);

            if (response)
            {
                await SaveGameData(game.Id);
            }
        }

        private async Task SaveGameData(int gameId)
        {
            var result = await new LocalDb().GetDbGameById(gameId);

            if (result.Count == 0)
            {
                var response = await RestConnector.GetObjectAsync(RestConnector.GameData + "?gameId=" + gameId);
                if (!string.IsNullOrEmpty(response))
                {
                    try
                    {
                        var gameData = JsonConvert.DeserializeObject<Models.Rest.GameData>(response);
                        var db = new LocalDb();
                        _ = db.AddGameData(gameData);
                    }
                    catch (Exception e)
                    {
                        await DisplayAlert(Message.TITLE_STATUS_ERROR, e.ToString(), Message.OPTION_OK);
                    }
                }
            }
            else
                await DisplayAlert(Message.TITLE_STATUS_WARNING, Message.SAVE_GAME_ERROR, Message.OPTION_OK);
        }

        private async void Start_OnClicked(object sender, EventArgs e)
        {
            App.DbGame = dbGame;
            await Navigation.PushAsync(new SelectAthletePage());
        }
    }
}