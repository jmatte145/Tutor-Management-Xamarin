using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace TutorManagementiOS
{
    public partial class ReportSessionPage : ContentPage
    {
        FirebaseRepo db = new FirebaseRepo();
        public ReportSessionPage()
        {
            InitializeComponent();
        }
        async void report(object sender, EventArgs e)
        {
            string report = Report.Text;
            if (!(report==""))
            {
                List<SessionClass> list = new List<SessionClass>();
                list = await db.GetAllSessions();
                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i].sessionID.Equals(ViewSessionPage.sessionID) & list[i].completed == false)
                    {
                        list[i].report = report;
                        list[i].completed = true;
                        await db.UpdateSession(list[i]);

                        nav();
                    }
                    else
                    {
                        _ = DisplayAlert("Failed", "Session already closed", "Ok");
                    }
                }

                nav();
            }
            else
                _ = DisplayAlert("Failed", "Please fill report", "Ok");

        }
        void goHome(object sender, EventArgs e)
        {
            nav();
        }

        async void nav()
        {
            await Navigation.PushAsync(new HomeTutor());
        }
    }
}
