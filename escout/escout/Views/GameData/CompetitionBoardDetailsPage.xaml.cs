using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using escout.Models.Rest;
using escout.Services.Rest;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace escout.Views.GameData
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CompetitionBoardDetailsPage : ContentPage
    {
        private Competition competition;

        public CompetitionBoardDetailsPage(Competition competition)
        {
            InitializeComponent();
            BindingContext = competition;
            this.competition = competition;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _ = GetCompetitionBoard();
        }

        private async Task GetCompetitionBoard()
        {
            var board = new List<BoardDetails>();
            var competitionBoard = await RestUtils.GetCompetitionBoardById(competition.Id);
            competitionBoard.OrderBy(c => c.Position);

            foreach (var c in competitionBoard)
            {
                board.Add(new BoardDetails { Position = "Position: " + c.Position + " Points: " + c.Points, ClubName = c.DisplayOptions["clubName"] });
            }

            list_competitionBoard.ItemsSource = board;
        }
    }

    public class BoardDetails
    {
        public string Position { get; set; }
        public string ClubName { get; set; }
    }
}