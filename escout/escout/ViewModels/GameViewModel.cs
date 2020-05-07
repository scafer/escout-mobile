using escout.Helpers;
using escout.Helpers.Services;
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

        private bool isLoadingData;
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
                    {"name"}
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

        public bool IsLoadingData
        {
            get => this.isLoadingData;
            set
            {
                isLoadingData = value;
                OnPropertyChanged();
            }
        }

        private async void SearchExecuted()
        {
            IsLoadingData = true;

            if (Key == null || string.IsNullOrEmpty(Value))
            {
                if (await App.DisplayMessage("Info", "Load all data?", "Cancel", "Yes"))
                    Games = new ObservableCollection<Game>(await GetGames(null));
            }
            else
            {
                Games = new ObservableCollection<Game>(await GetGames(new SearchQuery(Key, "iLIKE", Value + "%")));
            }

            IsLoadingData = false;

            if (Device.RuntimePlatform == Device.Android && Games != null)
                DependencyService.Get<IToast>().LongAlert(Games.Count + " results");
        }

        private async Task<List<Game>> GetGames(SearchQuery query)
        {
            var _games = new List<Game>();
            var request = RestConnector.Games;

            if (query != null)
                request += "?query=" + JsonConvert.SerializeObject(query);

            var response = await RestConnector.GetObjectAsync(request);
            if (!string.IsNullOrEmpty(response))
                _games = JsonConvert.DeserializeObject<List<Game>>(response);

            return _games;
        }
    }
}
