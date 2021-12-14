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
            if (list.Any())
            {
                collectionView.ItemsSource = await db.GetAllStudents();
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
            await Navigation.PushAsync(new UserDetailPageStudent()); ;

        }
  
    }
}
