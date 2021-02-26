using escout.Models;
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
                btn_isRunning.Text = "Pause";
            else
                btn_isRunning.Text = "Resume";
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
                if (await DisplayAlert(Message.TITLE_STATUS_WARNING, Message.MSG_GAME_START, Message.OPTION_YES, Message.OPTION_NO))
                    Resume_Clicked();
            }
            else
            {
                var button = sender as Button;
                if (!string.IsNullOrEmpty(button.Text))
                {
                    var text = button.Text;

                    PreviousEvent.Text = "Previous Event: " + text;

                    if (button.Text != await GetEventName("Soccer000"))
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

                    if(text.Equals(await GetEventName("Soccer000"))) //Board01
                        SetButtonBoard("Soccer001", "Soccer002", dbAthlete.PositionKey.Equals(1) ? "Soccer025" : "", "");
                    else if (text.Equals(await GetEventName("Soccer001"))) //Board02
                        SetButtonBoard("Soccer004", "Soccer002", "Soccer003", "Soccer005");
                    else if (text.Equals(await GetEventName("Soccer002"))) //Board05
                        SetButtonBoard("Soccer015", "Soccer016", "Soccer014", dbAthlete.PositionKey.Equals(1) ? "Soccer026" : "");
                    else if (text.Equals(await GetEventName("Soccer003"))) //Board01
                        SetButtonBoard("Soccer001", "Soccer002", dbAthlete.PositionKey.Equals(1) ? "Soccer025" : "", "");
                    else if (text.Equals(await GetEventName("Soccer004"))) //Board04
                        SetButtonBoard("Soccer011", "Soccer013", "Soccer010", "Soccer012");
                    else if (text.Equals(await GetEventName("Soccer005"))) //Board03
                        SetButtonBoard("Soccer006", "Soccer007", "", "");
                    else if (text.Equals(await GetEventName("Soccer006"))) //Board01
                        SetButtonBoard("Soccer001", "Soccer002", dbAthlete.PositionKey.Equals(1) ? "Soccer025" : "", "");
                    else if (text.Equals(await GetEventName("Soccer007"))) //Board11
                        SetButtonBoard("Soccer009", "Soccer008", "", "");
                    else if (text.Equals(await GetEventName("Soccer008"))) //Board01
                        SetButtonBoard("Soccer001", "Soccer002", dbAthlete.PositionKey.Equals(1) ? "Soccer025" : "", "");
                    else if (text.Equals(await GetEventName("Soccer009"))) //Board01
                        SetButtonBoard("Soccer001", "Soccer002", dbAthlete.PositionKey.Equals(1) ? "Soccer025" : "", "");
                    else if (text.Equals(await GetEventName("Soccer010"))) //Board01
                        SetButtonBoard("Soccer001", "Soccer002", dbAthlete.PositionKey.Equals(1) ? "Soccer025" : "", "");
                    else if (text.Equals(await GetEventName("Soccer011"))) //Board01
                        SetButtonBoard("Soccer001", "Soccer002", dbAthlete.PositionKey.Equals(1) ? "Soccer025" : "", "");
                    else if (text.Equals(await GetEventName("Soccer012"))) //Board07
                        SetButtonBoard("Soccer019", "Soccer020", "Soccer021", "");
                    else if (text.Equals(await GetEventName("Soccer013"))) //Board08
                        SetButtonBoard("Soccer022", "Soccer023", "", "");
                    else if (text.Equals(await GetEventName("Soccer014"))) //Board02
                        SetButtonBoard("Soccer004", "Soccer002", "Soccer003", "Soccer005");
                    else if (text.Equals(await GetEventName("Soccer015"))) //Board06
                        SetButtonBoard("Soccer018", "Soccer017", "Soccer000", "");
                    else if (text.Equals(await GetEventName("Soccer016"))) //Board02
                        SetButtonBoard("Soccer004", "Soccer002", "Soccer003", "Soccer005");
                    else if (text.Equals(await GetEventName("Soccer017"))) //Board01
                        SetButtonBoard("Soccer001", "Soccer002", dbAthlete.PositionKey.Equals(1) ? "Soccer025" : "", "");
                    else if (text.Equals(await GetEventName("Soccer018"))) //Board01
                        SetButtonBoard("Soccer001", "Soccer002", dbAthlete.PositionKey.Equals(1) ? "Soccer025" : "", "");
                    else if (text.Equals(await GetEventName("Soccer019"))) //Board01
                        SetButtonBoard("Soccer001", "Soccer002", dbAthlete.PositionKey.Equals(1) ? "Soccer025" : "", "");
                    else if (text.Equals(await GetEventName("Soccer020"))) //Board01
                        SetButtonBoard("Soccer001", "Soccer002", dbAthlete.PositionKey.Equals(1) ? "Soccer025" : "", "");
                    else if (text.Equals(await GetEventName("Soccer021"))) //Board01
                        SetButtonBoard("Soccer001", "Soccer002", dbAthlete.PositionKey.Equals(1) ? "Soccer025" : "", "");
                    else if (text.Equals(await GetEventName("Soccer022"))) //Board10
                        SetButtonBoard("Soccer024", "Soccer021", "", "");
                    else if (text.Equals(await GetEventName("Soccer023"))) //Board07
                        SetButtonBoard("Soccer010", "Soccer012", "Soccer011", "");
                    else if (text.Equals(await GetEventName("Soccer024"))) //Board01
                        SetButtonBoard("Soccer001", "Soccer002", dbAthlete.PositionKey.Equals(1) ? "Soccer025" : "", "");
                    else if (text.Equals(await GetEventName("Soccer025"))) //Board13
                        SetButtonBoard("Soccer030", "Soccer029", "", "");
                    else if (text.Equals(await GetEventName("Soccer026"))) //Board02
                        SetButtonBoard("Soccer004", "Soccer002", "Soccer003", "Soccer005");
                    else if (text.Equals(await GetEventName("Soccer027"))) //Board12
                        SetButtonBoard("Soccer027", "Soccer002", "Soccer028", "");
                    else if (text.Equals(await GetEventName("Soccer028"))) //Board01
                        SetButtonBoard("Soccer001", "Soccer002", dbAthlete.PositionKey.Equals(1) ? "Soccer025" : "", "");
                    else if (text.Equals(await GetEventName("Soccer029"))) //Board02
                        SetButtonBoard("Soccer004", "Soccer002", "Soccer003", "Soccer005");
                    else if (text.Equals(await GetEventName("Soccer030"))) //Board12
                        SetButtonBoard("Soccer027", "Soccer002", "Soccer028", "");
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
                        await DisplayAlert(Message.TITLE_STATUS_ERROR, ex.Message, Message.OPTION_OK);
                    }
                }
                else
                    await DisplayAlert(Message.TITLE_STATUS_ERROR, Message.MSG_TIMER, Message.OPTION_OK);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private async void LeaveItem_OnClicked(object sender, EventArgs e)
        {
            if (await App.DisplayMessage(Message.TITLE_STATUS_WARNING, Message.MSG_LEAVE, Message.OPTION_NO, Message.OPTION_YES))
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

        private async Task<string> GetEventName(string key)
        {
            if (string.IsNullOrEmpty(key))
                return string.Empty;

            var dbEvents = await new Query().GetEvents(dbGame.DataExt);
            return dbEvents.Find(x => x.Key.Equals(key)).Name;
        }

        private async void SetButtonBoard(string event1, string event2, string event3, string event4)
        {
            Event1Button.Text = await GetEventName(event1);
            Event2Button.Text = await GetEventName(event2);
            Event3Button.Text = await GetEventName(event3);
            Event4Button.Text = await GetEventName(event4);
        }

        private async Task BackgroundAsync()
        {
            while (true)
            {
                LbStopwatch.Text = StopWatch.ShowTime();
                await Task.Delay(1000);
            }
        }
    }
}
