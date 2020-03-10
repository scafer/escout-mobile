using System.ComponentModel;
using Xamarin.Forms;

namespace escout.ViewModels
{
    class ClubViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public INavigation Navigation;

        public ClubViewModel(INavigation navigation)
        {
            this.Navigation = navigation;
        }
    }
}
