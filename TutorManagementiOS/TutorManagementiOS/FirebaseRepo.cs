using Firebase.Database;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TutorManagementiOS
{
    public class FirebaseRepo
    {
        FirebaseClient firebaseClient =
    new FirebaseClient("https://tutorapp-daf4d-default-rtdb.firebaseio.com/");

        public async Task<bool> SaveUser(UserClass user)
        {
            var data = await firebaseClient.Child(nameof(UserClass)).
                PostAsync(JsonConvert.SerializeObject(user));

            if (!string.IsNullOrEmpty(data.Key))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<List<UserClass>> GetAllUsers()
        {
            return (await firebaseClient.
                Child(nameof(UserClass)).OnceAsync<UserClass>()).Select(
                item => new UserClass
                {
                    userID = item.Key,
                    firstName = item.Object.firstName,
                    lastName = item.Object.lastName,
                    email = item.Object.email,
                    userName = item.Object.userName,
                    password = item.Object.password,
                    userType = item.Object.userType,
                    approvalStatus = item.Object.approvalStatus
                }).ToList();
        }

        public async Task<UserClass> GetByUserId(string userID)
        {
            return (await firebaseClient.Child(nameof(UserClass) + "/" + userID).OnceSingleAsync<UserClass>());
        }

        public async Task<bool> UpdateUser(UserClass user)
        {
            await firebaseClient.Child(nameof(UserClass) + "/" + user.userID).PutAsync(JsonConvert.SerializeObject(user));
            return true;
        }
    }
}

