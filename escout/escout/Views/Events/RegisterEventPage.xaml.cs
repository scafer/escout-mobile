using escout.Helpers;
using escout.Models.Database;
using escout.Services;
using escout.Services.Database;
using escout.Services.Rest;
using escout.ViewModels;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace escout.Views.Events
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterEventPage : ContentPage
    {
        private DbGame dbGame;
        private DbAthlete dbAthlete;

        private bool isGameRunning = false;

        public RegisterEventPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            BindingContext = App.DbGame;
            dbGame = App.DbGame;
            dbAthlete = App.DbAthlete;

            Title = dbAthlete.Name;

            _ = BackgroundAsync();

            if (isGameRunning)
            {
                btn_isRunning.Text = "Pause";
            }
            else
            {
                btn_isRunning.Text = "Resume";
            }
        }

        private void IsRunning_Clicked(object sender, EventArgs e)
        {
            if (isGameRunning)
            {
                isGameRunning = !isGameRunning;
                Pause_Clicked();
            }
            else
            {
                isGameRunning = !isGameRunning;
                Resume_Clicked();
            }
        }

        private void Pause_Clicked()
        {
            btn_isRunning.Text = "Resume";
            Event1Button.IsEnabled = false;
            Event2Button.IsEnabled = false;
            Event3Button.IsEnabled = false;
            Event4Button.IsEnabled = false;
            StopWatch.Stop();
        }

        private void Resume_Clicked()
        {
            if (string.IsNullOrEmpty(Event1Button.Text) && string.IsNullOrEmpty(Event2Button.Text) && string.IsNullOrEmpty(Event3Button.Text) && string.IsNullOrEmpty(Event4Button.Text))
                SetButtonBoard("Soccer001", "Soccer002", dbAthlete.PositionKey.Equals(1) ? "Soccer025" : "", ""); //Board01

            btn_isRunning.Text = "Pause";
            Event1Button.IsEnabled = true;
            Event2Button.IsEnabled = true;
            Event3Button.IsEnabled = true;
            Event4Button.IsEnabled = true;

            StopWatch.Start();
        }

        private async void EventExecuted(object sender, EventArgs e)
        {
            if (StopWatch.ShowTime() == "00:00")
            {
                if (await DisplayAlert(ConstValues.TITLE_STATUS_WARNING, ConstValues.MSG_GAME_START, ConstValues.OPTION_YES, ConstValues.OPTION_NO))
                    Resume_Clicked();
            }
            else
            {
                var button = sender as Button;
                if (!string.IsNullOrEmpty(button.Text))
                {
                    var text = button.Text;
                    PreviousEvent.Text = string.Format(ConstValues.MSG_PREVIOUS_EVENT, text);

                    if (button.Text != await GetEventName(ConstValues.EVENT_SOCCER_000))
                    {
                        var evt = new DbGameEvent()
                        {
                            Key = DateTime.UtcNow.Ticks.ToString(),
                            Time = DateTime.UtcNow.ToString(),
                            GameTime = LbStopwatch.Text,
                            GameId = dbGame.Id,
                            EventId = await new Query().GetEventId(text),
                            AthleteId = dbAthlete.Id,
                            ClubId = dbAthlete.ClubId,
                            DataExt = dbGame.DataExt
                        };

                        _ = GameEventViewModel.RegisterEvent(evt);
                    }

                    ButtonBoardLogic(text);
                }
            }
        }

        private async void Timer_Tapped(object sender, EventArgs e)
        {
            try
            {
                var result = await DisplayPromptAsync("Change Timer", "MM:SS", keyboard: Keyboard.Default);
                string[] values = result.Split(':');
                if (values.Length == 2)
                {
                    try
                    {
                        StopWatch.SetTimer(int.Parse(values[0]), int.Parse(values[1]));
                    }
                    catch (Exception ex)
                    {
                        await DisplayAlert(ConstValues.TITLE_STATUS_ERROR, ex.Message, ConstValues.OPTION_OK);
                    }
                }
                else
                    await DisplayAlert(ConstValues.TITLE_STATUS_ERROR, ConstValues.MSG_TIMER, ConstValues.OPTION_OK);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private async void LeaveItem_OnClicked(object sender, EventArgs e)
        {
            if (await App.DisplayMessage(ConstValues.TITLE_STATUS_WARNING, ConstValues.MSG_LEAVE, ConstValues.OPTION_NO, ConstValues.OPTION_YES))
            {
                _ = RestUtils.UpdateGameStatus(App.DbGame.Id, 2);
                Application.Current.MainPage = new NavigationPage(new MainPage());
                await Navigation.PushAsync(new MainPage());
            }
        }

        private void EventsTimeline_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new EventsTimeLinePage());
        }

        private async Task BackgroundAsync()
        {
            while (true)
            {
                LbStopwatch.Text = StopWatch.ShowTime();
                await Task.Delay(1000);
            }
        }

        private async void SetButtonBoard(string event1, string event2, string event3, string event4)
        {
            Event1Button.Text = await GetEventName(event1);
            Event2Button.Text = await GetEventName(event2);
            Event3Button.Text = await GetEventName(event3);
            Event4Button.Text = await GetEventName(event4);
        }

        private async void ButtonBoardLogic(string text)
        {
            if (await IsSameEvent(text, ConstValues.EVENT_SOCCER_000) || await IsSameEvent(text, ConstValues.EVENT_SOCCER_003) ||
                        await IsSameEvent(text, ConstValues.EVENT_SOCCER_006) || await IsSameEvent(text, ConstValues.EVENT_SOCCER_008) ||
                        await IsSameEvent(text, ConstValues.EVENT_SOCCER_009) || await IsSameEvent(text, ConstValues.EVENT_SOCCER_010) ||
                        await IsSameEvent(text, ConstValues.EVENT_SOCCER_011) || await IsSameEvent(text, ConstValues.EVENT_SOCCER_017) ||
                        await IsSameEvent(text, ConstValues.EVENT_SOCCER_018) || await IsSameEvent(text, ConstValues.EVENT_SOCCER_019) ||
                        await IsSameEvent(text, ConstValues.EVENT_SOCCER_020) || await IsSameEvent(text, ConstValues.EVENT_SOCCER_021) ||
                        await IsSameEvent(text, ConstValues.EVENT_SOCCER_024) || await IsSameEvent(text, ConstValues.EVENT_SOCCER_028))
            {
                SetButtonBoard(ConstValues.EVENT_SOCCER_001, ConstValues.EVENT_SOCCER_002, GetEventByPositionKey(dbAthlete.PositionKey), "");
            }
            else if (
                await IsSameEvent(text, ConstValues.EVENT_SOCCER_001) || await IsSameEvent(text, ConstValues.EVENT_SOCCER_014) ||
                await IsSameEvent(text, ConstValues.EVENT_SOCCER_016) || await IsSameEvent(text, ConstValues.EVENT_SOCCER_026) ||
                await IsSameEvent(text, ConstValues.EVENT_SOCCER_029))
            {
                SetButtonBoard(ConstValues.EVENT_SOCCER_004, ConstValues.EVENT_SOCCER_002, ConstValues.EVENT_SOCCER_003, ConstValues.EVENT_SOCCER_005);
            }
            else if (await IsSameEvent(text, ConstValues.EVENT_SOCCER_005))
            {
                SetButtonBoard(ConstValues.EVENT_SOCCER_006, ConstValues.EVENT_SOCCER_007, "", "");
            }
            else if (await IsSameEvent(text, ConstValues.EVENT_SOCCER_004))
            {
                SetButtonBoard(ConstValues.EVENT_SOCCER_011, ConstValues.EVENT_SOCCER_013, ConstValues.EVENT_SOCCER_010, ConstValues.EVENT_SOCCER_012);
            }
            else if (await IsSameEvent(text, ConstValues.EVENT_SOCCER_002))
            {
                SetButtonBoard(ConstValues.EVENT_SOCCER_015, ConstValues.EVENT_SOCCER_016, ConstValues.EVENT_SOCCER_014, dbAthlete.PositionKey.Equals(1) ? ConstValues.EVENT_SOCCER_026 : "");
            }
            else if (await IsSameEvent(text, ConstValues.EVENT_SOCCER_015))
            {
                SetButtonBoard(ConstValues.EVENT_SOCCER_018, ConstValues.EVENT_SOCCER_017, ConstValues.EVENT_SOCCER_000, "");
            }
            else if (await IsSameEvent(text, ConstValues.EVENT_SOCCER_012) || await IsSameEvent(text, ConstValues.EVENT_SOCCER_023))
            {
                SetButtonBoard(ConstValues.EVENT_SOCCER_019, ConstValues.EVENT_SOCCER_020, ConstValues.EVENT_SOCCER_021, "");
            }
            else if (await IsSameEvent(text, ConstValues.EVENT_SOCCER_013))
            {
                SetButtonBoard(ConstValues.EVENT_SOCCER_022, ConstValues.EVENT_SOCCER_023, "", "");
            }
            else if (await IsSameEvent(text, ConstValues.EVENT_SOCCER_022))
            {
                SetButtonBoard(ConstValues.EVENT_SOCCER_024, ConstValues.EVENT_SOCCER_021, "", "");
            }
            else if (await IsSameEvent(text, ConstValues.EVENT_SOCCER_007))
            {
                SetButtonBoard(ConstValues.EVENT_SOCCER_009, ConstValues.EVENT_SOCCER_008, "", "");
            }
            else if (await IsSameEvent(text, ConstValues.EVENT_SOCCER_025))
            {
                SetButtonBoard(ConstValues.EVENT_SOCCER_030, ConstValues.EVENT_SOCCER_029, "", "");
            }
            else if (await IsSameEvent(text, ConstValues.EVENT_SOCCER_027) || await IsSameEvent(text, ConstValues.EVENT_SOCCER_030))
            {
                SetButtonBoard(ConstValues.EVENT_SOCCER_027, ConstValues.EVENT_SOCCER_002, ConstValues.EVENT_SOCCER_028, "");
            }
        }

        private async Task<string> GetEventName(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                return string.Empty;
            }

            var dbEvents = await new Query().GetEvents(dbGame.DataExt);
            return dbEvents.Find(x => x.Key.Equals(key)).Name;
        }

        private string GetEventByPositionKey(int positionKey)
        {
            return positionKey == 1 ? ConstValues.EVENT_SOCCER_025 : "";
        }

        private async Task<bool> IsSameEvent(string text, string eventName)
        {
            return text.Equals(await GetEventName(eventName));
        }
    }
}
