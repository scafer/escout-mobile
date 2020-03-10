using System.ComponentModel;
using Xamarin.Forms;

namespace escout.ViewModels
{
    class WatchingListViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public INavigation Navigation;

        public WatchingListViewModel(INavigation navigation)
        {
            this.Navigation = navigation;
        }
    }
}
