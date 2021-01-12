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
    class ClubViewModel : INotifyPropertyChanged
    {
        public INavigation Navigation;
        public ICommand SearchCommand { get; set; }

        private bool isBusy;
        private string key;
        private string pairValue;
        private ObservableCollection<Club> clubs;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ClubViewModel(INavigation navigation)
        {
            Navigation = navigation;
            SearchCommand = new Command(SearchExecuted);
        }

        public ObservableCollection<Club> Clubs
        {
            get => clubs;
            set
            {
                clubs = value;
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
                Clubs = new ObservableCollection<Club>(await GetFavoriteClubs());
            }
            else if (Key == null || string.IsNullOrEmpty(Value))
            {
                if (await App.DisplayMessage(Message.TITLE_STATUS_INFO, Message.LOAD_ALL_DATA_QUESTION, Message.OPTION_NO, Message.OPTION_YES))
                    Clubs = new ObservableCollection<Club>(await GetClubs(null));
            }
            else
            {
                Clubs = new ObservableCollection<Club>(await GetClubs(new SearchQuery(Key, "iLIKE", Value + "%")));
            }

            IsVisible = false;

            if (Device.RuntimePlatform == Device.Android && Clubs != null)
                DependencyService.Get<IToast>().LongAlert(Clubs.Count + Message.TOAST_RESULTS);
        }

        private async Task<List<Club>> GetClubs(SearchQuery query)
        {
            var _clubs = new List<Club>();
            var request = RestConnector.CLUBS;

            if (query != null)
                request += "?query=" + JsonConvert.SerializeObject(query);

            var response = await RestConnector.GetObjectAsync(request);
            if (!string.IsNullOrEmpty(await RestConnector.GetContent(response)))
                _clubs = JsonConvert.DeserializeObject<List<Club>>(await RestConnector.GetContent(response));

            return _clubs;
        }

        public async Task<Club> GetClubById(int? id)
        {
            var club = new Club();
            var request = RestConnector.CLUB + "?id=" + id;

            var response = await RestConnector.GetObjectAsync(request);
            if (!string.IsNullOrEmpty(await RestConnector.GetContent(response)))
                club = JsonConvert.DeserializeObject<Club>(await RestConnector.GetContent(response));

            return club;
        }

        private async Task<List<Club>> GetFavoriteClubs()
        {
            var clubs = new List<Club>();
            var request = RestConnector.FAVORITES + "?query=clubId";

            var favoriteResponse = await RestConnector.GetObjectAsync(request);
            if (!string.IsNullOrEmpty(await RestConnector.GetContent(favoriteResponse)))
            {
                var _favorites = JsonConvert.DeserializeObject<List<Favorite>>(await RestConnector.GetContent(favoriteResponse));
                foreach (var f in _favorites)
                    clubs.Add(await GetClubById(int.Parse(f.ClubId.ToString())));
            }

            return clubs;
        }
    }
}
