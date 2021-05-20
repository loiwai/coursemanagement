using System;
using System.Collections.Generic;

#nullable disable

namespace mscs.Models
{
    public partial class CouresRegistration
    {
        public string CourseRegNo { get; set; }
        public string StudentId { get; set; }
        public string CourseNo { get; set; }
        public DateTime? RegDate { get; set; }

        public virtual Course CourseNoNavigation { get; set; }
        public virtual Student Student { get; set; }
    }
}
