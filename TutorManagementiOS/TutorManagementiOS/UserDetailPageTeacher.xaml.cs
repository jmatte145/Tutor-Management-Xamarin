using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace TutorManagementiOS
{
    public partial class UserDetailPageTeacher : ContentPage
    {
        FirebaseRepo db = new FirebaseRepo();
        public UserDetailPageTeacher()
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