using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace escout.Views.Authentication
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SplashScreenPage : ContentPage
    {
        public SplashScreenPage() => InitializeComponent();

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            Version.Text = (Application.Current as App).Version;

            await LogoImage.ScaleTo(0.9, 1500, Easing.Linear);
            await LogoImage.ScaleTo(1, 1000, Easing.Linear);

            Application.Current.MainPage = new NavigationPage(new MainPage());
            await Navigation.PushAsync(new MainPage());
        }
    }
}