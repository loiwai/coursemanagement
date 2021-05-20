using System;
using System.Collections.Generic;

#nullable disable

namespace mscs.Models
{
    public partial class Transcript
    {
        public string TranscriptId { get; set; }
        public DateTime? PostDate { get; set; }
        public string StudentId { get; set; }

        public virtual Student Student { get; set; }
        public virtual OnlineActivity TranscriptNavigation { get; set; }
    }
}
