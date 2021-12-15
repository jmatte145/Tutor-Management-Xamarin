using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace TutorManagementiOS
{
    public partial class HomeAdmin : ContentPage
    {
        public HomeAdmin()
        {
            InitializeComponent();
        }
        async void goLogin(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new LoginPage()); ;
        }
        async void goAuthorization(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new AuthorizationPage()); ;
        }

        async void goCourse(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CourseManagementPage()); ;
        }

        async void goCourseInfo(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegisterCoursePage()); ;
        }
    }
}
