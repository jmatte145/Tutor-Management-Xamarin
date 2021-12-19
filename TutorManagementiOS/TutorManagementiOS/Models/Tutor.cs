using System;
using System.Collections.Generic;
using System.Text;

namespace TutorManagementiOS.Models
{
    public class Tutor
    {
        public string tutorID { get; set; }
        public string genUserID { get; set; }
        public string totalGrade { get; set; }
        public string totalHours { get; set; }
        public string availableStart { get; set; }
        public string availableEnd { get; set; }
    }
}
