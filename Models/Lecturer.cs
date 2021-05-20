using System;
using System.Collections.Generic;

#nullable disable

namespace mscs.Models
{
    public partial class Lecturer
    {
        public Lecturer()
        {
            News = new HashSet<News>();
            TutorialPosts = new HashSet<TutorialPost>();
        }

        public string LecturerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public virtual ICollection<News> News { get; set; }
        public virtual ICollection<TutorialPost> TutorialPosts { get; set; }
    }
}
