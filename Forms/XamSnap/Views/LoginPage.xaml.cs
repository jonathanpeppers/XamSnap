using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace XamSnap
{
    public partial class LoginPage : ContentPage
    {
        readonly LoginViewModel loginViewModel = new LoginViewModel();

        public LoginPage()
        {
            Title = "XamSnap";
            BindingContext = loginViewModel;

            loginViewModel.LoginCommand = new Command(async () =>
            {
                try
                {
                    await loginViewModel.Login();

                    await Navigation.PushAsync(new ConversationsPage());
                }
                catch (Exception exc)
                {
                    await DisplayAlert("Oops!", exc.Message, "Ok");
                }
            });

            InitializeComponent();
        }
    }
}
