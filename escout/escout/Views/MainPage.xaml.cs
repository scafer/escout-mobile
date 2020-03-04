using escout.Models;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace escout.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : MasterDetailPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            OptionsListView.ItemsSource = GetOptions();
            Detail = new NavigationPage(new UserDetailsPage());
        }

        private void OptionsListView_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var option = e.SelectedItem as Option;

            switch (option.Name)
            {
                case "User Details":
                    Detail = new NavigationPage(new UserDetailsPage())
                    {
                        //BarBackgroundColor = Color.FromHex("#262626")
                    };
                    break;
            }
            IsPresented = false;
        }

        private List<Option> GetOptions()
        {
            var option = new List<Option>
            {
                new Option{Name="User Details", ImageUrl=""}
            };

            return option;
        }
    }
}