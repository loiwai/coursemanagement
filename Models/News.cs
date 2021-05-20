using System;
using System.Collections.Generic;

#nullable disable

namespace mscs.Models
{
    public partial class News
    {
        public string NewsId { get; set; }
        public DateTime? PostDate { get; set; }
        public string StudentId { get; set; }
        public string LecturerId { get; set; }

        public virtual Lecturer Lecturer { get; set; }
        public virtual OnlineActivity NewsNavigation { get; set; }
        public virtual Student Student { get; set; }
    }
}
