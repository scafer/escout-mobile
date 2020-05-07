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

        private async Task GetData()
        {
            var response = RestConnector.GetObjectAsync(RestConnector.User);
            var user = JsonConvert.DeserializeObject<User>(await response);
            BindingContext = user;

            if (user.ImageId != null)
                _ = LoadImage(user.ImageId);
        }

        private void Button_OnClicked(object sender, EventArgs e)
        {
            throw new NotImplementedException();
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