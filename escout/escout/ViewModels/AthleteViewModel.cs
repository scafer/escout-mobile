using System.ComponentModel;
using Xamarin.Forms;

namespace escout.ViewModels
{
    class AthleteViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public INavigation Navigation;

        public AthleteViewModel(INavigation navigation)
        {
            this.Navigation = navigation;
        }
    }
}
