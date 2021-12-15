using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TutorManagementiOS.ViewModelsSession;

namespace TutorManagementiOS
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateSessionPage : ContentPage
    {
        public CreateSessionPage()
        {
            var vm = new ViewModelSession();
            this.BindingContext = vm;
            vm.DisplayInvalidLoginPrompt += () => DisplayAlert("Error", "Invalid Session, try again", "OK");

            InitializeComponent();

            Duration.Completed += (object sender, EventArgs e) =>
            {
                vm.SubmitCommand.Execute(null);
            };
        }

        void OnDateSelected(object sender, EventArgs e)
        {
            Time.Focus();
        }
        async void goLogin(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new LoginPage()); ;
        }
       
    }
}