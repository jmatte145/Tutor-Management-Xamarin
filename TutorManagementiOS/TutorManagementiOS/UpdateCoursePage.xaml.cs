using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Threading.Tasks;
using System;
using TutorManagementiOS.Models;
using System.Collections.Generic;

namespace TutorManagementiOS
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UpdateCoursePage : ContentPage
    {
        FirebaseRepo db = new FirebaseRepo();
        public UpdateCoursePage()
        {
            var vm = new ViewModelUpdateCourse();
            this.BindingContext = vm;
            vm.DisplayInvalidLoginPrompt += () => DisplayAlert("Error", "Invalid Register, try again", "OK");
            InitializeComponent();
            _ = setOldValAsync();

            CourseName.Completed += (object sender, EventArgs e) =>
            {
                CourseNumber.Focus();
            };

            CourseNumber.Completed += (object sender, EventArgs e) =>
            {
                vm.SubmitCommand.Execute(null);
            };
        }

        async Task setOldValAsync()
        {
            var list = await db.GetCourseByID(CourseManagementPage.courseId);
            CourseName.Text = list.courseName;
            CourseNumber.Text = list.courseNumber;
        }

        async void nav(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new HomeAdmin());
        }

    }
}