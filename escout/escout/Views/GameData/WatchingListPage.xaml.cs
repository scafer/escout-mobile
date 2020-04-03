using escout.Helpers;
using escout.Models.Db;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace escout.Views.GameData
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WatchingListPage : ContentPage
    {
        public WatchingListPage() => InitializeComponent();

        protected override void OnAppearing()
        {
            base.OnAppearing();
            LoadData();
        }

        private void GamesList_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var game = e.SelectedItem as DbGame;
            Navigation.PushAsync(new GameDetailsPage(game));
        }

        private async void RemoveItem_OnClicked(object sender, EventArgs e)
        {
            var menuItem = sender as MenuItem;
            var game = menuItem.CommandParameter as DbGame;

            var db = new LocalDb();
            await db.RemoveGameData(game.DataExt);
            LoadData();
        }

        private async void LoadData()
        {
            var db = new LocalDb();
            PendingGamesList.ItemsSource = await db.GetDbGame(0);
            ActiveGamesList.ItemsSource = await db.GetDbGame(1);
            FinishedGamesList.ItemsSource = await db.GetDbGame(2);
        }

        private async void MenuItem_OnClicked(object sender, EventArgs e)
        {
            var menuItem = sender as MenuItem;
            var game = menuItem.CommandParameter as DbGame;
            await Navigation.PushAsync(new GameDetailsPage(game));
        }
    }
}