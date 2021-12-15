using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using TutorManagementiOS.Models;
using Xamarin.Forms;
namespace TutorManagementiOS
{
    public class ViewModelUpdateStudent : INotifyPropertyChanged
    {
        public Action DisplayInvalidLoginPrompt;
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        private string fname;
        FirebaseRepo db = new FirebaseRepo();
        public string Fname
        {
            get { return fname; }
            set
            {
                fname = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Fname"));
            }
        }
        private string lname;
        public string Lname
        {
            get { return lname; }
            set
            {
                lname = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Lname"));
            }
        }
        private string email;
        public string Email
        {
            get { return email; }
            set
            {
                email = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Email"));
            }
        }
        private string user;
        public string User
        {
            get { return user; }
            set
            {
                user = value;
                PropertyChanged(this, new PropertyChangedEventArgs("User"));
            }
        }
        private string password;
        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Password"));
            }
        }
        private string totalVisits;
        public string TotalVisits
        {
            get { return totalVisits; }
            set
            {
                totalVisits = value;
                PropertyChanged(this, new PropertyChangedEventArgs("TotalVisits"));
            }
        }
        private string totalHours;
        public string TotalHours
        {
            get { return totalHours; }
            set
            {
                totalHours = value;
                PropertyChanged(this, new PropertyChangedEventArgs("TotalHours"));
            }
        }
        public ICommand SubmitCommand { protected set; get; }
        public ICommand SubmitTeacherCommand { protected set; get; }
        
        public ViewModelUpdateStudent()
        {
            SubmitCommand = new Command(OnSubmit);
            SubmitTeacherCommand = new Command(OnTeacherSubmit);
        }
        public void OnSubmit()
        {
            if (!(string.IsNullOrWhiteSpace(User) || string.IsNullOrWhiteSpace(Password) || string.IsNullOrWhiteSpace(Fname) || string.IsNullOrWhiteSpace(Lname) || string.IsNullOrWhiteSpace(Email)))
            {
                updateUser();
            }
            else
                DisplayInvalidLoginPrompt();

        }
        public void OnTeacherSubmit()
        {
            if (!(string.IsNullOrWhiteSpace(User) || string.IsNullOrWhiteSpace(Password) || string.IsNullOrWhiteSpace(Fname) || string.IsNullOrWhiteSpace(Lname) || string.IsNullOrWhiteSpace(Email)))
            {
                updateTeacherUser();
            }
            else
                DisplayInvalidLoginPrompt();

        }
        async void updateTeacherUser()
        {
            List<UserClass> list = await db.GetAllUsers();
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].userID.Equals(ViewStudents.genUserID))
                {
                    list[i].firstName = fname;
                    list[i].lastName = lname;
                    list[i].email = email;
                    list[i].userName = user;
                    list[i].password = password;
                    await db.UpdateUser(list[i]);
                }
            }
            List<Student> lister = await db.GetAllStudents();
            Console.WriteLine(AuthorizationPage.typeUserId);
            for (int n = 0; n < lister.Count; n++)
            {
                if (lister[n].studentID.Equals(ViewStudents.genUserID))
                {
                    if (string.IsNullOrWhiteSpace(TotalVisits))
                        lister[n].totalVisits = null;
                    else
                        lister[n].totalVisits = totalVisits;
                    if (string.IsNullOrWhiteSpace(totalHours))
                        lister[n].totalHours = null;
                    else
                        lister[n].totalHours = totalHours;
                    await db.UpdateStudent(lister[n]);
                }
            }
            await Application.Current.MainPage.Navigation.PushAsync(new UserDetailPageStudentTeacher());
        }
        async void updateUser()
        {
            List<UserClass> list = await db.GetAllUsers();
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].userID.Equals(AuthorizationPage.userId))
                {
                    list[i].firstName = fname;
                    list[i].lastName = lname;
                    list[i].email = email;
                    list[i].userName = user;
                    list[i].password = password;
                    await db.UpdateUser(list[i]);
                }
            }
            List<Student> lister = await db.GetAllStudents();
            Console.WriteLine(AuthorizationPage.typeUserId);
            for (int n = 0; n < lister.Count; n++)
            {
                if (lister[n].studentID.Equals(AuthorizationPage.typeUserId))
                {
                    if (string.IsNullOrWhiteSpace(TotalVisits))
                        lister[n].totalVisits = null;
                    else
                        lister[n].totalVisits = totalVisits;
                    if (string.IsNullOrWhiteSpace(totalHours))
                        lister[n].totalHours = null;
                    else
                        lister[n].totalHours = totalHours;
                    await db.UpdateStudent(lister[n]);
                }
            }
            await Application.Current.MainPage.Navigation.PushAsync(new UserDetailPageStudent());
        }
        
    }
}