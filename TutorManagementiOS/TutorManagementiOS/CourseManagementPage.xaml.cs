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
    public partial class CourseManagementPage : ContentPage
    {
        public static string courseID;
        FirebaseRepo fbr = new FirebaseRepo();
        public CourseManagementPage()
        {
            InitializeComponent();
            displayCourses();
        }

        async void displayCourses()
        {
            List<CourseClass> list = await fbr.GetAllCourses();
            if (list.Any())
            {
                collectionView.ItemsSource = await fbr.GetAllCourses();
            }
            else
            {
                _ = DisplayAlert("Error", "DB empty...", "ok");
            }
        }

        async void goCourseInfo(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new CourseDetailsPage()); ;
        }

        async void goHome(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new HomeAdmin()); ;
        }

        private void btnModifyCourse_Clicked(object sender, EventArgs e)
        {

        }
    }
}