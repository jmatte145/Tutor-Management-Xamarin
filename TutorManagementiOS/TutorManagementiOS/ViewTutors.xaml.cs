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
    public partial class ViewTutors : ContentPage
    {
        public static string genUserID;
        public static string typeUserId;
        FirebaseRepo db = new FirebaseRepo();
        public ViewTutors()
        {
            InitializeComponent();
            displayUsers();
        }
        async void displayUsers()
        {
            List<Tutor> list = await db.GetAllTutors();
            List<UserClass> list2 = await db.GetAllUsers();
            List<UserClass> list3 = new List<UserClass>();
            List<Tutor> list4 = new List<Tutor>();
            List<TutorAvailablity> list5 = new List<TutorAvailablity>();
            if (list.Any())
            {
                for (int i = 0; i < list.Count; i++)
                {
                    for (int y = 0; y < list2.Count; y++)
                    {
                        if (list2[y].userID == list[i].genUserID)
                        {
                            list3.Add(list2[y]);
                            list4.Add(list[i]);
                        }
                    }

                }
                for (int i = 0; i < list3.Count; i++)
                {
                    TutorAvailablity temp = new TutorAvailablity();
                    temp.firstName = list3[i].firstName;
                    temp.lastName = list3[i].lastName;
                    temp.userName = list3[i].userName;
                    temp.availableStart = list4[i].availableStart;
                    temp.availableEnd = list4[i].availableEnd;
                    list5.Add(temp);
                }
                collectionView.ItemsSource = list5;
            }
            else
            {
                _ = DisplayAlert("Error", "DB empty...", "ok");
            }
        }
        async void goDetail(object sender, EventArgs e)
        {
            var button = sender as Button;
            genUserID = (string)button.Text;
            List<Tutor> list = await db.GetAllTutors();

            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].genUserID.Equals(genUserID))
                {
                    typeUserId = list[i].tutorID;
                    navtutor();
                }
            }


        }
        async void navtutor()
        {
            await Navigation.PushAsync(new UserDetailPageTutorTeacher()); ;

        }

    }
}