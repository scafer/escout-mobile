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
        private object key;
        private string pairValue;
        private ObservableCollection<Athlete> athletes;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public AthleteViewModel(INavigation navigation)
        {
            this.Navigation = navigation;
            SearchCommand = new Command(SearchExecuted);
        }

        public ObservableCollection<Athlete> Athletes
        {
            get => this.athletes;
            set
            {
                athletes = value;
                OnPropertyChanged();
            }
        }

        public object Key
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
                    Athletes = new ObservableCollection<Athlete>(await GetAthletes(""));
            }
            else
            {
                Athletes = new ObservableCollection<Athlete>(await GetAthletes(new SearchQuery(Key.ToString(), "iLike", Value + "%")));
            }

            IsLoadingData = false;

            if (Device.RuntimePlatform == Device.Android && Athletes != null)
                DependencyService.Get<IToast>().LongAlert(Athletes.Count + " results");
        }

        private async Task<List<Athlete>> GetAthletes(string query)
        {
            var _athletes = new List<Athlete>();
            var response = await RestConnector.GetObjectAsync(RestConnector.Athletes + query);

            if (!string.IsNullOrEmpty(response))
                _athletes = JsonConvert.DeserializeObject<List<Athlete>>(response);

            return _athletes;
        }

        private async Task<List<Athlete>> GetAthletes(SearchQuery query)
        {
            var _athletes = new List<Athlete>();
            var response = await RestConnector.GetObjectAsync(RestConnector.Athletes + "?query=" + JsonConvert.SerializeObject(query));

            if (!string.IsNullOrEmpty(response))
                _athletes = JsonConvert.DeserializeObject<List<Athlete>>(response);

            return _athletes;
        }
    }
}
