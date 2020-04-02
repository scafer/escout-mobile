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
        }

        public GameDetailsPage(DbGame dbGame)
        {
            InitializeComponent();
            BindingContext = dbGame;
            AddItem.IsEnabled = false;
            startbt.IsEnabled = true;
            this.dbGame = dbGame;
        }

        private async void AddGameToWatchList(object sender, EventArgs e)
        {
            var response = await DisplayAlert("Message", "Add to watch list?", "Ok", "Cancel");

            if (response)
            {
                await SaveGameData(game.Id);
            }
        }

        private async Task SaveGameData(int gameId)
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
                    await DisplayAlert("Error", e.ToString(), "Ok");
                }
            }
        }

        private async void Start_OnClicked(object sender, EventArgs e)
        {
            Utils.DbGame = dbGame;
            Application.Current.MainPage = new NavigationPage(new RegisterEventPage());
            await Navigation.PushAsync(new RegisterEventPage());
        }
    }
}