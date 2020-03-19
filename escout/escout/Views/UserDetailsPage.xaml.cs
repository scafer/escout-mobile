using escout.Helpers;
using escout.Models;
using Newtonsoft.Json;
using System;
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
            GetData();
        }

        private async void GetData()
        {
            var response = RestConnector.GetObjectAsync(RestConnector.User);
            var user = JsonConvert.DeserializeObject<User>(await response);
            BindingContext = user;

            if (user.ImageId != null)
                LoadImage(user.ImageId);
        }

        private void Button_OnClicked(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        public async void LoadImage(int? imageId)
        {
            var img = await Utils.GetImage(imageId);
            if (!String.IsNullOrEmpty(img.ImageUrl))
            {
                Img.Source = img.ImageUrl;
                Img.Aspect = Aspect.AspectFill;
            }
        }
    }
}