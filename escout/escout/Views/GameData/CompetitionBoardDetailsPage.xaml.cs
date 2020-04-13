using escout.Models.Rest;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace escout.Views.GameData
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CompetitionBoardDetailsPage : ContentPage
    {
        public CompetitionBoardDetailsPage(Competition competition)
        {
            InitializeComponent();
            BindingContext = competition;
        }
    }
}