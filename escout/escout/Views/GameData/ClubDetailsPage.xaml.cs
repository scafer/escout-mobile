using escout.Models.Rest;
using escout.Services.Rest;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace escout.Views.GameData
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ClubDetailsPage : ContentPage
    {
        public ClubDetailsPage(Club club)
        {
            InitializeComponent();
            BindingContext = club;

            if (club.ImageId != null)
                _ = LoadImage(club.ImageId);
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