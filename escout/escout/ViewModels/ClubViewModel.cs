using escout.Helpers;
using escout.Models.Rest;
using escout.Services.Dependency;
using escout.Services.Rest;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net;
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
            SearchExecuted();
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
                Clubs = new ObservableCollection<Club>(await GetClubs(null));
            }
            else
            {
                Clubs = new ObservableCollection<Club>(await GetClubs(new SearchQuery(Key, "iLIKE", Value + "%")));
            }

            IsVisible = false;

            if (Device.RuntimePlatform == Device.Android && Clubs != null)
            {
                DependencyService.Get<IToast>().LongAlert(string.Format(ConstValues.TOAST_RESULTS, Clubs.Count));
            }
        }

        private async Task<List<Club>> GetClubs(SearchQuery query)
        {
            var clubs = new List<Club>();
            var request = RestConnector.CLUBS;

            if (query != null)
            {
                request += "?query=" + JsonConvert.SerializeObject(query);
            }

            var response = await RestConnector.GetObjectAsync(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                clubs = JsonConvert.DeserializeObject<List<Club>>(await response.Content.ReadAsStringAsync());
            }

            return clubs;
        }

        public async Task<Club> GetClubById(int? id)
        {
            var club = new Club();
            var request = RestConnector.CLUB + "?id=" + id;
            var response = await RestConnector.GetObjectAsync(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                club = JsonConvert.DeserializeObject<Club>(await response.Content.ReadAsStringAsync());
            }

            return club;
        }

        private async Task<List<Club>> GetFavoriteClubs()
        {
            var clubs = new List<Club>();
            var request = RestConnector.FAVORITES + "?query=clubId";
            var response = await RestConnector.GetObjectAsync(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var favorites = JsonConvert.DeserializeObject<List<Favorite>>(await response.Content.ReadAsStringAsync());

                foreach (var f in favorites)
                {
                    clubs.Add(await GetClubById(int.Parse(f.ClubId.ToString())));
                }
            }

            return clubs;
        }
    }
}
