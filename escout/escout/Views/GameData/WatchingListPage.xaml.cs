﻿using escout.Models.Database;
using escout.Services.Database;
using escout.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace escout.Views.GameData
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WatchingListPage : ContentPage
    {
        public WatchingListPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            BindingContext = new WatchingListViewModel(Navigation);
        }

        private void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var game = e.SelectedItem as DbGameWithClub;
            Navigation.PushAsync(new GameDetailsPage(game.DbGame));
        }

        private async void OnRemoveClicked(object sender, EventArgs e)
        {
            var menuItem = sender as MenuItem;
            var game = menuItem.CommandParameter as DbGameWithClub;
            await new Query().RemoveGameData(game.DbGame.DataExt);
        }
    }
}