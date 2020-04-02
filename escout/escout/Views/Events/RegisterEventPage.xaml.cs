
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
    }
}