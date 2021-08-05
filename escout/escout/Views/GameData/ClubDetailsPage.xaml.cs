using escout.Models.Rest;
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

            if (club.DisplayOptions.ContainsKey("imageUrl"))
            {
                Img.Source = club.DisplayOptions["imageUrl"];
                Img.Aspect = Aspect.AspectFill;
            }
        }
    }
}