using escout.Helpers;
using escout.Models.Db;
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

        private DbGame selectedGame;
        private ObservableCollection<DbGame> activeGames;
        private ObservableCollection<DbGame> pendingGames;
        private ObservableCollection<DbGame> finishedGames;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public WatchingListViewModel(INavigation navigation)
        {
            Navigation = navigation;
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

        public ObservableCollection<DbGame> ActiveGames
        {
            get => activeGames;
            set
            {
                activeGames = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<DbGame> PendingGames
        {
            get => pendingGames;
            set
            {
                pendingGames = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<DbGame> FinishedGames
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
            PendingGames = new ObservableCollection<DbGame>(await db.GetDbGame(0));
            ActiveGames = new ObservableCollection<DbGame>(await db.GetDbGame(1));
            FinishedGames = new ObservableCollection<DbGame>(await db.GetDbGame(2));
        }
    }
}
