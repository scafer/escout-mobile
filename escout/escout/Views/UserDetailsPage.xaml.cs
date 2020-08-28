using escout.Helpers;
using escout.Models.Rest;
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
        public UserDetailsPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _ = GetData();
        }

        private void SaveChanges_Clicked(object sender, EventArgs e)
        {

        }

        private void UpdatePassword_Clicked(object sender, EventArgs e)
        {

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