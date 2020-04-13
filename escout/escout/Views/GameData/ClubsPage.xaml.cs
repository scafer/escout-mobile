using escout.Models.Rest;
using escout.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace escout.Views.GameData
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ClubsPage : ContentPage
    {
        public ClubsPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            BindingContext = new ClubViewModel(Navigation);
        }

        private void ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var club = e.SelectedItem as Club;
            Navigation.PushAsync(new ClubDetailsPage(club));
        }
    }
}