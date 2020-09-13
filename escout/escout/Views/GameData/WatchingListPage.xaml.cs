﻿using escout.Helpers;
using escout.Models.Db;
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
            var game = e.SelectedItem as DbGame;
            Navigation.PushAsync(new GameDetailsPage(game));
        }

        private async void OnRemoveClicked(object sender, EventArgs e)
        {
            var menuItem = sender as MenuItem;
            var game = menuItem.CommandParameter as DbGame;

            var db = new LocalDb();
            await db.RemoveGameData(game.DataExt);
            await Navigation.PushAsync(new WatchingListPage());
        }
    }
}