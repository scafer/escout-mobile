using escout.Helpers;
using escout.Models.Database;
using escout.Models.Rest;
using escout.Services.Database;
using escout.Services.Rest;
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
            StartGame();
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

        private void StartGame()
        {
            lbl_home_result.Text = game.HomeScore.ToString();
            lbl_visitor_result.Text = game.VisitorScore.ToString();

            if (game.DisplayOptions.ContainsKey("homeName"))
            {
                lbl_home.Text = game.DisplayOptions["homeName"];
            }

            if (game.DisplayOptions.ContainsKey("visitorName"))
            {
                lbl_visitor.Text = game.DisplayOptions["visitorName"];
            }

            if (game.DisplayOptions.ContainsKey("homeImageUrl"))
            {
                img_home.Source = game.DisplayOptions["homeImageUrl"];
            }

            if (game.DisplayOptions.ContainsKey("visitorImageUrl"))
            {
                img_visitor.Source = game.DisplayOptions["visitorImageUrl"];
            }
        }

        private async Task StartDbGame()
        {
            var clubs = await new Query().GetClubs(dbGame.DataExt);
            lbl_home.Text = clubs[0].Name;
            lbl_visitor.Text = clubs[1].Name;
            lbl_home_result.Text = dbGame.HomeScore.ToString();
            lbl_visitor_result.Text = dbGame.VisitorScore.ToString();
        }

        private async void AddGameToWatchList(object sender, EventArgs e)
        {
            var response = await DisplayAlert(ConstValues.TITLE_STATUS_INFO, ConstValues.MSG_ADD_TO_WATCHING, ConstValues.OPTION_YES, ConstValues.OPTION_NO);

            if (response)
            {
                await SaveGameData(game.Id);
            }
        }

        private async Task SaveGameData(int gameId)
        {
            var result = await new Query().GetDbGameById(gameId);

            if (result.Count == 0)
            {
                var response = await RestConnector.GetObjectAsync(RestConnector.GAME_DATA + "?gameId=" + gameId);
                if (!string.IsNullOrEmpty(await response.Content.ReadAsStringAsync()))
                {
                    try
                    {
                        var gameData = JsonConvert.DeserializeObject<Models.Rest.GameData>(await response.Content.ReadAsStringAsync());
                        var db = new Query();
                        _ = db.AddGameData(gameData);
                    }
                    catch (Exception e)
                    {
                        await DisplayAlert(ConstValues.TITLE_STATUS_ERROR, e.ToString(), ConstValues.OPTION_OK);
                    }
                }
            }
            else
                await DisplayAlert(ConstValues.TITLE_STATUS_WARNING, ConstValues.MSG_SAVE_GAME_ERROR, ConstValues.OPTION_OK);
        }

        private async void Start_OnClicked(object sender, EventArgs e)
        {
            App.DbGame = dbGame;
            await Navigation.PushAsync(new SelectAthletePage());
        }
    }
}
