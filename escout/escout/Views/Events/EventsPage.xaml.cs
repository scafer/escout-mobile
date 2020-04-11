using escout.Helpers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace escout.Views.Events
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EventsPage : ContentPage
    {
        public EventsPage() => InitializeComponent();

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            listView.ItemsSource = await new LocalDb().GetGameEvents();
        }
    }
}