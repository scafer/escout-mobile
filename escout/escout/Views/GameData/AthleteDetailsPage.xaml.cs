using escout.Models.Rest;
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

            if (athlete.DisplayOptions.ContainsKey("imageUrl"))
            {
                Img.Source = athlete.DisplayOptions["imageUrl"];
                Img.Aspect = Aspect.AspectFill;
            }

            if (athlete.DisplayOptions.ContainsKey("clubName"))
            {
                lblClubName.Text = athlete.DisplayOptions["clubName"];
            }
        }
    }
}