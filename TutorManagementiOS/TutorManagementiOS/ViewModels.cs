using System;
using System.Collections.Generic;
using System.ComponentModel;
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
            if (user == "admin" || password == "secret")
            {
                //navAdmin();
                DisplayValidLoginPrompt();
            }
            else
            {
                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i].userName.Equals(user) & list[i].password.Equals(password))
                    {
                        /*if (list[i].userType.Equals("Student"))
                        {
                            currentUser=list[i].userID;
                            //nav();
                            DisplayValidLoginPrompt();
                        }
                        if (list[i].userType.Equals("Tutor"))
                        {
                            currentUser = list[i].userID;
                            //nav();
                            DisplayValidLoginPrompt();
                        }
                        if (list[i].userType.Equals("Teacher"))
                        {
                            currentUser = list[i].userID;
                            //nav();
                            DisplayValidLoginPrompt();
                        }*/

                        success = true;
                    }
                }
                if (!success)
                {
                    DisplayInvalidLoginPrompt();
                }

            }
        }


        //async void nav()
        //{
        //    await Application.Current.MainPage.Navigation.PushAsync(new Home());
        //}
        //async void navAdmin()
        //{
        //    await Application.Current.MainPage.Navigation.PushAsync(new HomeAdministration());
        //}
    }
}