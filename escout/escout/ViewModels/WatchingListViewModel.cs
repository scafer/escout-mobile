using escout.Helpers;
using escout.Models;
using escout.Models.Database;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;

namespace escout.ViewModels
{
    class WatchingListViewModel : INotifyPropertyChanged
    {
        public INavigation Navigation;
        public ICommand RemoveGameCommand { get; set; }
        public ICommand UpdateViewCommand { get; set; }

        private DbGame selectedGame;
        private ObservableCollection<DbGameWithClub> activeGames;
        private ObservableCollection<DbGameWithClub> pendingGames;
        private ObservableCollection<DbGameWithClub> finishedGames;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public WatchingListViewModel(INavigation navigation)
        {
            Navigation = navigation;
            UpdateViewCommand = new Command(GetGames);
            GetGames();
        }

        public DbGame SelectedGame
        {
            get => selectedGame;
            set
            {
                selectedGame = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<DbGameWithClub> ActiveGames
        {
            get => activeGames;
            set
            {
                activeGames = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<DbGameWithClub> PendingGames
        {
            get => pendingGames;
            set
            {
                pendingGames = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<DbGameWithClub> FinishedGames
        {
            get => finishedGames;
            set
            {
                finishedGames = value;
                OnPropertyChanged();
            }
        }

        private async void GetGames()
        {
            var db = new LocalDb();

            var pending = await db.GetDbGame(0);
            var active = await db.GetDbGame(1);
            var finished = await db.GetDbGame(2);

            var pendingList = new List<DbGameWithClub>();
            var activeList = new List<DbGameWithClub>();
            var finishedList = new List<DbGameWithClub>();

            foreach (var game in pending)
            {
                var g = new DbGameWithClub(game)
                {
                    DbClubHome = await new LocalDb().GetClub(game.HomeId),
                    DbClubVisitor = await new LocalDb().GetClub(game.VisitorId)
                };
                pendingList.Add(g);
            }

            foreach (var game in active)
            {
                var g = new DbGameWithClub(game)
                {
                    DbClubHome = await new LocalDb().GetClub(game.HomeId),
                    DbClubVisitor = await new LocalDb().GetClub(game.VisitorId)
                };
                activeList.Add(g);
            }

            foreach (var game in finished)
            {
                var g = new DbGameWithClub(game)
                {
                    DbClubHome = await new LocalDb().GetClub(game.HomeId),
                    DbClubVisitor = await new LocalDb().GetClub(game.VisitorId)
                };
                finishedList.Add(g);
            }

            PendingGames = new ObservableCollection<DbGameWithClub>(pendingList);
            ActiveGames = new ObservableCollection<DbGameWithClub>(activeList);
            FinishedGames = new ObservableCollection<DbGameWithClub>(finishedList);
        }
    }
}
