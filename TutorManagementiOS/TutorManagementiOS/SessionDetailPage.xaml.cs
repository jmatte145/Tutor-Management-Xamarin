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
    public partial class SessionDetailPage : ContentPage
    {
        FirebaseRepo db = new FirebaseRepo();
        public SessionDetailPage()
        {
            InitializeComponent();
            displayUser();
        }
        async void displayUser()
        {
            List<SessionClass> list = new List<SessionClass>();
            list.Add(await db.GetSessionByID(ViewSessionPage.sessionID));
            collectionView.ItemsSource = list;

        }
        public void btnOpen_Clicked(object sender, EventArgs e)
        {
            navReport();
        }
        public void btnUpdateRecord_Clicked(object sender, EventArgs e)
        {
            navUpdate();
        }
        async void btnDeleteRecord_Clicked(object sender, EventArgs e)
        {
            SessionClass session1 = await db.GetSessionByID(ViewSessionPage.sessionID);
            await db.DeleteSession(session1);
            nav();
        }

        void goHome(object sender, EventArgs e)
        {
            nav();
        }

        async void nav()
        {
            await Navigation.PushAsync(new HomeTutor());
        }
        async void navUpdate()
        {
            await Navigation.PushAsync(new UpdateSessionPage());
        }
        async void navReport()
        {
            await Navigation.PushAsync(new ReportSessionPage());
        }
    }
}