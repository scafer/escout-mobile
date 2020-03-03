using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace escout.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SplashScreenPage : ContentPage
    {
        public SplashScreenPage()
        {
            InitializeComponent();
            Version.Text = (Application.Current as App).Version;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            await LogoImage.ScaleTo(0.9, 1500, Easing.Linear);
            await LogoImage.ScaleTo(1, 1000, Easing.Linear);

            Application.Current.MainPage = new NavigationPage(new MainPage());
            await Navigation.PushAsync(new MainPage());
        }
    }
}