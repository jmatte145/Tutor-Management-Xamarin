using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace TutorManagementiOS
{
    public partial class UserDetailPage : ContentPage
    {
        FirebaseRepo db = new FirebaseRepo();
        public UserDetailPage()
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

        async void btnDeleteRecord_Clicked(object sender, EventArgs e)
        {
            UserClass user1 = await db.GetByUserId(AuthorizationPage.userId);
            await db.DeleteUser(user1);
            nav();
        }

        async void goHome(object sender, EventArgs e)
        {
            nav();
        }

        async void nav()
        {
            await Navigation.PushAsync(new HomeAdmin());
        }
    }
}
