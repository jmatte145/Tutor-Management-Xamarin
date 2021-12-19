using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace TutorManagementiOS
{
    public partial class HomeStudent : ContentPage
    {
        public HomeStudent()
        {
            InitializeComponent();
        }
        async void goLogin(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new LoginPage()); ;
        }
        async void joinSession(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new StudentJoinSession()); ;
        }
        async void viewSession(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new StudentViewSession()); ;
        }
    }
}
