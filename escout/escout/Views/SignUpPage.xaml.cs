﻿using escout.Models;
using escout.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace escout.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignUpPage : ContentPage
    {
        private User user = new User();

        public SignUpPage()
        {
            InitializeComponent();
            BindingContext = new AuthenticationViewModel(Navigation, user);
        }
    }
}