using System;
using System.Collections.Generic;

#nullable disable

namespace mscs.Models
{
    public partial class Resume
    {
        public string ResumeId { get; set; }
        public string StudentId { get; set; }
        public DateTime? PostDate { get; set; }

        public virtual OnlineActivity ResumeNavigation { get; set; }
        public virtual Student Student { get; set; }
    }
}
