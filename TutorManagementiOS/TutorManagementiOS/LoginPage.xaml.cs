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
    public partial class LoginPage : ContentPage
    {
        FirebaseRepo userRepo = new FirebaseRepo();
        public LoginPage()
        {
            InitializeComponent();
        }

        private async void btnRegister_Clicked(object sender, EventArgs e)
        {
           await Navigation.PushAsync(new Register());
        }

        private async void btnLogin_Clicked(object sender, EventArgs e)
        {
            List<User> users = await userRepo.GetAllUsers();

            for (int u = 0; u < users.Count; u++)
            {
                if (users[u].userName == txtUserName.Text && users[u].password == txtPassword.Text)
                {
                    await Navigation.PushAsync(new MainPage(users[u].userType));
                }
            }
        }
    }
}