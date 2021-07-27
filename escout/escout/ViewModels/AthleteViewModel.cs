using escout.Helpers;
using escout.Models.Rest;
using escout.Services;
using escout.Services.Dependency;
using escout.Services.Rest;
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
            SearchExecuted();
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
                Athletes = new ObservableCollection<Athlete>(await GetAthletes(null));
            }
            else
            {
                Athletes = new ObservableCollection<Athlete>(await GetAthletes(new SearchQuery(Key, "iLIKE", Value + "%")));
            }

            IsVisible = false;

            if (Device.RuntimePlatform == Device.Android && Athletes != null)
            {
                DependencyService.Get<IToast>().LongAlert(string.Format(ConstValues.TOAST_RESULTS, Athletes.Count));
            }
        }

        private async Task<List<Athlete>> GetAthletes(SearchQuery query)
        {
            var athletes = new List<Athlete>();
            var request = RestConnector.ATHLETES;

            if (query != null)
            {
                request += "?query=" + JsonConvert.SerializeObject(query);
            }

            var response = await RestConnector.GetObjectAsync(request);

            if (200 == (int)response.StatusCode)
            {
                athletes = JsonConvert.DeserializeObject<List<Athlete>>(await response.Content.ReadAsStringAsync());
            }

            return athletes;
        }

        public async Task<Athlete> GetAthleteById(int id)
        {
            var athlete = new Athlete();
            var response = await RestConnector.GetObjectAsync(RestConnector.ATHLETE + "?id=" + id);

            if (200 == (int)response.StatusCode)
            {
                athlete = JsonConvert.DeserializeObject<Athlete>(await response.Content.ReadAsStringAsync());
            }

            return athlete;
        }

        private async Task<List<Athlete>> GetFavoriteAthletes()
        {
            var athletes = new List<Athlete>();
            var response = await RestConnector.GetObjectAsync(RestConnector.FAVORITES + "?query=athleteId");

            if (200 == (int)response.StatusCode)
            {
                var favorites = JsonConvert.DeserializeObject<List<Favorite>>(await response.Content.ReadAsStringAsync());

                foreach (var favorite in favorites)
                {
                    athletes.Add(await GetAthleteById((int)favorite.AthleteId));
                }
            }

            return athletes;
        }
    }
}
