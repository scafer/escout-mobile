using escout.Helpers;
using escout.Helpers.Services;
using escout.Models;
using escout.Models.Rest;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace escout.ViewModels
{
    class GameViewModel : INotifyPropertyChanged
    {
        public INavigation Navigation;
        public ICommand SearchCommand { get; set; }

        private bool isBusy;
        private string key;
        private string pairValue;
        private ObservableCollection<GameWithClub> games;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public GameViewModel(INavigation navigation)
        {
            Navigation = navigation;
            SearchCommand = new Command(SearchExecuted);
        }

        public ObservableCollection<GameWithClub> Games
        {
            get => games;
            set
            {
                games = value;
                OnPropertyChanged();
            }
        }

        public List<string> Filter
        {
            get
            {
                return new List<string>
                {
                    "name", "favorites"
                };
            }
        }

        public string Key
        {
            get => key;
            set
            {
                key = value;
                OnPropertyChanged();
            }
        }

        public string Value
        {
            get => pairValue;
            set
            {
                pairValue = value;
                OnPropertyChanged();
            }
        }

        public bool IsBusy
        {
            get => isBusy;
            set
            {
                isBusy = value;
                OnPropertyChanged();
            }
        }

        public bool IsVisible
        {
            get => !IsBusy;
            set
            {
                IsBusy = value;
                OnPropertyChanged();
            }
        }

        private async void SearchExecuted()
        {
            IsVisible = true;
            var gameWithClub = new List<GameWithClub>();

            if (Key == "favorites")
            {
                foreach (var x in await GetFavoriteGames())
                {
                    var a = new GameWithClub(x)
                    {
                        ClubHome = await RestUtils.GetClub(x.HomeId),
                        ClubVisitor = await RestUtils.GetClub(x.VisitorId)
                    };
                    gameWithClub.Add(a);
                }

                Games = new ObservableCollection<GameWithClub>(gameWithClub);
            }
            else if (Key == null || string.IsNullOrEmpty(Value))
            {
                if (await App.DisplayMessage(Message.TITLE_STATUS_INFO, Message.LOAD_ALL_DATA_QUESTION, Message.OPTION_NO, Message.OPTION_YES))
                {
                    foreach (var x in await GetGames(null))
                    {
                        var a = new GameWithClub(x)
                        {
                            ClubHome = await RestUtils.GetClub(x.HomeId),
                            ClubVisitor = await RestUtils.GetClub(x.VisitorId)
                        };
                        gameWithClub.Add(a);
                    }

                    Games = new ObservableCollection<GameWithClub>(gameWithClub);
                }
            }
            else
            {
                foreach (var x in await GetGames(new SearchQuery(Key, "iLIKE", Value + "%")))
                {
                    var a = new GameWithClub(x)
                    {
                        ClubHome = await RestUtils.GetClub(x.HomeId),
                        ClubVisitor = await RestUtils.GetClub(x.VisitorId)
                    };
                    gameWithClub.Add(a);
                }

                Games = new ObservableCollection<GameWithClub>(gameWithClub);
            }

            IsVisible = false;

            if (Device.RuntimePlatform == Device.Android && Games != null)
                DependencyService.Get<IToast>().LongAlert(Games.Count + Message.TOAST_RESULTS);
        }

        private async Task<List<Game>> GetGames(SearchQuery query)
        {
            var _games = new List<Game>();
            var request = RestConnector.GAMES;

            if (query != null)
                request += "?query=" + JsonConvert.SerializeObject(query);

            var response = await RestConnector.GetObjectAsync(request);
            if (!string.IsNullOrEmpty(response))
                _games = JsonConvert.DeserializeObject<List<Game>>(response);

            return _games;
        }

        public async Task<Game> GetGameById(int id)
        {
            var game = new Game();
            var request = RestConnector.GAME + "?id=" + id;

            var response = await RestConnector.GetObjectAsync(request);
            if (!string.IsNullOrEmpty(response))
                game = JsonConvert.DeserializeObject<Game>(response);

            return game;
        }

        private async Task<List<Game>> GetFavoriteGames()
        {
            var games = new List<Game>();
            var request = RestConnector.FAVORITES + "?query=gameId";

            var favoriteResponse = await RestConnector.GetObjectAsync(request);
            if (!string.IsNullOrEmpty(favoriteResponse))
            {
                var _favorites = JsonConvert.DeserializeObject<List<Favorite>>(favoriteResponse);
                foreach (var f in _favorites)
                    games.Add(await GetGameById(int.Parse(f.GameId.ToString())));
            }

            return games;
        }
    }
}
