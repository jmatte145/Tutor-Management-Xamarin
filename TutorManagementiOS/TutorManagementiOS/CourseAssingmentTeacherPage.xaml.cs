using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TutorManagementiOS
{
    public partial class CourseAssingmentTeacherPage : ContentPage
    {
        FirebaseRepo db = new FirebaseRepo();

        public CourseAssingmentTeacherPage()
        {
            InitializeComponent();
            displayItems();
        }

        async void displayItems()
        {
            List<UserClass> list = await db.GetAllUsers();
            List<UserClass> listgood = new List<UserClass>();
            if (list.Any())
            {
                for (int i = 0; i < list.Count; i++)
                {
                    var listing2 = await db.GetAllStudents();

                    for (int n = 0; n < listing2.Count; n++)
                    {
                        if (listing2.Any() & listing2[n].genUserID.Equals(list[i].userID))
                        {
                            listgood.Add(list[i]);
                        }
                    }
                }
                User.ItemsSource = listgood;
            }
            else
            {
                _ = DisplayAlert("Error", "DB empty...", "ok");
            }
            List<CourseClass> lister = await db.GetAllCourses();
            if (lister.Any())
            {
                Course.ItemsSource = lister;
            }
            else
            {
                _ = DisplayAlert("Error", "DB empty...", "ok");
            }
        }

        void assignCourseMember(object sender, EventArgs args)
        {
            string temp = User.Items[User.SelectedIndex];
            string temp2 = Course.Items[Course.SelectedIndex];
            if (temp == null)
            {
                _ = DisplayAlert("Error", "No user selected", "OK");
            }
            else if (temp2 == null)
            {
                _ = DisplayAlert("Error", "No course selected", "OK");
            }
            else
            {
                assign(temp, temp2);
            }
        }
        async void assign(string name, string course)
        {
            string userId = "";
            string courseId = "";
            List<UserClass> listuser = await db.GetAllUsers();
            if (listuser.Any())
            {
                for (int i = 0; i < listuser.Count; i++)
                {
                    if (listuser[i].userName.Equals(name))
                    {
                        userId = listuser[i].userID;
                    }
                }
            }
            else
            {
                _ = DisplayAlert("Error", "DB empty...", "ok");
            }

            List<CourseClass> lister = await db.GetAllCourses();
            if (lister.Any())
            {
                for (int i = 0; i < lister.Count; i++)
                {
                    if (lister[i].courseName.Equals(course))
                    {
                        courseId = lister[i].courseID;
                    }
                }
            }
            else
            {
                _ = DisplayAlert("Error", "DB empty...", "ok");
            }

            Console.WriteLine(userId);
            _ = await db.SaveCourseMember(new CourseMemberClass
            {
                courseID = courseId,
                userID = userId,
            });
            _ = DisplayAlert("Success", "User successfully assignend to course", "Ok");
        }

        async void goHome(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new HomeAdmin());
        }


    }
}