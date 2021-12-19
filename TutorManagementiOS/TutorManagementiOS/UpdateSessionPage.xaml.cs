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

        FirebaseRepo db = new FirebaseRepo();
        public UpdateSessionPage()
        {
            var vm = new ViewModelSession();
            this.BindingContext = vm;
            vm.DisplayInvalidLoginPrompt += () => DisplayAlert("Error", "Session already closed", "OK");

            InitializeComponent();
            _ = setOldValAsync();

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
                vm.UpdateCommand.Execute(null);
            };

        }


        async Task setOldValAsync()
        {
            var list = await db.GetSessionByID(ViewSessionPage.sessionID);
            Duration.Text = list.duration;
            Time.Text = list.time;
            Date.Text = list.date;
        }

        async void goLogin(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new HomeTutor()); ;
        }
    }
}

