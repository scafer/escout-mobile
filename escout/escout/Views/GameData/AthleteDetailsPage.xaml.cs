using escout.Helpers;
using escout.Models.Rest;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace escout.Views.GameData
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AthleteDetailsPage : ContentPage
    {
        public AthleteDetailsPage(Athlete athlete)
        {
            InitializeComponent();
            BindingContext = athlete;

            if (athlete.ImageId != null)
                _ = LoadImage(athlete.ImageId);
            if (athlete.ClubId != null)
                _ = LoadClub(athlete.ClubId);
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

        private async Task LoadClub(int? clubId)
        {
            var club = await RestUtils.GetClub((int)clubId);
            lblClubName.Text = club.Name;
        }
    }
}