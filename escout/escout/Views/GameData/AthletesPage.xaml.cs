using escout.Helpers;
using escout.Models.Rest;
using escout.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace escout.Views.GameData
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AthletesPage : ContentPage
    {
        public AthletesPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            BindingContext = new AthleteViewModel(Navigation);
            filter.ItemsSource = Utils.AthleteFilter;
        }

        private void ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var athlete = e.SelectedItem as Athlete;
            Navigation.PushAsync(new AthleteDetailsPage(athlete));
        }
    }
}