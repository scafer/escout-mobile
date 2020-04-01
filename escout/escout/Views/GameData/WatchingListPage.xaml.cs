using System;
using escout.Models.Db;
using SQLite;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace escout.Views.GameData
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WatchingListPage : ContentPage
    {
        private SQLiteAsyncConnection _connection;

        public WatchingListPage()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            _connection = DependencyService.Get<ISQLiteDb>().GetConnection();

            await _connection.CreateTableAsync<DbAthlete>();
            await _connection.CreateTableAsync<DbClub>();
            await _connection.CreateTableAsync<DbCompetition>();
            await _connection.CreateTableAsync<DbEvent>();
            await _connection.CreateTableAsync<DbGame>();
            await _connection.CreateTableAsync<DbGameEvent>();
            await _connection.CreateTableAsync<DbSport>();

            ActiveGamesList.ItemsSource = await _connection.Table<DbGame>().ToListAsync();
            PendingGamesList.ItemsSource = await _connection.Table<DbGame>().ToListAsync();
            FinishedGamesList.ItemsSource = await _connection.Table<DbGame>().ToListAsync();
        }

        private void GamesList_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var game = e.SelectedItem as DbGame;
            Navigation.PushAsync(new GameDetailsPage(game));
        }


        private void RemoveItem_OnClicked(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}