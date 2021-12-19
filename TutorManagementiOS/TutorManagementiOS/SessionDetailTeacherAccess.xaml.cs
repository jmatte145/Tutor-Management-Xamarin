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
    public partial class SessionDetailTeacherAccess : ContentPage
    {
        FirebaseRepo db = new FirebaseRepo();
        public SessionDetailTeacherAccess()
        {
            InitializeComponent();
            displayUser();
        }
        async void displayUser()
        {
            List<SessionClass> list = new List<SessionClass>();
            list.Add(await db.GetSessionByID(ViewSessionTeacherAccess.sessionIDforteacher));
            collectionView.ItemsSource = list;

        }
        async void btnOpen_Clicked(object sender, EventArgs e)
        {
            List<SessionClass> list = new List<SessionClass>();
            list = await db.GetAllSessions();
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].sessionID.Equals(ViewSessionTeacherAccess.sessionIDforteacher))
                {
                    list[i].completed = !list[i].completed;
                    await db.UpdateSession(list[i]);
                    _ = DisplayAlert("Status Updated", "The status of the session is now: " + list[i].completed, "ok");
                    nav();
                }
            }
        }
        async void btnUpdateRecord_Clicked(object sender, EventArgs e)
        {
            SessionClass session1 = await db.GetSessionByID(ViewSessionTeacherAccess.sessionIDforteacher);
            await db.UpdateSession(session1);
            navUpdate();
        }
        async void btnDeleteRecord_Clicked(object sender, EventArgs e)
        {
            SessionClass session1 = await db.GetSessionByID(ViewSessionTeacherAccess.sessionIDforteacher);
            await db.DeleteSession(session1);
            nav();
        }

        void goHome(object sender, EventArgs e)
        {
            nav();
        }

        async void nav()
        {
            await Navigation.PushAsync(new HomeTeacher());
        }
        async void navUpdate()
        {
            await Navigation.PushAsync(new UpdateSessionPageTeacher());
        }
    }
}