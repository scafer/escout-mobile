using System;
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
                case "Home":
                    Detail = new NavigationPage(new UserDetailsPage())
                    {
                        //BarBackgroundColor = Color.FromHex("#262626")
                    };
                    break;
                case "Watching List":
                    break;
                case "Games":
                    break;
                case "Clubs":
                    break;
                case "Competition Boards":
                    break;
                case "Athletes":
                    break;
            }
            IsPresented = false;
        }

        private List<Option> GetOptions()
        {
            var option = new List<Option>
            {
                new Option{Name = "Home", ImageUrl = ""},
                new Option{Name = "Watching List", ImageUrl = ""},
                new Option{Name = "Games", ImageUrl = ""},
                new Option{Name = "Clubs", ImageUrl = ""},
                new Option{Name = "Competition Boards", ImageUrl = ""},
                new Option{Name = "Athletes", ImageUrl = ""}
            };

            return option;
        }

        private void UserSettings_OnTapped(object sender, EventArgs e)
        {
            Detail = new NavigationPage(new UserDetailsPage())
            {
                //BarBackgroundColor = Color.FromHex("#262626")
            };
        }
    }
}