using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace XamSnap
{
    public partial class ConversationsPage : ContentPage
    {
        readonly MessageViewModel messageViewModel = new MessageViewModel();

        public ConversationsPage()
        {
            Title = "Conversations";
            BindingContext = messageViewModel;

            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            try
            {
                await messageViewModel.GetConversations();
            }
            catch (Exception exc)
            {
                await DisplayAlert("Oops!", exc.Message, "Ok");
            }
        }
    }
}
