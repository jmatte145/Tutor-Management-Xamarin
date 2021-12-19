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
        public static string courseId;
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

        private void btnModifyCourse_Clicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            courseId = (string)button.Text;
            nav();
        }
        async void nav()
        {
            await Navigation.PushAsync(new UpdateCoursePage()); ;

        }
    }
}