
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace escout.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CompetitionBoardsPage : ContentPage
    {
        public CompetitionBoardsPage()
        {
            InitializeComponent();
        }
        private void SearchExecuted(object sender, EventArgs e)
        {
            activityIndicator.IsRunning = true;
        }
    }
}