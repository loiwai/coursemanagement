using System;
using System.Collections.Generic;

#nullable disable

namespace mscs.Models
{
    public partial class TutorialPost
    {
        public string TutorialId { get; set; }
        public string CourseNo { get; set; }
        public DateTime? PostDate { get; set; }
        public string LecturerId { get; set; }
        public string Url { get; set; }

        public virtual Lecturer Lecturer { get; set; }
    }
}
