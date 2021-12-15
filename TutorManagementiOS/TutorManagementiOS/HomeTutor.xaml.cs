using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace TutorManagementiOS
{
    public partial class HomeTutor : ContentPage
    {
        public HomeTutor()
        {
            InitializeComponent();
        }
        async void goLogin(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new LoginPage()); ;
        }
        async void createSession(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new CreateSessionPage()); ;
        }
        async void viewSession(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new ViewSessionPage()); ;
        }

        async void modAvail(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new ModAvailPage()); ;
        }
    }
}
