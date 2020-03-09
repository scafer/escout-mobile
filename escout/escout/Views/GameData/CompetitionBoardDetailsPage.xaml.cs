using escout.Models;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace escout.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CompetitionBoardDetailsPage : ContentPage
    {
        public CompetitionBoardDetailsPage(CompetitionBoard competitionBoard)
        {
            InitializeComponent();
        }
    }
}