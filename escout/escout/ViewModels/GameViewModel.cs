using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using escout.Helpers;
using escout.Models.Rest;
using escout.Services.Dependency;
using escout.Services.Rest;
using Newtonsoft.Json;
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
        private ObservableCollection<Game> games;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public GameViewModel(INavigation navigation)
        {
            Navigation = navigation;
            SearchCommand = new Command(SearchExecuted);
            SearchExecuted();
        }

        public ObservableCollection<Game> Games
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

            if (Key == "favorites")
            {
                Games = new ObservableCollection<Game>(await GetFavoriteGames());
            }
            else if (Key == null || string.IsNullOrEmpty(Value))
            {
                Games = new ObservableCollection<Game>(await GetGames(null));
            }
            else
            {
                Games = new ObservableCollection<Game>(await GetGames(new SearchQuery(Key, "iLIKE", Value + "%")));
            }

            IsVisible = false;

            if (Device.RuntimePlatform == Device.Android && Games != null)
            {
                DependencyService.Get<IToast>().LongAlert(string.Format(ConstValues.TOAST_RESULTS, Games.Count));
            }
        }

        private async Task<List<Game>> GetGames(SearchQuery query)
        {
            var _games = new List<Game>();
            var request = RestConnector.GAMES;

            if (query != null)
            {
                request += "?query=" + JsonConvert.SerializeObject(query);
            }

            var response = await RestConnector.GetObjectAsync(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                _games = JsonConvert.DeserializeObject<List<Game>>(await response.Content.ReadAsStringAsync());
            }

            return _games;
        }

        public async Task<Game> GetGameById(int id)
        {
            var game = new Game();
            var request = RestConnector.GAME + "?id=" + id;
            var response = await RestConnector.GetObjectAsync(request);

            if (200.Equals((int)response.StatusCode))
            {
                game = JsonConvert.DeserializeObject<Game>(await response.Content.ReadAsStringAsync());
            }

            return game;
        }

        private async Task<List<Game>> GetFavoriteGames()
        {
            var games = new List<Game>();
            var request = RestConnector.FAVORITES + "?query=gameId";
            var response = await RestConnector.GetObjectAsync(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var _favorites = JsonConvert.DeserializeObject<List<Favorite>>(await response.Content.ReadAsStringAsync());

                foreach (var f in _favorites)
                {
                    games.Add(await GetGameById(int.Parse(f.GameId.ToString())));
                }
            }

            return games;
        }
    }
}
