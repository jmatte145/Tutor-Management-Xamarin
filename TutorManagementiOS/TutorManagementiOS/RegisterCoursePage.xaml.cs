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
    public partial class RegisterCoursePage : ContentPage
    {
        FirebaseRepo fbr = new FirebaseRepo();
        public RegisterCoursePage()
        {
            InitializeComponent();
        }

        //async void btnCourseSubmit_Clicked(object sender, EventArgs e)
        //{
        //    if(!string.IsNullOrEmpty(cName.Text) && !string.IsNullOrEmpty(cNumber.Text))
        //    {
        //        await fbr.SaveCourse(new CourseClass
        //        {
        //            courseID = ,
        //            courseName = cName.Text,
        //            courseNumber = cNumber.Text
        //        });
        //    }
        //}
    }
}