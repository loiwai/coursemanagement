using System;
using System.Collections.Generic;

#nullable disable

namespace mscs.Models
{
    public partial class Student
    {
        public Student()
        {
            CouresRegistrations = new HashSet<CouresRegistration>();
            News = new HashSet<News>();
            Resumes = new HashSet<Resume>();
            Transcripts = new HashSet<Transcript>();
        }

        public string StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int? Year { get; set; }
        public string TutorialId { get; set; }
        public string CourseRegId { get; set; }
        public string CourseId { get; set; }

        public virtual ICollection<CouresRegistration> CouresRegistrations { get; set; }
        public virtual ICollection<News> News { get; set; }
        public virtual ICollection<Resume> Resumes { get; set; }
        public virtual ICollection<Transcript> Transcripts { get; set; }
    }
}
