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
    public partial class UpdateSessionPageTeacher : ContentPage
    {
        public UpdateSessionPageTeacher()
        {
            var vm = new ViewModelSession();
            this.BindingContext = vm;
            vm.DisplayInvalidLoginPrompt += () => DisplayAlert("Error", "Invalid Session, try again", "OK");

            InitializeComponent();
            Grade.Completed += (object sender, EventArgs e) =>
            {
                vm.UpdateTeacherCommand.Execute(null);
            };

        }

        async void goLogin(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new LoginPage()); ;
        }
    }
}

