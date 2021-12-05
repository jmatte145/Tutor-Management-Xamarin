using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace TutorManagementiOS
{
    public partial class AuthorizationPage : ContentPage
    {
        public static string userId;
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
        void goDetail(object sender, CheckedChangedEventArgs e)
        {
            var radiobtn = sender as RadioButton;
            userId = (string)radiobtn.Content;
            nav();

        }
        async void nav()
        {
            await Navigation.PushAsync(new UserDetailPage()); ;

        }
    }
}
