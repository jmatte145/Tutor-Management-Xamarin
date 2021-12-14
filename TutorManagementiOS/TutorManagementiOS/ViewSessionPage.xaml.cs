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
    public partial class ViewSessionPage : ContentPage
    {
        public static string sessionID;
        FirebaseRepo db = new FirebaseRepo();
        public ViewSessionPage()
        {
            InitializeComponent();
            displaySessions();
        }
        async void displaySessions()
        {
            List<SessionClass> list = await db.GetAllSessions();
            if (list.Any())
            {
                collectionView.ItemsSource = await db.GetAllSessions();
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
            await Navigation.PushAsync(new SessionDetailPage()); ;
        }

    }
}