
using escout.Helpers;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace escout.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AthletesPage : ContentPage
    {
        public AthletesPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            filter.ItemsSource = Utils.AthleteFilter;
        }

        private void SearchExecuted(object sender, EventArgs e)
        {
            activityIndicator.IsRunning = true;
        }
    }
}