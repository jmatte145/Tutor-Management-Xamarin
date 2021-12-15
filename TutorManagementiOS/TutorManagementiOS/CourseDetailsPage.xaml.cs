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
    public partial class CourseDetailsPage : ContentPage
    {
        FirebaseRepo fbr = new FirebaseRepo();
        public CourseDetailsPage()
        {
            InitializeComponent();
        }
        async void navManagementPage()
        {
            await Navigation.PushAsync(new CourseManagementPage()); ;
        }

        private void btnGoBack_Clicked(object sender, EventArgs e)
        {
            navManagementPage();
        }

        async void btnCourseSubmit_Clicked(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(cName.Text) && !string.IsNullOrEmpty(cNumber.Text))
            {
                CourseClass course = new CourseClass
                {
                    courseName = cName.Text,
                    courseNumber = cNumber.Text
                };
                await fbr.SaveCourse(course);
            }
            navManagementPage();
        }
    }
}