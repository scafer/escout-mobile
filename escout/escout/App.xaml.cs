using escout.Models.Db;
using escout.Views.Authentication;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace escout
{
    public partial class App : Application
    {
        public static string UserId;
        public static DbGame DbGame;
        public static DbAthlete DbAthlete;

        private const string UsernameKey = "Username";
        private const string PasswordKey = "Password";
        private const string AuthSaveKey = "false";
        private const string VersionKey = "";

        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new SignInPage());
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
            get => Properties.ContainsKey(VersionKey) ? Properties[VersionKey].ToString() : "Version 1.0.2";
            set => Properties[VersionKey] = value;
        }

        public static async Task DisplayMessage(string title, string message, string cancel)
        {
            await App.Current.MainPage.DisplayAlert(title, message, cancel);
        }

        public static async Task<bool> DisplayMessage(string title, string message, string cancel, string accept)
        {
            return await App.Current.MainPage.DisplayAlert(title, message, accept, cancel);
        }
    }
}
