using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using TutorManagementiOS.Models;
using Xamarin.Forms;
namespace TutorManagementiOS
{
    public class ViewModelUpdateCourse : INotifyPropertyChanged
    {
        public Action DisplayInvalidLoginPrompt;
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        FirebaseRepo db = new FirebaseRepo();
        
        private string courseName;
        public string CourseName
        {
            get { return courseName; }
            set
            {
                courseName = value;
                PropertyChanged(this, new PropertyChangedEventArgs("CourseName"));
            }
        }
        private string courseNumber;
        public string CourseNumber
        {
            get { return courseNumber; }
            set
            {
                courseNumber = value;
                PropertyChanged(this, new PropertyChangedEventArgs("CourseNumber"));
            }
        }
        public ICommand SubmitCommand { protected set; get; }

        public ViewModelUpdateCourse()
        {
            SubmitCommand = new Command(OnSubmit);
        }
        public void OnSubmit()
        {
            if (!(string.IsNullOrWhiteSpace(CourseName) || string.IsNullOrWhiteSpace(CourseNumber)))
            {
                updateCourse();
            }
            else
                DisplayInvalidLoginPrompt();

        }
        
        async void updateCourse()
        {
            List<CourseClass> list = await db.GetAllCourses();
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].courseID.Equals(CourseManagementPage.courseId))
                {
                    list[i].courseName = courseName;
                    list[i].courseNumber = courseNumber;
                    await db.UpdateCourse(list[i]);
                }
            }
            await Application.Current.MainPage.Navigation.PushAsync(new HomeAdmin());
        }

    }
}