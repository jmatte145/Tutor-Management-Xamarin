using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TutorManagementiOS.ViewModelsRegister;

namespace TutorManagementiOS
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Register : ContentPage
    {
        public Register()
        {
            var vm = new LoginViewModelRegister();
            this.BindingContext = vm;
            vm.DisplayInvalidLoginPrompt += () => DisplayAlert("Error", "Invalid Register, try again", "OK");
            InitializeComponent();

            Fname.Completed += (object sender, EventArgs e) =>
            {
                Lname.Focus();
            };

            Lname.Completed += (object sender, EventArgs e) =>
            {
                Email.Focus();
            };

            Email.Completed += (object sender, EventArgs e) =>
            {
                User.Focus();
            };

            User.Completed += (object sender, EventArgs e) =>
            {
                Password.Focus();
            };

            Password.Completed += (object sender, EventArgs e) =>
            {
                vm.SubmitCommand.Execute(null);
            };
        }
        async void navRegister(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new LoginPage()); ;
        }


    }
}