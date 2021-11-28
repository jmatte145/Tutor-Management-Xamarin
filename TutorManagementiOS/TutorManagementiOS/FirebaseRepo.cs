﻿using Firebase.Database;
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
    new FirebaseClient("https://tutorxamarinproject-default-rtdb.firebaseio.com/");

        public async Task<bool> SaveUser(User user)
        {
            var data = await firebaseClient.Child(nameof(User)).
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

        public async Task<List<User>> GetAllUsers()
        {
            return (await firebaseClient.
                Child(nameof(User)).OnceAsync<User>()).Select(
                item => new User
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

        public async Task<User> GetByUserId(string userID)
        {
            return (await firebaseClient.Child(nameof(User) + "/" + userID).OnceSingleAsync<User>());
        }

        public async Task<bool> UpdateUser(User user)
        {
            await firebaseClient.Child(nameof(User) + "/" + user.userID).PutAsync(JsonConvert.SerializeObject(user));
            return true;
        }
    }
}
