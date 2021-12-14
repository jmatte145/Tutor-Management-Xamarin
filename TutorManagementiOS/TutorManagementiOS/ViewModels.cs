using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;
namespace TutorManagementiOS.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        public static string currentUser = "";
        public Action DisplayInvalidLoginPrompt;

        //removed later
        public Action DisplayValidLoginPrompt;

        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        private string user;
        FirebaseRepo db = new FirebaseRepo();
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
        public ICommand SubmitCommand { protected set; get; }
        public LoginViewModel()
        {
            SubmitCommand = new Command(OnSubmit);
        }

        List<UserClass> list = new List<UserClass>();
        public async void OnSubmit()
        {

            list = await db.GetAllUsers();
            bool success = false;
            if (user == "admin" && password == "secret")
            {
                navAdmin();
                DisplayValidLoginPrompt();
            }
            else
            {
                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i].userName.Equals(user) & list[i].password.Equals(password) & list[i].approvalStatus == true)
                    {
                        Console.WriteLine("hello");
                        var listing1 = await db.GetAllStudents();

                        for (int a = 0; a < listing1.Count; a++)
                        {
                            if (listing1.Any() & listing1[a].genUserID.Equals(list[i].userID))
                            {
                                currentUser = list[i].userID;
                                //nav student home page
                                nav();
                                //DisplayValidLoginPrompt();
                            }
                        }
                        var listing2 = await db.GetAllTutors();

                        for (int n = 0; n < listing2.Count; n++)
                        {
                            if (listing2.Any() & listing2[n].genUserID.Equals(list[i].userID))
                            {
                                currentUser = list[i].userID;
                                //nav tutor home page
                                navTutor();
                                //DisplayValidLoginPrompt();
                            }
                        }
                        var listing3 = await db.GetAllTeachers();

                        for (int m = 0; m < listing3.Count; m++)
                        {
                            if (listing3.Any() & listing3[m].genUserID.Equals(list[i].userID))
                            {
                                currentUser = list[i].userID;
                                //nav student home page
                                navTeacher();
                                //DisplayValidLoginPrompt();
                            }
                        }
                        success = true;
                    }
                }
                if (!success)
                {
                    DisplayInvalidLoginPrompt();
                }

            }
        }


        async void nav()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new HomeStudent());
        }
        async void navTutor()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new HomeTutor());
        }
        async void navTeacher()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new HomeTeacher());
        }
        async void navAdmin()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new HomeAdmin());
        }
    }
}