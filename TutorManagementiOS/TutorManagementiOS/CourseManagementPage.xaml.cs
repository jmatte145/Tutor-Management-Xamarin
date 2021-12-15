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

        //async void goDetail(object sender, EventArgs e)
        //{
        //    var button = sender as Button;
        //    courseId = (string)button.Text;
        //    List<UserClass> list = await db.GetAllUsers();

        //    for (int i = 0; i < list.Count; i++)
        //    {
        //        if (list[i].userID.Equals(userId))
        //        {

        //            var listing1 = await db.GetAllStudents();

        //            for (int a = 0; a < listing1.Count; a++)
        //            {
        //                if (listing1.Any() & listing1[a].genUserID.Equals(list[i].userID))
        //                {
        //                    typeUserId = listing1[a].studentID;
        //                    //nav student home page
        //                    navstudent();
        //                    //DisplayValidLoginPrompt();
        //                }
        //            }
        //            var listing2 = await db.GetAllTutors();

        //            for (int n = 0; n < listing2.Count; n++)
        //            {
        //                if (listing2.Any() & listing2[n].genUserID.Equals(list[i].userID))
        //                {
        //                    typeUserId = listing2[n].tutorID;
        //                    //nav tutor home page
        //                    navtutor();
        //                    //DisplayValidLoginPrompt();
        //                }
        //            }
        //            var listing3 = await db.GetAllTeachers();

        //            for (int m = 0; m < listing3.Count; m++)
        //            {
        //                if (listing3.Any() & listing3[m].genUserID.Equals(list[i].userID))
        //                {
        //                    typeUserId = listing3[m].teacherID;
        //                    //nav student home page
        //                    navteacher();
        //                    //DisplayValidLoginPrompt();
        //                }
        //            }
        //        }
        //    }


        //}
    }
}