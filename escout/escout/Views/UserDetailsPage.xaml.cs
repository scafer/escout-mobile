using escout.Helpers;
using escout.Models.Rest;
using escout.ViewModels;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace escout.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserDetailsPage : ContentPage
    {
        private User user;

        public UserDetailsPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _ = GetData();
        }

        private async void SaveChanges_Clicked(object sender, EventArgs e)
        {
            if(await App.DisplayMessage(Message.TITLE_STATUS_WARNING, Message.UPDATE_USER_QUESTION, Message.OPTION_NO, Message.OPTION_YES))
            {
                var response = await RestConnector.PutObjectAsync(RestConnector.User, user);
                if (!string.IsNullOrEmpty(response))
                    await App.DisplayMessage(Message.TITLE_STATUS_INFO, Message.USER_UPDATED, Message.OPTION_OK);
            }
        }

        private async void UpdatePassword_Clicked(object sender, EventArgs e)
        {
            if (await App.DisplayMessage(Message.TITLE_STATUS_WARNING, Message.UPDATE_USER_QUESTION, Message.OPTION_NO, Message.OPTION_YES))
            {
                var request = RestConnector.ChangePassword + "?newPassword=" + AuthenticationViewModel.GenerateSha256String(Password.Text);
                var response = await RestConnector.PostObjectAsync(request, null);
                if (!string.IsNullOrEmpty(response))
                    await App.DisplayMessage(Message.TITLE_STATUS_INFO, Message.USER_UPDATED, Message.OPTION_OK);
            }
        }

        private void UserCell_Tapped(object sender, EventArgs e)
        {
            var swt = sender as SwitchCell;
            if (swt.On == true)
            {
                PasswordCell.On = false;
                PasswordButton.IsVisible = false;
                UserButton.IsVisible = true;
            }
            else
            {
                UserButton.IsVisible = false;
            }
        }

        private void PasswordCell_Tapped(object sender, EventArgs e)
        {
            var swt = sender as SwitchCell;
            if (swt.On == true)
            {
                UserCell.On = false;
                UserButton.IsVisible = false;
                PasswordButton.IsVisible = true;
            }
            else
            {
                PasswordButton.IsVisible = false;
            }
        }

        private async Task GetData()
        {
            var response = RestConnector.GetObjectAsync(RestConnector.User);
            var user = JsonConvert.DeserializeObject<User>(await response);
            BindingContext = user;
            this.user = user;

            if (user.ImageId != null)
                _ = LoadImage(user.ImageId);
        }

        private async Task LoadImage(int? imageId)
        {
            var img = await RestUtils.GetImage(imageId);
            if (!string.IsNullOrEmpty(img.ImageUrl))
            {
                Img.Source = img.ImageUrl;
                Img.Aspect = Aspect.AspectFill;
            }
        }
    }
}