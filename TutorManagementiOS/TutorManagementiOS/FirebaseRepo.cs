using Firebase.Database;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TutorManagementiOS.Models;

namespace TutorManagementiOS
{
    public class FirebaseRepo
    {
        FirebaseClient firebaseClient =
    new FirebaseClient("https://tutorxamarinproject-default-rtdb.firebaseio.com/");

        // User //
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

        // Student //
        public async Task<bool> SaveStudent(Student student)
        {
            var data = await firebaseClient.Child(nameof(Student)).
                PostAsync(JsonConvert.SerializeObject(student));

            if (!string.IsNullOrEmpty(data.Key))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<List<Student>> GetAllStudents()
        {
            return (await firebaseClient.
                Child(nameof(Student)).OnceAsync<Student>()).Select(
                item => new Student
                {
                    userID = item.Key
                }).ToList();
        }

        // Tutor // 
        public async Task<bool> SaveTutor(Tutor tutor)
        {
            var data = await firebaseClient.Child(nameof(Tutor)).
                PostAsync(JsonConvert.SerializeObject(tutor));

            if (!string.IsNullOrEmpty(data.Key))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<List<Tutor>> GetAllTutors()
        {
            return (await firebaseClient.
                Child(nameof(Tutor)).OnceAsync<Tutor>()).Select(
                item => new Tutor
                {
                    userID = item.Key
                }).ToList();
        }

        // Teacher // 
        public async Task<bool> SaveTeacher(Teacher teacher)
        {
            var data = await firebaseClient.Child(nameof(Teacher)).
                PostAsync(JsonConvert.SerializeObject(teacher));

            if (!string.IsNullOrEmpty(data.Key))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<List<Teacher>> GetAllTeachers()
        {
            return (await firebaseClient.
                Child(nameof(Teacher)).OnceAsync<Teacher>()).Select(
                item => new Teacher
                {
                    userID = item.Key
                }).ToList();
        }
    }
}

