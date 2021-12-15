using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace TutorManagementiOS
{
    public class ViewModelsUpdateCourse : INotifyPropertyChanged
    {
        public Action DisplayInvalidLoginPrompt;
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        private string cname;
        FirebaseRepo db = new FirebaseRepo();
        public string cName
        {
            get { return cname; }
            set
            {
                cname = value;
                PropertyChanged(this, new PropertyChangedEventArgs("cName"));
            }
        }
        private string cnumber;
        public string cNumber
        {
            get { return cnumber; }
            set
            {
                cnumber = value;
                PropertyChanged(this, new PropertyChangedEventArgs("cNumber"));
            }
        }
        
        public ICommand SubmitCommand { protected set; get; }
        public ViewModelsUpdateCourse()
        {
            SubmitCommand = new Command(OnSubmit);
        }
        public void OnSubmit()
        {
            if (!(string.IsNullOrWhiteSpace(cName) || string.IsNullOrWhiteSpace(cNumber) ))
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
                if (list[i].courseID.Equals(CourseManagementPage.courseID))
                {
                    list[i].courseName = cName;
                    list[i].courseNumber = cNumber;
                    await db.UpdateCourse(list[i]);
                }
            }
            await Application.Current.MainPage.Navigation.PushAsync(new CourseManagementPage());
        }

    }
}

