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
    public partial class SessionGradingPage : ContentPage
    {
        FirebaseRepo db = new FirebaseRepo();
        public SessionGradingPage()
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
        public void btnOpen_Clicked(object sender, EventArgs e)
        {
            updateSession();
        }

        async void updateSession()
        {
            string temp = NewGrade.Text;

            List<SessionClass> list = await db.GetAllSessions();
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].sessionID.Equals(ViewSessionTeacherAccess.sessionIDforteacher))
                {
                    list[i].grade = temp;
                    await db.UpdateSession(list[i]);
                    nav();
                }
            }
        }

        void goHome(object sender, EventArgs e)
        {
            nav();
        }

        async void nav()
        {
            await Navigation.PushAsync(new HomeTeacher());
        }

    }
}