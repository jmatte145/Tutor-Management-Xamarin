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
        //private string course = CreateSessionPage.test();

        public ICommand SubmitCommand { protected set; get; }
        public ICommand UpdateCommand { protected set; get; }
        public ICommand UpdateTeacherCommand { protected set; get; }
        public ViewModelSession()
        {
            SubmitCommand = new Command(OnSubmit);
            //UpdateCommand = new Command(UpdateSubmit);
            //UpdateTeacherCommand = new Command(UpdateSubmitTeacher);
        }
        async void OnSubmit()
        {
            if (!(string.IsNullOrWhiteSpace(Date) || string.IsNullOrWhiteSpace(Time) || string.IsNullOrWhiteSpace(Duration)))
            {
                string temp = await db.SaveSession(new SessionClass
                {
                    tutorID = ViewModels.LoginViewModel.currentUser,
                    course = "idk how",
                    date = date,
                    time = time,
                    duration = duration,
                    report = "no report",
                    grade = "not graded",
                    completed = false
                });
                List<SessionClass> lister = await db.GetAllSessions();
                string currentSession = "";
                for (int i = 0; i < lister.Count; i++)
                {
                    if (lister[i].date.Equals(date) & lister[i].time.Equals(time) & lister[i].duration.Equals(duration))
                    {
                        currentSession = lister[i].sessionID;
                    }
                }
                    CreateSessionPage.test(currentSession);
                nav();
            }
            else
                DisplayInvalidLoginPrompt();

        }
        //async void UpdateSubmitTeacher()
        //{
        //    if (!string.IsNullOrWhiteSpace(Grade))
        //    {
        //        List<SessionClass> list = new List<SessionClass>();
        //        list = await db.GetAllSessions();
        //        for (int i = 0; i < list.Count; i++)
        //        {
        //            if (list[i].sessionID.Equals(ViewSessionTeacherAccess.sessionID))
        //            {
        //                list[i].grade = grade;
        //                await db.UpdateSession(list[i]);

        //                navTeacher();
        //            }
        //        }

        //        navTeacher();
        //    }
        //    else
        //        Console.WriteLine("hello2");

        //}
        //async void UpdateSubmit()
        //{
        //    if (!(string.IsNullOrWhiteSpace(Date) || string.IsNullOrWhiteSpace(Time) || string.IsNullOrWhiteSpace(Duration)))
        //    {
        //        List<SessionClass> list = new List<SessionClass>();
        //        list = await db.GetAllSessions();
        //        for (int i = 0; i < list.Count; i++)
        //        {
        //            if (list[i].sessionID.Equals(ViewSessionPage.sessionID))
        //            {
        //                list[i].sessionMembers = sessionMembers;
        //                list[i].date = date;
        //                list[i].time = time;
        //                list[i].duration = duration;
        //                list[i].report = report;
        //                await db.UpdateSession(list[i]);
                        
        //                nav();
        //            }
        //        }

        //        nav();
        //    }
        //    else
        //        DisplayInvalidLoginPrompt();

        //}
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
