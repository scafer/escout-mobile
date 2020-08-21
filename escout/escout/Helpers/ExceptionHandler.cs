using System;
using System.Net.Http;
using Xamarin.Forms;

namespace escout.Helpers
{
    public static class ExceptionHandler
    {
        public static void GenericException(Exception exception)
        {
            Application.Current.MainPage.DisplayAlert(Message.TITLE_STATUS_ERROR, exception.Message, Message.OPTION_OK);
        }

        public static void HttpRequestException(HttpRequestException exception)
        {
            Application.Current.MainPage.DisplayAlert(Message.TITLE_STATUS_ERROR, exception.Message, Message.OPTION_OK);
        }
    }
}
