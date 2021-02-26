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
    class CompetitionBoardViewModel : INotifyPropertyChanged
    {
        public INavigation Navigation;
        public ICommand SearchCommand { get; set; }

        private bool isBusy;
        private string key;
        private string pairValue;
        private ObservableCollection<Competition> competitions;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public CompetitionBoardViewModel(INavigation navigation)
        {
            Navigation = navigation;
            SearchCommand = new Command(SearchExecuted);
        }

        public ObservableCollection<Competition> Competitions
        {
            get => competitions;
            set
            {
                competitions = value;
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
                Competitions = new ObservableCollection<Competition>(await GetFavoriteCompetitions());
            }
            else if (Key == null || string.IsNullOrEmpty(Value))
            {
                if (await App.DisplayMessage(Message.TITLE_STATUS_INFO, Message.MSG_LOAD_ALL_DATA_QUESTION, Message.OPTION_NO, Message.OPTION_YES))
                    Competitions = new ObservableCollection<Competition>(await GetCompetitions(null));
            }
            else
                Competitions = new ObservableCollection<Competition>(await GetCompetitions(new SearchQuery(Key, "iLIKE", Value + "%")));

            IsVisible = false;

            if (Device.RuntimePlatform == Device.Android && Competitions != null)
                DependencyService.Get<IToast>().LongAlert(Competitions.Count + Message.TOAST_RESULTS);
        }

        private async Task<List<Competition>> GetCompetitions(SearchQuery query)
        {
            var _competition = new List<Competition>();
            var request = RestConnector.COMPETITIONS;

            if (query != null)
                request += "?query=" + JsonConvert.SerializeObject(query);

            var response = await RestConnector.GetObjectAsync(request);
            if (200 == (int)response.StatusCode)
                _competition = JsonConvert.DeserializeObject<List<Competition>>(await response.Content.ReadAsStringAsync());

            return _competition;
        }

        public async Task<Competition> GetCompetitionById(int id)
        {
            var competition = new Competition();
            var request = RestConnector.COMPETITION + "?id=" + id;

            var response = await RestConnector.GetObjectAsync(request);
            if (200 == (int)response.StatusCode)
                competition = JsonConvert.DeserializeObject<Competition>(await response.Content.ReadAsStringAsync());

            return competition;
        }

        private async Task<List<Competition>> GetFavoriteCompetitions()
        {
            var competitions = new List<Competition>();
            var request = RestConnector.FAVORITES + "?query=competitionId";

            var response = await RestConnector.GetObjectAsync(request);
            if (200 == (int)response.StatusCode)
            {
                var _favorites = JsonConvert.DeserializeObject<List<Favorite>>(await response.Content.ReadAsStringAsync());
                foreach (var f in _favorites)
                    competitions.Add(await GetCompetitionById(int.Parse(f.CompetitionId.ToString())));
            }

            return competitions;
        }
    }
}
