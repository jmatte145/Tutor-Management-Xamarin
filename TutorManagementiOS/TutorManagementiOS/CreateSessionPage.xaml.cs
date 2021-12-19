using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TutorManagementiOS.ViewModelsSession;

namespace TutorManagementiOS
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateSessionPage : ContentPage
    {
        FirebaseRepo db = new FirebaseRepo();
        //string temp = Course.Items[Course.SelectedIndex];
        public CreateSessionPage()
        {
            var vm = new ViewModelSession();
            this.BindingContext = vm;
            vm.DisplayInvalidLoginPrompt += () => DisplayAlert("Error", "Invalid Session, try again", "OK");

            InitializeComponent();
            displayItems();
            Date.Completed += (object sender, EventArgs e) =>
            {
                Time.Focus();
            };

            Time.Completed += (object sender, EventArgs e) =>
            {
                Duration.Focus();
            };

            Duration.Completed += (object sender, EventArgs e) =>
            {
                vm.SubmitCommand.Execute(null);
            };
        }

        async void displayItems()
        {
            List<CourseClass> listgood = new List<CourseClass>();
            List<CourseClass> lister = await db.GetAllCourses();
            if (lister.Any())
            {
                for (int i = 0; i < lister.Count; i++)
                {
                    var listing2 = await db.GetAllCoursesMembers();

                    for (int n = 0; n < listing2.Count; n++)
                    {
                        if (listing2.Any() & listing2[n].userID.Equals(ViewModels.LoginViewModel.currentUser))
                        {
                            listgood.Add(lister[i]);
                        }
                    }
                }
                Course.ItemsSource = lister;
            }
            else
            {
                _ = DisplayAlert("Error", "DB empty...", "ok");
            }

            string kek = Course.Items[Course.SelectedIndex];
        }
        public async static void test(string id)
        {
            FirebaseRepo db = new FirebaseRepo();
            List<SessionClass> lister = await db.GetAllSessions();
            for (int i = 0; i < lister.Count; i++)
            {
                if (lister[i].sessionID.Equals(id))
                {
                    lister[i].course= Course.Items[Course.SelectedIndex];
                }
            }
        }

        async void goLogin(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new LoginPage()); ;
        }

    }
}