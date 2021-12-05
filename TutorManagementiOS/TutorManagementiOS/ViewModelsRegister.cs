using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using TutorManagementiOS.Models;
using Xamarin.Forms;
namespace TutorManagementiOS.ViewModelsRegister
{
    public class LoginViewModelRegister : INotifyPropertyChanged
    {
        public Action DisplayInvalidLoginPrompt;
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        FirebaseRepo db = new FirebaseRepo();

        private string fname;
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
        private string role;
        public string Role
        {
            get { return role; }
            set
            {
                role = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Role"));
            }
        }
        public ICommand SubmitCommand { protected set; get; }
        public LoginViewModelRegister()
        {
            SubmitCommand = new Command(OnSubmit);
        }
        public void OnSubmit()
        {
            if (!(string.IsNullOrWhiteSpace(User) || string.IsNullOrWhiteSpace(Password) || string.IsNullOrWhiteSpace(Fname) || string.IsNullOrWhiteSpace(Lname) || string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Role)))
            {
                string savedRole = role;
                string savedName = user;
                string savedPass = password;
                registerUser();
                Console.WriteLine(savedName);
                registerUserType(savedRole, savedName, savedPass);
                nav();
            }
            else
                DisplayInvalidLoginPrompt();

        }

        async void registerUserType(string savedRole, string savedName, string savedPass)
        {
            List<UserClass> list = new List<UserClass>();
            list = await db.GetAllUsers();
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].userName.Equals(savedName) & list[i].password.Equals(savedPass))
                {
                    if (savedRole.Equals("Student"))
                    {
                        registerStudent(list[i].userID);
                    }
                    if (savedRole.Equals("Tutor"))
                    {
                        registerTutor(list[i].userID);
                    }
                    if (savedRole.Equals("Teacher"))
                    {
                        registerTeacher(list[i].userID);
                    }
                }
            }
        }

        async void nav()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new LoginPage());
        }
        async void registerUser()
        {
            await db.SaveUser(new UserClass
            {
                firstName = fname,
                lastName = lname,
                email = email,
                userName = user,
                password = password,
                approvalStatus = false,
            });
        }

        async void registerStudent(string id)
        {
            await db.SaveStudent(new Student
            {
                userID = id
            });
        }

        async void registerTutor(string id)
        {
            await db.SaveTutor(new Tutor
            {
                userID = id
            });
        }

        async void registerTeacher(string id)
        {
            await db.SaveTeacher(new Teacher
            {
                userID = id
            });
        }
    }
}