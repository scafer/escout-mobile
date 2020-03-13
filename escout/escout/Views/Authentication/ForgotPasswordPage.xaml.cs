﻿using escout.Models;
using escout.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace escout.Views.Authentication
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ForgotPasswordPage : ContentPage
    {
        User user = new User();

        public ForgotPasswordPage()
        {
            InitializeComponent();
            BindingContext = new AuthenticationViewModel(Navigation, user);
        }
    }
}