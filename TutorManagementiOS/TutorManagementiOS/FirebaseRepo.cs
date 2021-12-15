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
        // This is a test comment // 
        FirebaseClient firebaseClient =
    new FirebaseClient("https://phone-displayer-default-rtdb.firebaseio.com/");

        // User //
        public async Task<string> SaveUser(UserClass user)
        {
            var data = await firebaseClient.Child(nameof(UserClass)).
                PostAsync(JsonConvert.SerializeObject(user));

            if (!string.IsNullOrEmpty(data.Key))
            {
                //Console.WriteLine(data.Key);
                return data.Key;
            }
            else
            {
                return "fail";
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

        public async Task<bool> DeleteUser(UserClass user)
        {
            await firebaseClient.Child(nameof(UserClass) + "/" + user.userID).DeleteAsync();
            return true;
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
                    studentID = item.Key,
                    genUserID = item.Object.genUserID,
                    totalVisits = item.Object.totalVisits,
                    totalHours = item.Object.totalHours,
                }).ToList();
        }
        public async Task<Student> GetStudentByID(string sID)
        {
            return (await firebaseClient.Child(nameof(Student)
            + "/" + sID).OnceSingleAsync<Student>());
        }

        public async Task<bool> DeleteStudent(Student student)
        {
            await firebaseClient.Child(nameof(Student) + "/" + student.studentID).DeleteAsync();
            return true;
        }

        public async Task<bool> UpdateStudent(Student student)
        {
            await firebaseClient.Child(nameof(Student) + "/" + student.studentID).PutAsync(JsonConvert.SerializeObject(student));
            return true;
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
                    tutorID = item.Key,
                    genUserID = item.Object.genUserID,
                    totalGrade = item.Object.totalGrade,
                    totalHours = item.Object.totalHours
                }).ToList();
        }
        public async Task<Tutor> GetTutorByID(string sID)
        {
            return (await firebaseClient.Child(nameof(Tutor)
            + "/" + sID).OnceSingleAsync<Tutor>());
        }

        public async Task<bool> DeleteTutor(Tutor tutor)
        {
            await firebaseClient.Child(nameof(Tutor) + "/" + tutor.tutorID).DeleteAsync();
            return true;
        }

        public async Task<bool> UpdateTutor(Tutor tutor)
        {
            await firebaseClient.Child(nameof(Tutor) + "/" + tutor.tutorID).PutAsync(JsonConvert.SerializeObject(tutor));
            return true;
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
                    teacherID = item.Key,
                    genUserID = item.Object.genUserID
                }).ToList();
        }
        public async Task<Teacher> GetTeacherByID(string sID)
        {
            return (await firebaseClient.Child(nameof(Teacher)
            + "/" + sID).OnceSingleAsync<Teacher>());
        }

        public async Task<bool> DeleteTeacher(Teacher teacher)
        {
            await firebaseClient.Child(nameof(Teacher) + "/" + teacher.teacherID).DeleteAsync();
            return true;
        }

        public async Task<bool> UpdateTeacher(Teacher teacher)
        {
            await firebaseClient.Child(nameof(Teacher) + "/" + teacher.teacherID).PutAsync(JsonConvert.SerializeObject(teacher));
            return true;
        }

        // Session //
        public async Task<string> SaveSession(SessionClass session)
        {
            var data = await firebaseClient.Child(nameof(SessionClass)).
                PostAsync(JsonConvert.SerializeObject(session));

            if (!string.IsNullOrEmpty(data.Key))
            {
                return data.Key;
            }
            else
            {
                return "fail";
            }
        }

        public async Task<bool> UpdateSession(SessionClass session)
        {
            await firebaseClient.Child(nameof(SessionClass) + "/" + session.sessionID).PutAsync(JsonConvert.SerializeObject(session));
            return true;
        }
        public async Task<bool> DeleteSession(SessionClass session)
        {
            await firebaseClient.Child(nameof(SessionClass) + "/" + session.sessionID).DeleteAsync();
            return true;
        }
        public async Task<List<SessionClass>> GetAllSessions()
        {
            return (await firebaseClient.
                Child(nameof(SessionClass)).OnceAsync<SessionClass>()).Select(
                item => new SessionClass
                {
                    sessionID = item.Key,
                    tutorID = item.Object.tutorID,
                    sessionMembers = item.Object.sessionMembers,
                    date = item.Object.date,
                    time = item.Object.time,
                    duration= item.Object.duration,
                    report= item.Object.report,
                    grade = item.Object.grade,
                    open = item.Object.open,

                }).ToList();
        }
        public async Task<SessionClass> GetSessionByID(string sID)
        {
            return (await firebaseClient.Child(nameof(SessionClass)
            + "/" + sID).OnceSingleAsync<SessionClass>());
        }

        // Course Class // 
        public async Task<bool> SaveCourse(CourseClass course)
        {
            var data = await firebaseClient.Child(nameof(CourseClass)).
                PostAsync(JsonConvert.SerializeObject(course));

            if (!string.IsNullOrEmpty(data.Key))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<List<CourseClass>> GetAllCourses()
        {
            return (await firebaseClient.
                Child(nameof(CourseClass)).OnceAsync<CourseClass>()).Select(
                item => new CourseClass
                {
                    courseID = item.Key,
                    courseName = item.Object.courseName,
                    courseNumber = item.Object.courseNumber
                }).ToList();
        }
        public async Task<CourseClass> GetCourseByID(string cID)
        {
            return (await firebaseClient.Child(nameof(CourseClass)
            + "/" + cID).OnceSingleAsync<CourseClass>());
        }

        public async Task<bool> DeleteCourse(CourseClass course)
        {
            await firebaseClient.Child(nameof(CourseClass) + "/" + course.courseID).DeleteAsync();
            return true;
        }

        public async Task<bool> UpdateCourse(CourseClass course)
        {
            await firebaseClient.Child(nameof(CourseClass) + "/" + course.courseID).PutAsync(JsonConvert.SerializeObject(course));
            return true;
        }
    }
}

