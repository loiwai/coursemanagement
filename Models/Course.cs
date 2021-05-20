using System;
using System.Collections.Generic;

#nullable disable

namespace mscs.Models
{
    public partial class Course
    {
        public Course()
        {
            CouresRegistrations = new HashSet<CouresRegistration>();
        }

        public string CourseNo { get; set; }
        public string CourseName { get; set; }
        public string Desccription { get; set; }
        public string LecturerId { get; set; }

        public virtual ICollection<CouresRegistration> CouresRegistrations { get; set; }
    }
}
