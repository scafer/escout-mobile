using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace escout.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            lbl_message.Text = "This app allows you to register events from one athlete during a football game. The web portal is available at: https://www.escout-web.herokuapp.com\n" +
            "\n" +
            "Steps to register events:\n" +
            " - Go to \"Games\" page;\n" +
            " - Select the game you want. Click \"Search\" to see all games;\n" +
            " - Click \"Add\" to save the game localy (Top right);\n" +
            " - Go to the \"Watching\" page and select the game you added;\n" +
            " - Click \"Start Event Registration\" and then select the team and athlete.\n" +
            " - Click \"Resume\" to start the game and register the events made by the selected athlete.\n" +
            "\n" +
            "Notes:\n" +
            " - If you want to change the timer click on it and insert the desired time;\n" +
            " - If you register the events offline go to the \"Events\" page and upload them using the syncronization button.\n" +
            "\n" +
            "For feedback, support and problems contact: scafa1@iscte-iul.pt";
        }
    }
}