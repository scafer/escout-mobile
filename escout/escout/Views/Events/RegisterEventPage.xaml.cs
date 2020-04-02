
using escout.Helpers;
using escout.Models.Db;
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
            SetBoard1();
        }

        private async Task BackgroundAsync()
        {
            while (true)
            {
                LbStopwatch.Text = StopWatch.ShowTime();
                await Task.Delay(1000);
            }
        }

        private async void LeaveItem_OnClicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new NavigationPage(new MainPage());
            await Navigation.PushAsync(new MainPage());
        }


        private void Pause_Clicked(object sender, EventArgs e)
        {
            StopWatch.Stop();
        }

        private void Resume_Clicked(object sender, EventArgs e)
        {
            StopWatch.Start();
        }

        private void EventExecuted(object sender, EventArgs e)
        {
            var button = sender as Button;
        }

        private void SetBoard1()
        {
            Event1Button.Text = "Recuperação de bola";
            Event2Button.Text = "Interrupção";
            Event3Button.Text = "";
            Event4Button.Text = "";
        }

        private void SetBoard2()
        {
            Event1Button.Text = "Remate";
            Event2Button.Text = "Interrupção";
            Event3Button.Text = "Perda de bola";
            Event4Button.Text = "Passe";
        }

        private void SetBoard3()
        {
            Event1Button.Text = "Passe Falhado";
            Event2Button.Text = "Passe Concretizado";
            Event3Button.Text = "";
            Event4Button.Text = "";
        }

        private void SetBoard4()
        {
            Event1Button.Text = "Intercetado";
            Event2Button.Text = "Bola parada";
            Event3Button.Text = "Para fora";
            Event4Button.Text = "À baliza";
        }
        private void SetBoard5()
        {
            Event1Button.Text = "Falta Cometida";
            Event2Button.Text = "Falta Sofrida";
            Event3Button.Text = "Lançamento a favor";
            Event4Button.Text = "";
        }

        private void SetBoard6()
        {
            Event1Button.Text = "Falta Cometida";
            Event2Button.Text = "Falta Sofrida";
            Event3Button.Text = "Lançamento a favor";
            Event4Button.Text = "";
        }
    }
}