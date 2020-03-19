using System;
using escout.Helpers;
using escout.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace escout.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ClubDetailsPage : ContentPage
    {
        public ClubDetailsPage(Club club)
        {
            InitializeComponent();
            BindingContext = club;

            if (club.ImageId != null)
                LoadImage(club.ImageId);
        }

        private async void LoadImage(int? imageId)
        {
            var img = await Utils.GetImage(imageId);
            if (!string.IsNullOrEmpty(img.ImageUrl))
            {
                Img.Source = img.ImageUrl;
                Img.Aspect = Aspect.AspectFill;
            }
        }
    }
}