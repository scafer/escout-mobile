using escout.Helpers;
using escout.Models.Db;
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

        private int type = 0;

        private DbGame dbGame;

        public RegisterEventPage()
        {
            InitializeComponent();
            this.dbGame = Utils.DbGame;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            BindingContext = Utils.DbGame;
            _ = BackgroundAsync();
            SetButtonBoard("Soccer001", "Soccer002", type.Equals(1) ? "Soccer025" : "", ""); //Board01
        }

        private async void LeaveItem_OnClicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new NavigationPage(new MainPage());
            await Navigation.PushAsync(new MainPage());
        }

        private void EventExecuted(object sender, EventArgs e)
        {
            var events = Utils.SoccerEventList;
            var button = sender as Button;

            switch (button.Text)
            {
                case Soccer001: SetButtonBoard("Soccer004", "Soccer002", "Soccer003", "Soccer005"); break;  //Board02
                case Soccer002: SetButtonBoard("Soccer015", "Soccer016", "Soccer014", type.Equals(1) ? "Soccer026" : ""); break;  //Board05
                case Soccer003: SetButtonBoard("Soccer001", "Soccer002", type.Equals(1) ? "Soccer025" : "", ""); break;  //Board01
                case Soccer004: SetButtonBoard("Soccer011", "Soccer013", "Soccer010", "Soccer012"); break;  //Board04
                case Soccer005: SetButtonBoard("Soccer006", "Soccer007", "", ""); break;  //Board03
                case Soccer006: SetButtonBoard("Soccer001", "Soccer002", type.Equals(1) ? "Soccer025" : "", ""); break;  //Board01
                case Soccer007: SetButtonBoard("Soccer009", "Soccer008", "", ""); break;  //Board11
                case Soccer008: SetButtonBoard("Soccer001", "Soccer002", type.Equals(1) ? "Soccer025" : "", ""); break;  //Board01
                case Soccer009: SetButtonBoard("Soccer001", "Soccer002", type.Equals(1) ? "Soccer025" : "", ""); break;  //Board01
                case Soccer010: SetButtonBoard("Soccer001", "Soccer002", type.Equals(1) ? "Soccer025" : "", ""); break;  //Board01
                case Soccer011: SetButtonBoard("Soccer001", "Soccer002", type.Equals(1) ? "Soccer025" : "", ""); break;  //Board01
                case Soccer012: SetButtonBoard("Soccer019", "Soccer020", "Soccer021", ""); break;  //Board07
                case Soccer013: SetButtonBoard("Soccer022", "Soccer023", "", ""); break;  //Board08
                case Soccer014: SetButtonBoard("Soccer004", "Soccer002", "Soccer003", "Soccer005"); break;  //Board02
                case Soccer015: SetButtonBoard("Soccer018", "Soccer017", "", ""); break;  //Board06
                case Soccer016: SetButtonBoard("Soccer004", "Soccer002", "Soccer003", "Soccer005"); break;  //Board02
                case Soccer017: SetButtonBoard("Soccer001", "Soccer002", type.Equals(1) ? "Soccer025" : "", ""); break;  //Board01
                case Soccer018: SetButtonBoard("Soccer001", "Soccer002", type.Equals(1) ? "Soccer025" : "", ""); break;  //Board01
                case Soccer019: SetButtonBoard("Soccer001", "Soccer002", type.Equals(1) ? "Soccer025" : "", ""); break;  //Board01
                case Soccer020: SetButtonBoard("Soccer001", "Soccer002", type.Equals(1) ? "Soccer025" : "", ""); break;  //Board01
                case Soccer021: SetButtonBoard("Soccer001", "Soccer002", type.Equals(1) ? "Soccer025" : "", ""); break;  //Board01
                case Soccer022: SetButtonBoard("Soccer024", "Soccer021", "", ""); break;  //Board10
                case Soccer023: SetButtonBoard("Soccer010", "Soccer012", "Soccer011", ""); break;  //Board07
                case Soccer024: SetButtonBoard("Soccer001", "Soccer002", type.Equals(1) ? "Soccer025" : "", ""); break;  //Board01
                case Soccer025: SetButtonBoard("Soccer030", "Soccer029", "", ""); break;  //Board13
                case Soccer027: SetButtonBoard("Soccer004", "Soccer002", "Soccer003", "Soccer005"); break;  //Board02
                case Soccer026: SetButtonBoard("Soccer027", "Soccer002", "Soccer028", ""); break;  //Board12
                case Soccer028: SetButtonBoard("Soccer001", "Soccer002", type.Equals(1) ? "Soccer025" : "", ""); break;  //Board01
                case Soccer029: SetButtonBoard("Soccer004", "Soccer002", "Soccer003", "Soccer005"); break;  //Board02
                case Soccer030: SetButtonBoard("Soccer027", "Soccer002", "Soccer028", ""); break;  //Board12
            }
        }

        private void SetButtonBoard(string event1, string event2, string event3, string event4)
        {
            Event1Button.Text = Utils.GetSoccerEvent(event1).Name;
            Event2Button.Text = Utils.GetSoccerEvent(event2).Name;
            Event3Button.Text = Utils.GetSoccerEvent(event3).Name;
            Event4Button.Text = Utils.GetSoccerEvent(event4).Name;
        }

        private void Pause_Clicked(object sender, EventArgs e)
        {
            //StopWatch.Stop();
            type = 0;
            SetButtonBoard("Soccer001", "Soccer002", type.Equals(1) ? "Soccer025" : "", ""); //Board01
        }

        private void Resume_Clicked(object sender, EventArgs e)
        {
            //StopWatch.Start();
            type = 1;
            SetButtonBoard("Soccer001", "Soccer002", type.Equals(1) ? "Soccer025" : "", ""); //Board01
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