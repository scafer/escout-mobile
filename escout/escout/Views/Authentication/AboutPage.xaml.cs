using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace escout.Views.Authentication
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AboutPage : ContentPage
    {
        public AboutPage()
        {
            InitializeComponent();

            if (Device.RuntimePlatform == Device.Android)
            {
                BtnBack.IsVisible = false;
            }
        }

        void BtnBack_Clicked(object sender, System.EventArgs e)
        {
            Navigation.PopModalAsync();
        }
    }
}