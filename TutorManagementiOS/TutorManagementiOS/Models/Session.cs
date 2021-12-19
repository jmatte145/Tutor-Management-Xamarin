using System;
using System.Collections.Generic;
using System.Text;

namespace TutorManagementiOS
{
    public class SessionClass
    {
        public string sessionID { get; set; }
        public string tutorID { get; set; }
        public string course { get; set; }
        public string date { get; set; }
        public string time { get; set; }
        public string duration { get; set; }
        public string report { get; set; }
        public string grade { get; set; }
        public bool completed { get; set; }
    }
}
