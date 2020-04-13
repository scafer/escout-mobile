using escout.Models.Rest;
using escout.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace escout.Views.GameData
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CompetitionBoardsPage : ContentPage
    {
        public CompetitionBoardsPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            BindingContext = new CompetitionBoardViewModel(Navigation);
        }

        private void ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var competition = e.SelectedItem as Competition;
            Navigation.PushAsync(new CompetitionBoardDetailsPage(competition));
        }
    }
}