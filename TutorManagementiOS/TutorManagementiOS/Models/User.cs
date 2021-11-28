using System;
using System.Collections.Generic;
using System.Text;

namespace TutorManagementiOS
{
    public class User
    {
        public string userID { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string userName { get; set; }
        public string password { get; set; }
        public string userType { get; set; }
        public bool approvalStatus { get; set; }
    }
}
