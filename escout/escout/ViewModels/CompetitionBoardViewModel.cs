using System.ComponentModel;
using Xamarin.Forms;

namespace escout.ViewModels
{
    class CompetitionBoardViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public INavigation Navigation;

        public CompetitionBoardViewModel(INavigation navigation)
        {
            this.Navigation = navigation;
        }
    }
}
