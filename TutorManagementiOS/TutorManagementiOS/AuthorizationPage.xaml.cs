using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace TutorManagementiOS
{
    public partial class AuthorizationPage : ContentPage
    {
        public static string userId;
        public static string typeUserId;
        FirebaseRepo db = new FirebaseRepo();
        public AuthorizationPage()
        {
            InitializeComponent();
            displayUsers();
        }
        async void displayUsers()
        {
            List<UserClass> list = await db.GetAllUsers();
            if (list.Any())
            {
                collectionView.ItemsSource = await db.GetAllUsers();
            }
            else
            {
                _ = DisplayAlert("Error", "DB empty...", "ok");
            }
        }
        async void goDetail(object sender, EventArgs e)
        {
            var button = sender as Button;
            userId = (string)button.Text;
            List<UserClass> list = await db.GetAllUsers();

            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].userID.Equals(userId))
                {

                    var listing1 = await db.GetAllStudents();

                    for (int a = 0; a < listing1.Count; a++)
                    {
                        if (listing1.Any() & listing1[a].genUserID.Equals(list[i].userID))
                        {
                            typeUserId = listing1[a].studentID;
                            //nav student home page
                            navstudent();
                            //DisplayValidLoginPrompt();
                        }
                    }
                    var listing2 = await db.GetAllTutors();

                    for (int n = 0; n < listing2.Count; n++)
                    {
                        if (listing2.Any() & listing2[n].genUserID.Equals(list[i].userID))
                        {
                            typeUserId = listing2[n].tutorID;
                            //nav tutor home page
                            navtutor();
                            //DisplayValidLoginPrompt();
                        }
                    }
                    var listing3 = await db.GetAllTeachers();

                    for (int m = 0; m < listing3.Count; m++)
                    {
                        if (listing3.Any() & listing3[m].genUserID.Equals(list[i].userID))
                        {
                            typeUserId = listing3[m].teacherID;
                            //nav student home page
                            navteacher();
                            //DisplayValidLoginPrompt();
                        }
                    }
                }
            }


        }
        async void navstudent()
        {
            await Navigation.PushAsync(new UserDetailPageStudent()); ;

        }
        async void navtutor()
        {
            await Navigation.PushAsync(new UserDetailPageTutor()); ;

        }
        async void navteacher()
        {
            await Navigation.PushAsync(new UserDetailPageTeacher()); ;

        }
    }
}
