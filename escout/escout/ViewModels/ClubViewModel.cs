﻿using escout.Helpers;
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

        private bool isLoadingData;
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
            this.Navigation = navigation;
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
                    Clubs = new ObservableCollection<Club>(await GetClubs());
            }
            else
            {
                Clubs = new ObservableCollection<Club>(await GetClubs(new SearchQuery(Key, "iLIKE", Value + "%")));
            }

            IsLoadingData = false;

            if (Device.RuntimePlatform == Device.Android && Clubs != null)
                DependencyService.Get<IToast>().LongAlert(Clubs.Count + " results");
        }

        private async Task<List<Club>> GetClubs()
        {
            var _clubs = new List<Club>();
            var response = await RestConnector.GetObjectAsync(RestConnector.Clubs);

            if (!string.IsNullOrEmpty(response))
                _clubs = JsonConvert.DeserializeObject<List<Club>>(response);

            return _clubs;
        }

        private async Task<List<Club>> GetClubs(SearchQuery query)
        {
            var _clubs = new List<Club>();
            var response = await RestConnector.GetObjectAsync(RestConnector.Clubs + "?query=" + JsonConvert.SerializeObject(query));

            if (!string.IsNullOrEmpty(response))
                _clubs = JsonConvert.DeserializeObject<List<Club>>(response);

            return _clubs;
        }
    }
}
