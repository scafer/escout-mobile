using System;
using System.ComponentModel;
using System.Net;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Input;
using escout.Helpers;
using escout.Models.Rest;
using escout.Services.Rest;
using escout.Views.Authentication;
using escout.Views.GameData;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace escout.ViewModels
{
    class AuthenticationViewModel : INotifyPropertyChanged
    {
        public INavigation Navigation;
        public ICommand SignInCommand { get; set; }
        public ICommand SignUpCommand { get; set; }
        public ICommand ForgotPasswordCommand { get; set; }
        public ICommand SignUpViewCommand { get; set; }
        public ICommand ForgotPasswordViewCommand { get; set; }
        public ICommand AboutViewCommand { get; set; }
        public ICommand SettingsViewCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        private User user;
        private bool isBusy;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public AuthenticationViewModel(INavigation navigation, User user)
        {
            this.user = user;
            Navigation = navigation;

            SignInCommand = new Command(SignInExecuted);
            SignUpCommand = new Command(SignUpExecuted);
            ForgotPasswordCommand = new Command(ForgotPasswordExecuted);
            SignUpViewCommand = new Command(SignUpViewExecuted);
            ForgotPasswordViewCommand = new Command(ForgetPasswordViewExecuted);
            AboutViewCommand = new Command(AboutViewExecuted);
            SettingsViewCommand = new Command(SettingsViewExecuted);
            CancelCommand = new Command(CancelExecuted);
        }

        public int UserId
        {
            get => user.Id;
            set => user.Id = value;
        }

        public string Username
        {
            get => user.Username;
            set => user.Username = value;
        }

        public string Password
        {
            get => user.Password;
            set => user.Password = value;
        }

        public string Email
        {
            get => user.Email;
            set => user.Email = value;
        }

        public bool IsBusy
        {
            get => isBusy;
            set
            {
                isBusy = value;
                OnPropertyChanged();
            }
        }

        public bool IsVisible
        {
            get => !IsBusy;
            set
            {
                IsBusy = value;
                OnPropertyChanged();
            }
        }

        public static string GenerateSha256String(string inputString)
        {
            var sb = new StringBuilder();

            using var hash = SHA256.Create();

            var enc = Encoding.UTF8;
            var result = hash.ComputeHash(enc.GetBytes(inputString));

            foreach (var b in result)
                sb.Append(b.ToString("x2"));

            return sb.ToString();
        }

        private async void SignInExecuted()
        {
            if (!(string.IsNullOrEmpty(Username)) && !(string.IsNullOrEmpty(Password)))
            {
                try
                {
                    IsVisible = true;
                    var response = await RestConnector.PostObjectAsync(RestConnector.SIGN_IN,
                        new User(Username, GenerateSha256String(Password), Email));

                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        _ = App.DisplayMessage(ConstValues.TITLE_STATUS_ERROR, await response.Content.ReadAsStringAsync(), ConstValues.OPTION_OK);
                    }
                    else
                    {
                        var auth = JsonConvert.DeserializeObject<AuthData>(await response.Content.ReadAsStringAsync());
                        RestConnector.Token = auth.Token;
                        App.UserId = auth.Id;
                        await Navigation.PushAsync(new SplashScreenPage());
                    }
                }
                catch
                {
                    if (await App.DisplayMessage(ConstValues.TITLE_STATUS_INFO, ConstValues.MSG_OFFLINE, ConstValues.OPTION_NO, ConstValues.OPTION_YES))
                    {
                        Application.Current.MainPage = new NavigationPage(new WatchingListPage());
                        await Navigation.PushAsync(new WatchingListPage());
                    }
                }
                finally
                {
                    IsVisible = false;
                }
            }
            else
            {
                _ = App.DisplayMessage(ConstValues.TITLE_STATUS_ERROR, ConstValues.MSG_AUTHENTICATION_INVALID, ConstValues.OPTION_OK);
            }
        }

        private async void SignUpExecuted()
        {
            if (!(string.IsNullOrEmpty(Username) && string.IsNullOrEmpty(Email) && string.IsNullOrEmpty(Password)))
            {
                try
                {
                    IsVisible = true;
                    var response = await RestConnector.PostObjectAsync(RestConnector.SIGN_UP, new User(Username, GenerateSha256String(Password), Email));

                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        _ = App.DisplayMessage(ConstValues.TITLE_STATUS_INFO, ConstValues.MSG_SUCCESS, ConstValues.OPTION_OK);
                        await Navigation.PopModalAsync();
                    }
                    else
                    {
                        _ = App.DisplayMessage(ConstValues.TITLE_STATUS_ERROR, await response.Content.ReadAsStringAsync(), ConstValues.OPTION_OK);
                    }
                }
                catch (Exception ex)
                {
                    ExceptionHandler.GenericException(ex);
                }
                finally
                {
                    IsVisible = false;
                }
            }
            else
            {
                _ = App.DisplayMessage(ConstValues.TITLE_STATUS_ERROR, ConstValues.MSG_GENERIC_ERROR, ConstValues.OPTION_OK);
            }
        }

        private async void ForgotPasswordExecuted()
        {
            if (!(string.IsNullOrEmpty(Username)) || !(string.IsNullOrEmpty(Email)))
            {
                try
                {
                    IsVisible = true;
                    var response = await RestConnector.PostObjectAsync(RestConnector.RESET_PASSWORD, new User(Username, null, Email));

                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        _ = App.DisplayMessage(ConstValues.TITLE_STATUS_INFO, ConstValues.MSG_SUCCESS, ConstValues.OPTION_OK);
                        await Navigation.PopModalAsync();
                    }
                    else
                    {
                        _ = App.DisplayMessage(ConstValues.TITLE_STATUS_ERROR, await response.Content.ReadAsStringAsync(), ConstValues.OPTION_OK);
                    }
                }
                catch (Exception ex)
                {
                    ExceptionHandler.GenericException(ex);
                }
                finally
                {
                    IsVisible = false;
                }
            }
            else
            {
                _ = App.DisplayMessage(ConstValues.MSG_GENERIC_ERROR, ConstValues.MSG_AUTHENTICATION_INVALID_2, ConstValues.OPTION_OK);
            }
        }

        private async void SettingsViewExecuted()
        {
            var app = Application.Current as App;

            try
            {
                var result = await App.DisplayPrompt(ConstValues.TITLE_STATUS_SETTINGS, ConstValues.MSG_CHANGE_SERVER);

                if (result != null)
                {
                    app.DefaultServer = result;
                }
            }
            catch (Exception) { }
        }

        private async void SignUpViewExecuted()
        {
            await Navigation.PushModalAsync(new SignUpPage());
        }

        private async void ForgetPasswordViewExecuted()
        {
            await Navigation.PushModalAsync(new ForgotPasswordPage());
        }

        private async void AboutViewExecuted()
        {
            await Navigation.PushModalAsync(new AboutPage());
        }

        private async void CancelExecuted()
        {
            await Navigation.PopModalAsync();
        }
    }
}
