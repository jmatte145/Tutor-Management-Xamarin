using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TutorManagementiOS.ViewModelsSession;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TutorManagementiOS
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UpdateSessionPage : ContentPage
    {
        public UpdateSessionPage()
        {
            var vm = new ViewModelSession();
            this.BindingContext = vm;
            vm.DisplayInvalidLoginPrompt += () => DisplayAlert("Error", "Invalid Session, try again", "OK");

            InitializeComponent();
            SessionMembers.Completed += (object sender, EventArgs e) =>
            {
                Date.Focus();
            };
            Date.Completed += (object sender, EventArgs e) =>
            {
                Time.Focus();
            };

            Time.Completed += (object sender, EventArgs e) =>
            {
                Duration.Focus();
            };

            Duration.Completed += (object sender, EventArgs e) =>
            {
                Report.Focus();
            };
            Report.Completed += (object sender, EventArgs e) =>
            {
                Grade.Focus();
            };
            Grade.Completed += (object sender, EventArgs e) =>
            {
                vm.UpdateCommand.Execute(null);
            };

        }

        async void goLogin(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new LoginPage()); ;
        }
    }
}

