using System.ComponentModel;
using Xamarin.Forms;

namespace escout.ViewModels
{
    class GameViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public INavigation Navigation;

        public GameViewModel(INavigation navigation)
        {
            this.Navigation = navigation;
        }
    }
}
