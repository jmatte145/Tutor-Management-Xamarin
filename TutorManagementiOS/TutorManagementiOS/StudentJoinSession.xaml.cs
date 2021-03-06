using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TutorManagementiOS.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TutorManagementiOS
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StudentJoinSession : ContentPage
    {
        public static string sessionIDforstudent;
        FirebaseRepo db = new FirebaseRepo();
        public StudentJoinSession()
        {
            InitializeComponent();
            displaySessions();
        }
        async void displaySessions()
        {
            List<SessionClass> list = await db.GetAllSessions();
            List<SessionClass> listgood = new List<SessionClass>();
            List<UserClass> listeroo = await db.GetAllUsers();
            List<CourseMemberClass> lister = await db.GetAllCoursesMembers();
            List<CourseClass> listering = await db.GetAllCourses();
            List<Student> listing1 = await db.GetAllStudents();
            for (int i = 0; i < listeroo.Count; i++)
            {
                for (int a = 0; a < listing1.Count; a++)
                {
                    if (listing1[a].genUserID.Equals(listeroo[i].userID))
                    {
                        for (int h = 0; h < lister.Count; h++)
                        {
                            if (lister[h].userID.Equals(listeroo[i].userID))
                            {
                                for (int p = 0; p < listering.Count; p++)
                                {
                                    if (listering[p].courseID.Equals(lister[h].courseID))
                                    {
                                        for (int g = 0; g < list.Count; g++)
                                        {
                                            if (list[g].course.Equals(listering[p].courseName) & list[g].completed == true)
                                            {
                                                listgood.Add(list[g]);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            if (listgood.Any())
            {
                collectionView.ItemsSource = listgood;
            }
            else
            {
                _ = DisplayAlert("Error", "DB empty...", "ok");
            }
        }
        async void goDetail(object sender, EventArgs e)
        {
            var button = sender as Button;
            sessionIDforstudent = (string)button.Text;
            List<SessionClass> list = await db.GetAllSessions();

            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].sessionID.Equals(sessionIDforstudent))
                {

                    _ = await db.SaveSessionMember(new SessionMemberClass
                    {
                        sessionID = list[i].sessionID,
                        userID = ViewModels.LoginViewModel.currentUser,
                    });
                    _ = DisplayAlert("Success", "You have joined the sesssion", "Ok");
                }
            }


        }

    }
}