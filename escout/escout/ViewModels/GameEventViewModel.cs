using System.ComponentModel;
using Xamarin.Forms;

namespace escout.ViewModels
{
    class GameEventViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public INavigation Navigation;

        public GameEventViewModel(INavigation navigation)
        {
            this.Navigation = navigation;
        }
    }
}
