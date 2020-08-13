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

        private bool isLoadingData;
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
                    Athletes = new ObservableCollection<Athlete>(await GetAthletes(null));
            }
            else
            {
                Athletes = new ObservableCollection<Athlete>(await GetAthletes(new SearchQuery(Key, "iLIKE", Value + "%")));
            }

            IsLoadingData = false;

            if (Device.RuntimePlatform == Device.Android && Athletes != null)
                DependencyService.Get<IToast>().LongAlert(Athletes.Count + " results");
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
    }
}
