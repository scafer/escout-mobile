using escout.Helpers;
using escout.Models;
using escout.Models.Db;
using escout.ViewModels;
using System;
using System.Collections.Generic;
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

        public RegisterEventPage() => InitializeComponent();

        protected override void OnAppearing()
        {
            base.OnAppearing();

            BindingContext = App.DbGame;
            dbGame = App.DbGame;
            dbAthlete = App.DbAthlete;

            _ = BackgroundAsync();
            btn_pause.IsVisible = false;
        }

        private void Pause_Clicked(object sender, EventArgs e)
        {
            StopWatch.Stop();
            btn_resume.IsVisible = true;
            btn_pause.IsVisible = false;
        }

        private void Resume_Clicked(object sender, EventArgs e)
        {
            if (Event1Button.Text == "" && Event2Button.Text == "" && Event3Button.Text == "" && Event4Button.Text == "")
                SetButtonBoard("Soccer001", "Soccer002", dbAthlete.PositionKey.Equals(1) ? "Soccer025" : "", ""); //Board01

            StopWatch.Start();
            btn_resume.IsVisible = false;
            btn_pause.IsVisible = true;
        }

        private async void LeaveItem_OnClicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new NavigationPage(new MainPage());
            await Navigation.PushAsync(new MainPage());
        }

        private async void EventExecuted(object sender, EventArgs e)
        {
            if (StopWatch.ShowTime() == "00:00")
            {
                if (await DisplayAlert(Message.TITLE_STATUS_WARNING, Message.GAME_START, Message.OPTION_YES, Message.OPTION_NO))
                    StopWatch.Start();
            }
            else
            {
                var button = sender as Button;
                PreviousEvent.Text = "Previous Event: " + button.Text;

                if (!string.IsNullOrEmpty(button.Text))
                {
                    var evt = new DbGameEvent()
                    {
                        Key = DateTime.UtcNow.Ticks.ToString(),
                        Time = DateTime.UtcNow.ToString(),
                        GameTime = LbStopwatch.Text,
                        GameId = dbGame.Id,
                        EventId = await new LocalDb().GetEventId(button.Text),
                        AthleteId = dbAthlete.Id,
                        ClubId = dbAthlete.ClubId,
                        DataExt = dbGame.DataExt
                    };

                    _ = GameEventViewModel.RegisterEvent(evt);

                    switch (button.Text)
                    {
                        case Soccer001: SetButtonBoard("Soccer004", "Soccer002", "Soccer003", "Soccer005"); break;  //Board02
                        case Soccer002: SetButtonBoard("Soccer015", "Soccer016", "Soccer014", dbAthlete.PositionKey.Equals(1) ? "Soccer026" : ""); break;  //Board05
                        case Soccer003: SetButtonBoard("Soccer001", "Soccer002", dbAthlete.PositionKey.Equals(1) ? "Soccer025" : "", ""); break;  //Board01
                        case Soccer004: SetButtonBoard("Soccer011", "Soccer013", "Soccer010", "Soccer012"); break;  //Board04
                        case Soccer005: SetButtonBoard("Soccer006", "Soccer007", "", ""); break;  //Board03
                        case Soccer006: SetButtonBoard("Soccer001", "Soccer002", dbAthlete.PositionKey.Equals(1) ? "Soccer025" : "", ""); break;  //Board01
                        case Soccer007: SetButtonBoard("Soccer009", "Soccer008", "", ""); break;  //Board11
                        case Soccer008: SetButtonBoard("Soccer001", "Soccer002", dbAthlete.PositionKey.Equals(1) ? "Soccer025" : "", ""); break;  //Board01
                        case Soccer009: SetButtonBoard("Soccer001", "Soccer002", dbAthlete.PositionKey.Equals(1) ? "Soccer025" : "", ""); break;  //Board01
                        case Soccer010: SetButtonBoard("Soccer001", "Soccer002", dbAthlete.PositionKey.Equals(1) ? "Soccer025" : "", ""); break;  //Board01
                        case Soccer011: SetButtonBoard("Soccer001", "Soccer002", dbAthlete.PositionKey.Equals(1) ? "Soccer025" : "", ""); break;  //Board01
                        case Soccer012: SetButtonBoard("Soccer019", "Soccer020", "Soccer021", ""); break;  //Board07
                        case Soccer013: SetButtonBoard("Soccer022", "Soccer023", "", ""); break;  //Board08
                        case Soccer014: SetButtonBoard("Soccer004", "Soccer002", "Soccer003", "Soccer005"); break;  //Board02
                        case Soccer015: SetButtonBoard("Soccer018", "Soccer017", "", ""); break;  //Board06
                        case Soccer016: SetButtonBoard("Soccer004", "Soccer002", "Soccer003", "Soccer005"); break;  //Board02
                        case Soccer017: SetButtonBoard("Soccer001", "Soccer002", dbAthlete.PositionKey.Equals(1) ? "Soccer025" : "", ""); break;  //Board01
                        case Soccer018: SetButtonBoard("Soccer001", "Soccer002", dbAthlete.PositionKey.Equals(1) ? "Soccer025" : "", ""); break;  //Board01
                        case Soccer019: SetButtonBoard("Soccer001", "Soccer002", dbAthlete.PositionKey.Equals(1) ? "Soccer025" : "", ""); break;  //Board01
                        case Soccer020: SetButtonBoard("Soccer001", "Soccer002", dbAthlete.PositionKey.Equals(1) ? "Soccer025" : "", ""); break;  //Board01
                        case Soccer021: SetButtonBoard("Soccer001", "Soccer002", dbAthlete.PositionKey.Equals(1) ? "Soccer025" : "", ""); break;  //Board01
                        case Soccer022: SetButtonBoard("Soccer024", "Soccer021", "", ""); break;  //Board10
                        case Soccer023: SetButtonBoard("Soccer010", "Soccer012", "Soccer011", ""); break;  //Board07
                        case Soccer024: SetButtonBoard("Soccer001", "Soccer002", dbAthlete.PositionKey.Equals(1) ? "Soccer025" : "", ""); break;  //Board01
                        case Soccer025: SetButtonBoard("Soccer030", "Soccer029", "", ""); break;  //Board13
                        case Soccer027: SetButtonBoard("Soccer004", "Soccer002", "Soccer003", "Soccer005"); break;  //Board02
                        case Soccer026: SetButtonBoard("Soccer027", "Soccer002", "Soccer028", ""); break;  //Board12
                        case Soccer028: SetButtonBoard("Soccer001", "Soccer002", dbAthlete.PositionKey.Equals(1) ? "Soccer025" : "", ""); break;  //Board01
                        case Soccer029: SetButtonBoard("Soccer004", "Soccer002", "Soccer003", "Soccer005"); break;  //Board02
                        case Soccer030: SetButtonBoard("Soccer027", "Soccer002", "Soccer028", ""); break;  //Board12
                    }
                }
            }
        }

        private void SetButtonBoard(string event1, string event2, string event3, string event4)
        {
            Event1Button.Text = SoccerEvent[event1].Name;
            Event2Button.Text = SoccerEvent[event2].Name;
            Event3Button.Text = SoccerEvent[event3].Name;
            Event4Button.Text = SoccerEvent[event4].Name;
            Event1Button.ImageSource = SoccerEvent[event1].ImageUrl;
            Event2Button.ImageSource = SoccerEvent[event2].ImageUrl;
            Event3Button.ImageSource = SoccerEvent[event3].ImageUrl;
            Event4Button.ImageSource = SoccerEvent[event4].ImageUrl;
        }

        private async Task BackgroundAsync()
        {
            while (true)
            {
                LbStopwatch.Text = StopWatch.ShowTime();
                await Task.Delay(1000);
            }
        }

        public static readonly Dictionary<string, Option> SoccerEvent = new Dictionary<string, Option>
        {
            {string.Empty, new Option(string.Empty, string.Empty) },
            {"Soccer001", new Option("Ball Recovery", "") },
            {"Soccer002", new Option("Interruption", "") },
            {"Soccer003", new Option("Ball Loss", "") },
            {"Soccer004", new Option("Shot", "") },
            {"Soccer005", new Option("Pass", "")},
            {"Soccer006", new Option("Missed Pass", "") },
            {"Soccer007", new Option("Pass Success", "") },
            {"Soccer008", new Option("Assistance - Yes", "") },
            {"Soccer009", new Option("Assistance - No", "") },
            {"Soccer010", new Option("Out", "") },
            {"Soccer011", new Option("Intercepted", "") },
            {"Soccer012", new Option("On target", "") },
            {"Soccer013", new Option("Ball Stop", "") },
            {"Soccer014", new Option("Kick in Favor", "") },
            {"Soccer015", new Option("Foul Committed", "") },
            {"Soccer016", new Option("Missed Foul", "") },
            {"Soccer017", new Option("Red Card", "") },
            {"Soccer018", new Option("Yellow Card", "") },
            {"Soccer019", new Option("To Post", "") },
            {"Soccer020", new Option("Goalkeeper Defended", "") },
            {"Soccer021", new Option("Goal", "") },
            {"Soccer022", new Option("Penalty", "") },
            {"Soccer023", new Option("Free Kick", "") },
            {"Soccer024", new Option("Missed", "") },
            {"Soccer025", new Option("Defense", "") },
            {"Soccer026", new Option("Goal Kick", "") },
            {"Soccer027", new Option("Ball Possession", "") },
            {"Soccer028", new Option("Opposing Team Ball Possession", "") },
            {"Soccer029", new Option("Grabbed the Ball", "") },
            {"Soccer030", new Option("Didn't Grab the Ball", "") },
        };

        const string Soccer001 = "Ball Recovery";
        const string Soccer002 = "Interruption";
        const string Soccer003 = "Ball Loss";
        const string Soccer004 = "Shot";
        const string Soccer005 = "Pass";
        const string Soccer006 = "Missed Pass";
        const string Soccer007 = "Pass Success";
        const string Soccer008 = "Assistance - Yes";
        const string Soccer009 = "Assistance - No";
        const string Soccer010 = "Out";
        const string Soccer011 = "Intercepted";
        const string Soccer012 = "On target";
        const string Soccer013 = "Ball Stop";
        const string Soccer014 = "Kick in Favor";
        const string Soccer015 = "Foul Committed";
        const string Soccer016 = "Missed Foul";
        const string Soccer017 = "Red Card";
        const string Soccer018 = "Yellow Card";
        const string Soccer019 = "To Post";
        const string Soccer020 = "Goalkeeper Defended";
        const string Soccer021 = "Goal";
        const string Soccer022 = "Penalty";
        const string Soccer023 = "Free Kick";
        const string Soccer024 = "Missed";
        const string Soccer025 = "Defense";
        const string Soccer026 = "Goal Kick";
        const string Soccer027 = "Ball Possession";
        const string Soccer028 = "Opposing Team Ball Possession";
        const string Soccer029 = "Grabbed the Ball";
        const string Soccer030 = "Didn't Grab the Ball";

        private void Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new EventsTimeLinePage());
        }
    }
}