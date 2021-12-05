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


        async void goHome(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new HomeAdmin());
        }
    }
}
