using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace escout.Views.Events
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EventsTimeLinePage : ContentPage
    {
        public EventsTimeLinePage()
        {
            InitializeComponent();

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            var list = new List<obj>();
            list.Add(new obj { Name = "Test", ImageUrl = "escout_icon.png", Timer = "00:00" });
            list.Add(new obj { Name = "Test", ImageUrl = "escout_icon.png", Timer = "00:00" });
            list.Add(new obj { Name = "Test", ImageUrl = "escout_icon.png", Timer = "00:00" });
            list.Add(new obj { Name = "Test", ImageUrl = "escout_icon.png", Timer = "00:00" });
            list.Add(new obj { Name = "Test", ImageUrl = "escout_icon.png", Timer = "00:00" });
            timelineListView.ItemsSource = list;
        }

        private void timelineListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            timelineListView.SelectedItem = null;
        }
    }

    public class obj
    {
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string Timer { get; set; }
    }
}