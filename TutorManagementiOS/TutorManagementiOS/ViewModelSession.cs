using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using TutorManagementiOS.Models;
using Xamarin.Forms;

namespace TutorManagementiOS.ViewModelsSession
{
    public class ViewModelSession : INotifyPropertyChanged
    {
        public Action DisplayInvalidLoginPrompt;
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        FirebaseRepo db = new FirebaseRepo();

        private string sessionMembers;
        public string SessionMembers
        {
            get { return sessionMembers; }
            set
            {
                sessionMembers = value;
                PropertyChanged(this, new PropertyChangedEventArgs("SessionMembers"));
            }
        }
        private string date;
        public string Date
        {
            get { return date; }
            set
            {
                date = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Date"));
            }
        }
        private string time;
        public string Time
        {
            get { return time; }
            set
            {
                time = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Time"));
            }
        }
        private string duration;
        public string Duration
        {
            get { return duration; }
            set
            {
                duration = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Duration"));
            }
        }
        private string report;
        public string Report
        {
            get { return report; }
            set
            {
                report = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Report"));
            }
        }
        private string grade;
        public string Grade
        {
            get { return grade; }
            set
            {
                grade = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Grade"));
            }
        }
     
        public ICommand SubmitCommand { protected set; get; }
        public ICommand UpdateCommand { protected set; get; }
        public ICommand UpdateTeacherCommand { protected set; get; }
        public ViewModelSession()
        {
            SubmitCommand = new Command(OnSubmit);
            UpdateCommand = new Command(UpdateSubmit);
            UpdateTeacherCommand = new Command(UpdateSubmitTeacher);
        }
        async void OnSubmit()
        {
            if (!(string.IsNullOrWhiteSpace(Date) || string.IsNullOrWhiteSpace(Time) || string.IsNullOrWhiteSpace(Duration)))
            {
                string temp = await db.SaveSession(new SessionClass
                {
                    tutorID = null,
                    sessionMembers = null,
                    date = date,
                    time = time,
                    duration = duration,
                    report = null,
                    grade = null,
                    open = true
                });

                nav();
            }
            else
                DisplayInvalidLoginPrompt();

        }
        async void UpdateSubmitTeacher()
        {
            if (!string.IsNullOrWhiteSpace(Grade))
            {
                List<SessionClass> list = new List<SessionClass>();
                list = await db.GetAllSessions();
                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i].sessionID.Equals(ViewSessionTeacherAccess.sessionID))
                    {
                        list[i].grade = grade;
                        await db.UpdateSession(list[i]);

                        navTeacher();
                    }
                }

                navTeacher();
            }
            else
                Console.WriteLine("hello2");
                DisplayInvalidLoginPrompt();

        }
        async void UpdateSubmit()
        {
            if (!(string.IsNullOrWhiteSpace(Date) || string.IsNullOrWhiteSpace(Time) || string.IsNullOrWhiteSpace(Duration)))
            {
                List<SessionClass> list = new List<SessionClass>();
                list = await db.GetAllSessions();
                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i].sessionID.Equals(ViewSessionPage.sessionID))
                    {
                        list[i].sessionMembers = sessionMembers;
                        list[i].date = date;
                        list[i].time = time;
                        list[i].duration = duration;
                        list[i].report = report;
                        await db.UpdateSession(list[i]);
                        
                        nav();
                    }
                }

                nav();
            }
            else
                DisplayInvalidLoginPrompt();

        }
        async void nav()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new HomeTutor());
        }
        async void navTeacher()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new HomeTeacher());
        }
    }
}
