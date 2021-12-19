using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TutorManagementiOS.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TutorManagementiOS
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StudentViewSession : ContentPage
    {
        FirebaseRepo db = new FirebaseRepo();
        public StudentViewSession()
        {
            InitializeComponent();
            displaySessions();
        }
        async void displaySessions()
        {
            List<SessionClass> list = await db.GetAllSessions();
            List<SessionClass> listgood = new List<SessionClass>();
            List<SessionMemberClass> lister = await db.GetAllSessionMembers();

            for (int i = 0; i < lister.Count; i++)
            {
                if (lister[i].userID.Equals(ViewModels.LoginViewModel.currentUser))
                {
                    for (int d = 0; d < list.Count; d++)
                    {
                        if (list[d].sessionID.Equals(lister[i].sessionID))
                        {
                            listgood.Add(list[d]);
                        }
                    }
                }
            }
            if (listgood.Any())
            {
                collectionView.ItemsSource = listgood;
            }
            else
            {
                _ = DisplayAlert("Error", "DB empty...", "ok");
            }
        }

    }
}