using escout.Models.Database;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace escout.Views.Events
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EventDetailsPage : ContentPage
    {
        public EventDetailsPage(DbGameEvent dbGameEvent)
        {
            InitializeComponent();
            BindingContext = dbGameEvent;
        }
    }
}