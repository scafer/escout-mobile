using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace escout.Views.GameData
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WatchingListPage : ContentPage
    {
        public WatchingListPage()
        {
            InitializeComponent();
        }

        private void ActiveGamesList_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            throw new System.NotImplementedException();
        }

        private void PendingGamesList_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void FinishedGamesList_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void RemoveItem_OnClicked(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}