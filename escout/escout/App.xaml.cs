using escout.Views;
using escout.Views.Authentication;
using Xamarin.Forms;

namespace escout
{
    public partial class App : Application
    {
        private const string UsernameKey = "Username";
        private const string PasswordKey = "Password";
        private const string AuthSaveKey = "false";
        private const string VersionKey = "0.5";

        public App()
        {
            InitializeComponent();

             //MainPage = new NavigationPage(new SignInPage());
            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        public string Username
        {
            get => Properties.ContainsKey(UsernameKey) ? Properties[UsernameKey].ToString() : "";
            set => Properties[UsernameKey] = value;
        }

        public string Password
        {
            get => Properties.ContainsKey(PasswordKey) ? Properties[PasswordKey].ToString() : "";
            set => Properties[PasswordKey] = value;
        }

        public string BasicAuth
        {
            get => Properties.ContainsKey(AuthSaveKey) ? Properties[AuthSaveKey].ToString() : "false";
            set => Properties[AuthSaveKey] = value;
        }

        public string Version
        {
            get => Properties.ContainsKey(VersionKey) ? Properties[VersionKey].ToString() : "";
            set => Properties[VersionKey] = value;
        }

        public static async void DisplayMessage(string title, string message, string cancel)
        {
            await App.Current.MainPage.DisplayAlert(title, message, cancel);
        }

        public static async void DisplayMessage(string title, string message, string cancel, string accept)
        {
            await App.Current.MainPage.DisplayAlert(title, message, accept, cancel);
        }
    }
}
