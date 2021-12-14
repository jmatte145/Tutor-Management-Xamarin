using System;
using System.Collections.Generic;
using TutorManagementiOS.Models;
using Xamarin.Forms;

namespace TutorManagementiOS
{
    public partial class UserDetailPageStudent : ContentPage
    {
        FirebaseRepo db = new FirebaseRepo();

        public UserDetailPageStudent()
        {
            InitializeComponent();
            displayUser();
        }
        async void displayUser()
        {
            //List<UserClass> list = await App.Database.GetPeopleAsyncUsr();
            //var list= await App.Database.GetPeopleAsyncUsr();

            List<UserClass> list = new List<UserClass>();
            list.Add(await db.GetByUserId(AuthorizationPage.userId));
            collectionView.ItemsSource = list;

            List<Student> lister = new List<Student>();
            lister.Add(await db.GetStudentByID(AuthorizationPage.typeUserId));
            collectionView2.ItemsSource = lister;

        }
        async void btnAuth_Clicked(object sender, EventArgs e)
        {
            List<UserClass> list = new List<UserClass>();
            list = await db.GetAllUsers();
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].userID.Equals(AuthorizationPage.userId))
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
            UserClass user1 = await db.GetByUserId(AuthorizationPage.userId);
            await db.DeleteUser(user1);
            nav();
        }

        async void btnUpdateRecord_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new UserUpdatePageStudent());
        }

        void goHome(object sender, EventArgs e)
        {
            nav();
        }

        async void nav()
        {
            await Navigation.PushAsync(new HomeAdmin());
        }
    }
}
