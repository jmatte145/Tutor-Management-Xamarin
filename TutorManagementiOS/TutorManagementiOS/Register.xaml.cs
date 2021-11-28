using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TutorManagementiOS
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Register : ContentPage
    {
        FirebaseRepo repository = new FirebaseRepo();
        string userRole = string.Empty;

        public Register()
        {
            InitializeComponent();
        }

        private async void btnRegister_Clicked(object sender, EventArgs e)
        {
            string fName = txtFName.Text;
            string lName = txtLName.Text;
            string email = txtEmail.Text;
            string userName = txtUserName.Text;
            string passWord = txtPassword.Text;

            if (string.IsNullOrEmpty(fName))
            {
                await DisplayAlert("Required", "Enter First Name", "Cancel");
            }
            if (string.IsNullOrEmpty(lName))
            {
                await DisplayAlert("Required", "Enter Last Name", "Cancel");
            }
            if (string.IsNullOrEmpty(email))
            {
                await DisplayAlert("Required", "Enter Email Address", "Cancel");
            }
            if (string.IsNullOrEmpty(userName))
            {
                await DisplayAlert("Required", "Enter Username", "Cancel");
            }
            if (string.IsNullOrEmpty(passWord))
            {
                await DisplayAlert("Required", "Enter Password", "Cancel");
            }
            if (string.IsNullOrEmpty(userRole))
            {
                await DisplayAlert("Required", "Enter User Role", "Cancel");
            }

            if(!string.IsNullOrEmpty(fName) && !string.IsNullOrEmpty(lName) && !string.IsNullOrEmpty(email) 
                && !string.IsNullOrEmpty(userName) && !string.IsNullOrEmpty(passWord) && !string.IsNullOrEmpty(userRole))
            {
                User user = new User();
                user.firstName = fName;
                user.lastName = lName;
                user.email = email;
                user.userName = userName;
                user.password = passWord;
                user.userType = userRole;
                user.approvalStatus = false;

                var isSaved = await repository.SaveUser(user);
                if (isSaved)
                {
                    await DisplayAlert("Success", "User has been successfully saved!", "Ok");
                }
                else
                {
                    await DisplayAlert("Failed", "User was not saved. ", "Ok");
                }
                ClearUser();
            }
            
        }

        private void ClearUser()
        {
            txtFName.Text = string.Empty;
            txtLName.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtUserName.Text = string.Empty;
            txtPassword.Text = string.Empty;
            userRole = string.Empty;
        }

        private void Roles_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            RadioButton rb = (RadioButton)sender;

            userRole = rb.ContentAsString();
        }
    }
}