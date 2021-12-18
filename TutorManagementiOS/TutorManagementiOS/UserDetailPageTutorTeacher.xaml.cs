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
            list.Add(await db.GetByUserId(ViewTutors.genUserID));
            collectionView.ItemsSource = list;

            List<Tutor> lister = new List<Tutor>();
            lister.Add(await db.GetTutorByID(ViewTutors.typeUserId));
            collectionView2.ItemsSource = lister;

        }

        void goHome(object sender, EventArgs e)
        {
            nav();
        }

        async void nav()
        {
            await Navigation.PushAsync(new HomeTutor());
        }
    }
}