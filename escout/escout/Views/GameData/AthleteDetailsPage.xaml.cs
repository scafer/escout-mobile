using escout.Helpers;
using escout.Models;
using System.Threading.Tasks;
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

            if (athlete.ImageId != null)
                _ = LoadImage(athlete.ImageId);
        }

        private async Task LoadImage(int? imageId)
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