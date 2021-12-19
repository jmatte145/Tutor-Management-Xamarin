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
    public partial class ModAvailPage : ContentPage
    {
        FirebaseRepo db = new FirebaseRepo();
        string timeStart = "";
        string timeEnd = "";

        public ModAvailPage()
        {
            InitializeComponent();
        }
        async void save(object sender, EventArgs e)
        {
            timeStart = DateTime.Today.Add(StartTime.Time).ToString(StartTime.Format);
            timeEnd = DateTime.Today.Add(EndTime.Time).ToString(EndTime.Format);
            List<Tutor> list = new List<Tutor>();
            list = await db.GetAllTutors();
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].genUserID.Equals(ViewModels.LoginViewModel.currentUser))
                {
                    list[i].availableStart = timeStart;
                    list[i].availableEnd = timeEnd;
                    await db.UpdateTutor(list[i]);

                    await Navigation.PushAsync(new HomeTutor());
                }
            }
        }
    }
}