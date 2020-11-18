using escout.Helpers;
using escout.Models.Rest;
using escout.Views.Authentication;
using escout.Views.GameData;
using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Input;
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

        private async void SignInExecuted()
        {
            if (!(string.IsNullOrEmpty(Username)) && !(string.IsNullOrEmpty(Password)))
            {
                try
                {
                    IsVisible = true;
                    var response = await RestConnector.PostObjectAsync(RestConnector.SIGN_IN,
                        new User(Username, GenerateSha256String(Password), Email));
                    if (!string.IsNullOrEmpty(response))
                    {
                        var result = JsonConvert.DeserializeObject<AuthData>(response);

                        if (!string.IsNullOrEmpty(result.Token))
                        {
                            RestConnector.Token = result.Token;
                            App.UserId = result.Id;
                            await Navigation.PushAsync(new SplashScreenPage());
                        }
                        else
                            _ = App.DisplayMessage(Message.TITLE_STATUS_ERROR, Message.AUTHENTICATION_INVALID, Message.OPTION_OK);
                    }
                    else
                    {
                        _ = App.DisplayMessage(Message.TITLE_STATUS_ERROR, Message.GENERIC_ERROR, Message.OPTION_OK);
                    }
                }
                catch
                {
                    var option = await App.DisplayMessage(Message.TITLE_STATUS_INFO, Message.OFFLINE, Message.OPTION_NO, Message.OPTION_YES);
                    if (option)
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
                _ = App.DisplayMessage(Message.TITLE_STATUS_ERROR, Message.AUTHENTICATION_INVALID, Message.OPTION_OK);
        }

        private async void SignUpExecuted()
        {
            if (!(string.IsNullOrEmpty(Username) && string.IsNullOrEmpty(Email) && string.IsNullOrEmpty(Password)))
            {
                try
                {
                    IsVisible = true;

                    var response = await RestConnector.PostObjectAsync(RestConnector.SIGN_UP,
                        new User(Username, GenerateSha256String(Password), Email));
                    if (!string.IsNullOrEmpty(response))
                    {
                        var result = JsonConvert.DeserializeObject<SvcResult>(response);
                        _ = App.DisplayMessage(Message.TITLE_STATUS_ERROR, result.ErrorMessage, Message.OPTION_OK);

                        if (result.ErrorCode == 0)
                            await Navigation.PopModalAsync();
                    }
                    else
                    {
                        _ = App.DisplayMessage(Message.TITLE_STATUS_ERROR, Message.GENERIC_ERROR, Message.OPTION_OK);
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
                _ = App.DisplayMessage(Message.TITLE_STATUS_ERROR, Message.GENERIC_ERROR, Message.OPTION_OK);
        }

        private async void ForgotPasswordExecuted()
        {
            if (!(string.IsNullOrEmpty(Username)) || !(string.IsNullOrEmpty(Email)))
            {
                try
                {
                    IsVisible = true;

                    var response = await RestConnector.PostObjectAsync(RestConnector.RESET_PASSWORD, new User(Username, null, Email));
                    if (!string.IsNullOrEmpty(response))
                    {
                        var result = JsonConvert.DeserializeObject<SvcResult>(response);
                        _ = App.DisplayMessage(Message.TITLE_STATUS_ERROR, result.ErrorMessage, Message.OPTION_OK);
                        await Navigation.PopModalAsync();
                    }
                    else
                    {
                        _ = App.DisplayMessage(Message.TITLE_STATUS_ERROR, Message.GENERIC_ERROR, Message.OPTION_OK);
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
                _ = App.DisplayMessage(Message.GENERIC_ERROR, Message.AUTHENTICATION_INVALID_2, Message.OPTION_OK);
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
    }
}
