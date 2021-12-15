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
    public partial class ViewStudents : ContentPage
    {
        public static string genUserID;
        public static string typeUserId;
        FirebaseRepo db = new FirebaseRepo();
        public ViewStudents()
        {
            InitializeComponent();
            displayUsers();
        }
        async void displayUsers()
        {
            List<Student> list = await db.GetAllStudents();
            List<UserClass> list2 = await db.GetAllUsers();

            if (list.Any())
            {
                List<UserClass> list3 = new List<UserClass>();
                for (int i = 0; i < list.Count; i++)
                {
                    for(int y=0; y <list2.Count; y++)
                    {
                        if (list2[y].userID == list[i].genUserID)
                        {
                            list3.Add(list2[y]);
                        }
                    }
                    
                }
                collectionView.ItemsSource = list3.ToList<UserClass>();
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
            List<Student> list = await db.GetAllStudents();

            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].genUserID.Equals(genUserID))
                {
                    navstudent();          
                }
            }


        }
        async void navstudent()
        {
            await Navigation.PushAsync(new UserDetailPageStudentTeacher()); ;

        }
  
    }
}
