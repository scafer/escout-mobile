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
    class CompetitionBoardViewModel : INotifyPropertyChanged
    {
        public INavigation Navigation;
        public ICommand SearchCommand { get; set; }

        private bool isLoadingData;
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
            get => isLoadingData;
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
                    Competitions = new ObservableCollection<Competition>(await GetCompetitionBoards());
            }
            else
            {
                Competitions = new ObservableCollection<Competition>(await GetCompetitionBoards(new SearchQuery(Key, "iLIKE", Value + "%")));
            }

            IsLoadingData = false;

            if (Device.RuntimePlatform == Device.Android && Competitions != null)
                DependencyService.Get<IToast>().LongAlert(Competitions.Count + " results");
        }

        private async Task<List<Competition>> GetCompetitionBoards()
        {
            var _competitions = new List<Competition>();
            var response = await RestConnector.GetObjectAsync(RestConnector.Competitions);

            if (!string.IsNullOrEmpty(response))
                _competitions = JsonConvert.DeserializeObject<List<Competition>>(response);

            return _competitions;
        }

        private async Task<List<Competition>> GetCompetitionBoards(SearchQuery query)
        {
            var _competition = new List<Competition>();
            var response = await RestConnector.GetObjectAsync(RestConnector.Competitions + "?query=" + JsonConvert.SerializeObject(query));

            if (!string.IsNullOrEmpty(response))
                _competition = JsonConvert.DeserializeObject<List<Competition>>(response);

            return _competition;
        }
    }
}
