using escout.Models.Rest;
using escout.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace escout.Views.Authentication
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ForgotPasswordPage : ContentPage
    {
        private User user = new User();

        public ForgotPasswordPage()
        {
            InitializeComponent();
            BindingContext = new AuthenticationViewModel(Navigation, user);
        }
    }
}