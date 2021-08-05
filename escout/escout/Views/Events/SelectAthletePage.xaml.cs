using escout.Helpers;
using escout.Models.Database;
using escout.Services.Database;
using escout.Services.Rest;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace escout.Views.Events
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SelectAthletePage : ContentPage
    {
        private List<DbClub> dbClubs;

        public SelectAthletePage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            dbClubs = await new Query().GetClubs(App.DbGame.DataExt);
            bt_home.Text = dbClubs[0].Name;
            bt_visitor.Text = dbClubs[1].Name;
            _ = LoadImages(dbClubs[0].ImageId, dbClubs[1].ImageId);
        }

        private async Task LoadImages(int? homeImageId, int? visitorImageId)
        {
            try
            {
                if (homeImageId != null)
                {
                    var img1 = await RestUtils.GetImage(homeImageId);
                    if (!string.IsNullOrEmpty(img1.ImageUrl))
                        img_home.Source = img1.ImageUrl;
                }

                if (visitorImageId != null)
                {
                    var img2 = await RestUtils.GetImage(visitorImageId);
                    if (!string.IsNullOrEmpty(img2.ImageUrl))
                        img_visitor.Source = img2.ImageUrl;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private async void Bt_home_OnClicked(object sender, EventArgs e)
        {
            listView.ItemsSource = await new Query().GetAthletes(App.DbGame.DataExt, dbClubs[0].Id);
        }

        private async void Bt_visitor_OnClicked(object sender, EventArgs e)
        {
            listView.ItemsSource = await new Query().GetAthletes(App.DbGame.DataExt, dbClubs[1].Id);
        }

        private async void Button_OnClicked(object sender, EventArgs e)
        {
            if (listView.SelectedItem != null)
            {
                if (await App.DisplayMessage(ConstValues.TITLE_STATUS_INFO, ConstValues.MSG_EVENT_PAGE, ConstValues.OPTION_NO, ConstValues.OPTION_YES))
                {
                    App.DbAthlete = listView.SelectedItem as DbAthlete;
                    _ = RestUtils.AddGameUser(App.DbGame.Id, App.DbAthlete.Id);
                    _ = RestUtils.UpdateGameStatus(App.DbGame.Id, 1);
                    Application.Current.MainPage = new NavigationPage(new RegisterEventPage());
                    await Navigation.PushAsync(new RegisterEventPage());
                }
            }
            else
                await App.DisplayMessage(ConstValues.TITLE_STATUS_INFO, ConstValues.MSG_SELECT_ATHLETE, ConstValues.OPTION_OK);
        }
    }
}