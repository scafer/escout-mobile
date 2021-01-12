using escout.Models.Rest;
using escout.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace escout.Views.Authentication
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignInPage : ContentPage
    {
        private User user = new User();

        public SignInPage()
        {
            InitializeComponent();
            BindingContext = new AuthenticationViewModel(Navigation, user);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            var app = Application.Current as App;
            Username.Text = app.Username;
            Password.Text = app.Password;
            SwSave.IsToggled = bool.Parse(app.BasicAuth);

            Save_Changed();
        }

        private void Save_Toggled(object sender, ToggledEventArgs e)
        {
            if (string.IsNullOrEmpty(Username.Text) || string.IsNullOrEmpty(Password.Text))
                (sender as Switch).IsToggled = false;
            else
                Save_Changed();
        }

        private void Save_Changed()
        {
            var app = Application.Current as App;

            if (SwSave.IsToggled)
            {
                app.Username = Username.Text;
                app.Password = Password.Text;
                app.BasicAuth = (SwSave.IsToggled).ToString();
            }
            else
            {
                Username.Text = string.Empty;
                Password.Text = string.Empty;
                app.Username = string.Empty;
                app.Password = string.Empty;
                app.BasicAuth = (SwSave.IsToggled).ToString();
            }
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            var app = Application.Current as App;

            try
            {
                var result = await DisplayPromptAsync("Settings", "Change server address. Leave empty to use the default server.", keyboard: Keyboard.Default);
                if (result != null)
                    app.DefaultServer = result;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}