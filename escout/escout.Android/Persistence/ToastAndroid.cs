using Android.App;
using Android.Widget;
using escout.Droid.Persistence;
using escout.Services.Dependency;

[assembly: Xamarin.Forms.Dependency(typeof(ToastAndroid))]

namespace escout.Droid.Persistence
{
    class ToastAndroid : IToast
    {
        public void LongAlert(string message)
        {
            Toast.MakeText(Application.Context, message, ToastLength.Long).Show();
        }

        public void ShortAlert(string message)
        {
            Toast.MakeText(Application.Context, message, ToastLength.Short).Show();
        }
    }
}
