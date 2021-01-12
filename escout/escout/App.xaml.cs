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

        private const string USERNAME_KEY = "Username";
        private const string PASSWORD_KEY = "Password";
        private const string AUTH_SAVE_KEY = "false";
        private const string VERSION_KEY = "version";
        private const string DEFAULT_SERVER_KEY = "";

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
            get => Properties.ContainsKey(USERNAME_KEY) ? Properties[USERNAME_KEY].ToString() : "";
            set => Properties[USERNAME_KEY] = value;
        }

        public string Password
        {
            get => Properties.ContainsKey(PASSWORD_KEY) ? Properties[PASSWORD_KEY].ToString() : "";
            set => Properties[PASSWORD_KEY] = value;
        }

        public string BasicAuth
        {
            get => Properties.ContainsKey(AUTH_SAVE_KEY) ? Properties[AUTH_SAVE_KEY].ToString() : "false";
            set => Properties[AUTH_SAVE_KEY] = value;
        }

        public string Version
        {
            get => Properties.ContainsKey(VERSION_KEY) ? Properties[VERSION_KEY].ToString() : "Version 1.0.4";
            set => Properties[VERSION_KEY] = value;
        }

        public string DefaultServer
        {
            get => Properties.ContainsKey(DEFAULT_SERVER_KEY) && !string.IsNullOrEmpty(Properties[DEFAULT_SERVER_KEY].ToString()) ? Properties[DEFAULT_SERVER_KEY].ToString() : "https://escout-server.herokuapp.com";
            set => Properties[DEFAULT_SERVER_KEY] = value;
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
