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
    class AthleteViewModel : INotifyPropertyChanged
    {
        public INavigation Navigation;
        public ICommand SearchCommand { get; set; }

        private bool isBusy;
        private string key;
        private string pairValue;
        private ObservableCollection<Athlete> athletes;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public AthleteViewModel(INavigation navigation)
        {
            Navigation = navigation;
            SearchCommand = new Command(SearchExecuted);
        }

        public ObservableCollection<Athlete> Athletes
        {
            get => athletes;
            set
            {
                athletes = value;
                OnPropertyChanged();
            }
        }

        public List<string> Filter
        {
            get
            {
                return new List<string>
                {
                    "name", "fullname", "favorites"
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
                Athletes = new ObservableCollection<Athlete>(await GetFavoriteAthletes());
            }
            else if (Key == null || string.IsNullOrEmpty(Value))
            {
                if (await App.DisplayMessage(Message.TITLE_STATUS_INFO, Message.LOAD_ALL_DATA_QUESTION, Message.OPTION_NO, Message.OPTION_YES))
                    Athletes = new ObservableCollection<Athlete>(await GetAthletes(null));
            }
            else
                Athletes = new ObservableCollection<Athlete>(await GetAthletes(new SearchQuery(Key, "iLIKE", Value + "%")));

            IsVisible = false;

            if (Device.RuntimePlatform == Device.Android && Athletes != null)
                DependencyService.Get<IToast>().LongAlert(Athletes.Count + Message.TOAST_RESULTS);
        }

        private async Task<List<Athlete>> GetAthletes(SearchQuery query)
        {
            var _athletes = new List<Athlete>();
            var request = RestConnector.Athletes;

            if (query != null)
                request += "?query=" + JsonConvert.SerializeObject(query);

            var response = await RestConnector.GetObjectAsync(request);
            if (!string.IsNullOrEmpty(response))
                _athletes = JsonConvert.DeserializeObject<List<Athlete>>(response);

            return _athletes;
        }

        public async Task<Athlete> GetAthleteById(int id)
        {
            var athlete = new Athlete();
            var request = RestConnector.Athlete + "?id=" + id;

            var response = await RestConnector.GetObjectAsync(request);
            if (!string.IsNullOrEmpty(response))
                athlete = JsonConvert.DeserializeObject<Athlete>(response);

            return athlete;
        }

        private async Task<List<Athlete>> GetFavoriteAthletes()
        {
            var athletes = new List<Athlete>();
            var request = RestConnector.FAVORITES + "?query=athleteId";

            var favoriteResponse = await RestConnector.GetObjectAsync(request);
            if (!string.IsNullOrEmpty(favoriteResponse))
            {
                var _favorites = JsonConvert.DeserializeObject<List<Favorite>>(favoriteResponse);
                foreach (var f in _favorites)
                    athletes.Add(await GetAthleteById(int.Parse(f.AthleteId.ToString())));
            }

            return athletes;
        }
    }
}
