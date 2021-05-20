using System;
using System.Collections.Generic;

#nullable disable

namespace mscs.Models
{
    public partial class OnlineActivity
    {
        public string ActivityId { get; set; }
        public string ActivityName { get; set; }
        public string Url { get; set; }
        public DateTime? Activity { get; set; }

        public virtual News News { get; set; }
        public virtual Resume Resume { get; set; }
        public virtual Transcript Transcript { get; set; }
    }
}
