using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Threading.Tasks;
using System;
using TutorManagementiOS.Models;
using System.Collections.Generic;
namespace TutorManagementiOS
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserUpdatePageStudentTeacher : ContentPage
    {
        FirebaseRepo db = new FirebaseRepo();
        public UserUpdatePageStudentTeacher()
        {
            var vm = new ViewModelUpdateStudent();
            this.BindingContext = vm;
            vm.DisplayInvalidLoginPrompt += () => DisplayAlert("Error", "Invalid Register, try again", "OK");
            InitializeComponent();
            _ = setOldValAsync();

            Fname.Completed += (object sender, EventArgs e) =>
            {
                Lname.Focus();
            };

            Lname.Completed += (object sender, EventArgs e) =>
            {
                Email.Focus();
            };

            Email.Completed += (object sender, EventArgs e) =>
            {
                User.Focus();
            };

            User.Completed += (object sender, EventArgs e) =>
            {
                Password.Focus();
            };

            Password.Completed += (object sender, EventArgs e) =>
            {
                TotalVisits.Focus();
            };

            TotalVisits.Completed += (object sender, EventArgs e) =>
            {
                TotalHours.Focus();
            };

            TotalHours.Completed += (object sender, EventArgs e) =>
            {
                vm.SubmitTeacherCommand.Execute(null);
            };
        }

        async Task setOldValAsync()
        {
            var list = await db.GetByUserId(ViewStudents.genUserID);
            Fname.Text = list.firstName;
            Lname.Text = list.lastName;
            Email.Text = list.email;
            User.Text = list.userName;
            Password.Text = list.password;
            var lister = await db.GetStudentByID(ViewStudents.genUserID);
            TotalVisits.Text = lister.totalVisits;
            TotalHours.Text = lister.totalHours;
        }

        async void nav(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new HomeTeacher());
        }

    }
}