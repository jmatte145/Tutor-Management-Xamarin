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
    public partial class UserDetailPageTutorTeacher : ContentPage
    {
        FirebaseRepo db = new FirebaseRepo();
        public UserDetailPageTutorTeacher()
        {
            InitializeComponent();
            displayUser();
        }
        async void displayUser()
        {

            List<UserClass> list = new List<UserClass>();
            list.Add(await db.GetByUserId(AuthorizationPage.userId));
            collectionView.ItemsSource = list;

            List<Tutor> lister = new List<Tutor>();
            lister.Add(await db.GetTutorByID(AuthorizationPage.typeUserId));
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
            await Navigation.PushAsync(new UserUpdatePageTutor());
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