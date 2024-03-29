﻿using escout.Models;
using escout.Models.Database;
using escout.Services.Database;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace escout.Views.Events
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EventsTimeLinePage : ContentPage
    {
        public EventsTimeLinePage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (Device.RuntimePlatform == Device.Android)
            {
                BtnBack.IsVisible = false;
            }

            LoadTimeLineEvents();
        }

        void BtnBack_Clicked(object sender, System.EventArgs e)
        {
            Navigation.PopModalAsync();
        }

        private void ItemTapped(object sender, ItemTappedEventArgs e)
        {
            timelineListView.SelectedItem = null;
        }

        private async void LoadTimeLineEvents()
        {
            var db = new Query();
            var timeLine = new ObservableCollection<TimeLineElement>();
            var events = new ObservableCollection<DbEvent>(await db.GetEvents());
            var gameEvents = new ObservableCollection<DbGameEvent>(await db.GetGameEvents()).
                Where(e => e.GameId == App.DbGame.Id && e.AthleteId == App.DbAthlete.Id).ToList();

            foreach (DbGameEvent gameEvent in gameEvents)
            {
                foreach (DbEvent e in events)
                {
                    if (gameEvent.EventId == e.Id && gameEvent.DataExt == e.DataExt)
                    {
                        var timeLineElement = new TimeLineElement
                        {
                            Name = e.Name,
                            ImageUrl = "timeline_" + e.Key + ".png",
                            Timer = gameEvent.GameTime
                        };
                        timeLine.Add(timeLineElement);
                    }
                }
            }
            timelineListView.ItemsSource = timeLine.OrderByDescending(e => e.Timer);
        }
    }
}