using escout.Models.Database;
using escout.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace escout.Views.Events
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EventsPage : ContentPage
    {
        public EventsPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            BindingContext = new EventsViewModel(Navigation);
        }

        private async void ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var dbGameEvent = e.SelectedItem as DbGameEvent;
            await Navigation.PushAsync(new EventDetailsPage(dbGameEvent));
        }
    }
}