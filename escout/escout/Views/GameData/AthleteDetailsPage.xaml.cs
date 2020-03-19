using System;
using escout.Helpers;
using escout.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace escout.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AthleteDetailsPage : ContentPage
    {
        public AthleteDetailsPage(Athlete athlete)
        {
            InitializeComponent();
            BindingContext = athlete;

            if (true)
                LoadImage(1);
            else
                Img.Source = "user_image.png";
        }

        private async void LoadImage(int imageId)
        {
            var img = await Utils.GetImage(imageId);
            if (!String.IsNullOrEmpty(img.ImageUrl))
                Img.Source = img.ImageUrl;
        }
    }
}