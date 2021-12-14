using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TutorManagementiOS
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomeTeacher : ContentPage
    {
        public HomeTeacher()
        {
            InitializeComponent();
        }
        async void goLogin(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new LoginPage()); ;
        }
        async void manageStudent(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new ViewStudents()); ;
        }
        async void gradeSessions(object sender, EventArgs args)
        {
            //change for teacher
            await Navigation.PushAsync(new ViewSessionPage()); ;         
        }
        async void viewTutorHours(object sender, EventArgs args)
        {
            //change for teacher
            await Navigation.PushAsync(new UserDetailPageTutor()); ;
        }



    }
}