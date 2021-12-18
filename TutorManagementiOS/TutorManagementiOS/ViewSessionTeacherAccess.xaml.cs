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
    public partial class ViewSessionTeacherAccess : ContentPage
    {
        public static string sessionID;
        FirebaseRepo db = new FirebaseRepo();
        public ViewSessionTeacherAccess()
        {
            InitializeComponent();
            displaySessions();
        }
        async void displaySessions()
        {
            List<SessionClass> list = await db.GetAllSessions();
            List<SessionClass> filtered = new List<SessionClass>();
            for (int i = 0; i < list.Count; i++)
            {
                if(list[i].open == false)
                {
                    filtered.Add(list[i]);
                }
            }
            
            if (filtered.Any())
            {
                collectionView.ItemsSource = filtered;
            }
            else
            {
                _ = DisplayAlert("Error", "DB empty...", "ok");
            }
        }
        async void goDetail(object sender, CheckedChangedEventArgs e)
        {
            var radiobtn = sender as RadioButton;
            sessionID = (string)radiobtn.Content;
            List<SessionClass> list = await db.GetAllSessions();

            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].sessionID.Equals(sessionID))
                {

                    nav();

                }
            }


        }
        async void nav()
        {
            await Navigation.PushAsync(new SessionDetailTeacherAccess()); ;
        }

    }
}