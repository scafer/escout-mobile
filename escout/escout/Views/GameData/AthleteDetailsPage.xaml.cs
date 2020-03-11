using escout.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace escout.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AthleteDetailsPage : ContentPage
    {
        public AthleteDetailsPage(Athlete athlete)
        {
            InitializeComponent();
            BindingContext = athlete;
        }
    }
}