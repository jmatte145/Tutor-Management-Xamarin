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
    public partial class UserDetailPageStudentTeacher : ContentPage
    {
        FirebaseRepo db = new FirebaseRepo();

        public UserDetailPageStudentTeacher()
        {
            InitializeComponent();
            displayUser();
        }
        async void displayUser()
        {
            List<UserClass> list = new List<UserClass>();
            list.Add(await db.GetByUserId(ViewStudents.genUserID));
            collectionView.ItemsSource = list;

            List<Student> list2 = new List<Student>();
            list2.Add(await db.GetStudentByID(ViewStudents.typeUserId));
            collectionView2.ItemsSource = list2;

        }
        async void btnAuth_Clicked(object sender, EventArgs e)
        {
            List<UserClass> list = new List<UserClass>();
            list = await db.GetAllUsers();
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].userID.Equals(ViewStudents.genUserID))
                {
                    list[i].approvalStatus = !list[i].approvalStatus;
                    await db.UpdateUser(list[i]);
                    _ = DisplayAlert("Status Updated", "The approval status is now: " + list[i].approvalStatus, "ok");
                    nav();
                }
            }
        }

        async void btnDeleteRecord_Clicked(object sender, EventArgs e)
        {
            UserClass user1 = await db.GetByUserId(ViewStudents.genUserID);
            await db.DeleteUser(user1);
            nav();
        }

        async void btnUpdateRecord_Clicked(object sender, EventArgs e)
        {
            //do for teacher
            await Navigation.PushAsync(new UserUpdatePageStudent());
        }

        void goHome(object sender, EventArgs e)
        {
            nav();
        }

        async void nav()
        {
            await Navigation.PushAsync(new HomeTeacher());
        }
    }
}
