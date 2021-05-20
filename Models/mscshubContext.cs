using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace mscs.Models
{
    public partial class mscshubContext : DbContext
    {
        public mscshubContext()
        {
        }

        public mscshubContext(DbContextOptions<mscshubContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CouresRegistration> CouresRegistrations { get; set; }
        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<Lecturer> Lecturers { get; set; }
        public virtual DbSet<News> News { get; set; }
        public virtual DbSet<OnlineActivity> OnlineActivities { get; set; }
        public virtual DbSet<Resume> Resumes { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Transcript> Transcripts { get; set; }
        public virtual DbSet<TutorialPost> TutorialPosts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DESKTOP-F2M336D\\SQLEXPRESS;Database=mscshub;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<CouresRegistration>(entity =>
            {
                entity.HasKey(e => e.CourseRegNo)
                    .HasName("PK_Table_1");

                entity.ToTable("CouresRegistration");

                entity.Property(e => e.CourseRegNo)
                    .HasMaxLength(50)
                    .HasColumnName("courseRegNo");

                entity.Property(e => e.CourseNo)
                    .HasMaxLength(20)
                    .HasColumnName("courseNo");

                entity.Property(e => e.RegDate)
                    .HasColumnType("date")
                    .HasColumnName("regDate");

                entity.Property(e => e.StudentId)
                    .HasMaxLength(10)
                    .HasColumnName("studentId")
                    .IsFixedLength(true);

                entity.HasOne(d => d.CourseNoNavigation)
                    .WithMany(p => p.CouresRegistrations)
                    .HasForeignKey(d => d.CourseNo)
                    .HasConstraintName("FK_CouresRegistration_Course");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.CouresRegistrations)
                    .HasForeignKey(d => d.StudentId)
                    .HasConstraintName("FK_CouresRegistration_Student");
            });

            modelBuilder.Entity<Course>(entity =>
            {
                entity.HasKey(e => e.CourseNo);

                entity.ToTable("Course");

                entity.Property(e => e.CourseNo).HasMaxLength(20);

                entity.Property(e => e.CourseName)
                    .HasMaxLength(50)
                    .HasColumnName("courseName");

                entity.Property(e => e.Desccription)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("desccription");

                entity.Property(e => e.LecturerId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("lecturerId");
            });

            modelBuilder.Entity<Lecturer>(entity =>
            {
                entity.ToTable("Lecturer");

                entity.Property(e => e.LecturerId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("lecturerId");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("firstName");

                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("lastName");
            });

            modelBuilder.Entity<News>(entity =>
            {
                entity.Property(e => e.NewsId)
                    .HasMaxLength(50)
                    .HasColumnName("newsId");

                entity.Property(e => e.LecturerId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("lecturerId");

                entity.Property(e => e.PostDate)
                    .HasColumnType("date")
                    .HasColumnName("postDate");

                entity.Property(e => e.StudentId)
                    .HasMaxLength(10)
                    .HasColumnName("studentId")
                    .IsFixedLength(true);

                entity.HasOne(d => d.Lecturer)
                    .WithMany(p => p.News)
                    .HasForeignKey(d => d.LecturerId)
                    .HasConstraintName("FK_News_Lecturer");

                entity.HasOne(d => d.NewsNavigation)
                    .WithOne(p => p.News)
                    .HasForeignKey<News>(d => d.NewsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_News_Activity");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.News)
                    .HasForeignKey(d => d.StudentId)
                    .HasConstraintName("FK_News_Student");
            });

            modelBuilder.Entity<OnlineActivity>(entity =>
            {
                entity.HasKey(e => e.ActivityId)
                    .HasName("PK_Activity");

                entity.ToTable("OnlineActivity");

                entity.Property(e => e.ActivityId)
                    .HasMaxLength(50)
                    .HasColumnName("activityId");

                entity.Property(e => e.Activity)
                    .HasColumnType("date")
                    .HasColumnName("activity");

                entity.Property(e => e.ActivityName)
                    .HasMaxLength(10)
                    .HasColumnName("activityName")
                    .IsFixedLength(true);

                entity.Property(e => e.Url)
                    .HasMaxLength(10)
                    .HasColumnName("url")
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<Resume>(entity =>
            {
                entity.ToTable("Resume");

                entity.Property(e => e.ResumeId)
                    .HasMaxLength(50)
                    .HasColumnName("resumeId");

                entity.Property(e => e.PostDate)
                    .HasColumnType("date")
                    .HasColumnName("postDate");

                entity.Property(e => e.StudentId)
                    .HasMaxLength(10)
                    .HasColumnName("studentId")
                    .IsFixedLength(true);

                entity.HasOne(d => d.ResumeNavigation)
                    .WithOne(p => p.Resume)
                    .HasForeignKey<Resume>(d => d.ResumeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Resume_Activity");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.Resumes)
                    .HasForeignKey(d => d.StudentId)
                    .HasConstraintName("FK_Resume_Student");
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.ToTable("Student");

                entity.Property(e => e.StudentId)
                    .HasMaxLength(10)
                    .HasColumnName("studentId")
                    .IsFixedLength(true);

                entity.Property(e => e.CourseId).HasMaxLength(50);

                entity.Property(e => e.CourseRegId).HasMaxLength(50);

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .HasColumnName("email");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("firstName");

                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("lastName");

                entity.Property(e => e.TutorialId)
                    .HasMaxLength(50)
                    .HasColumnName("tutorialId");

                entity.Property(e => e.Year).HasColumnName("year");
            });

            modelBuilder.Entity<Transcript>(entity =>
            {
                entity.ToTable("Transcript");

                entity.Property(e => e.TranscriptId)
                    .HasMaxLength(50)
                    .HasColumnName("transcriptId");

                entity.Property(e => e.PostDate)
                    .HasColumnType("date")
                    .HasColumnName("postDate");

                entity.Property(e => e.StudentId)
                    .HasMaxLength(10)
                    .HasColumnName("studentId")
                    .IsFixedLength(true);

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.Transcripts)
                    .HasForeignKey(d => d.StudentId)
                    .HasConstraintName("FK_Transcript_Student");

                entity.HasOne(d => d.TranscriptNavigation)
                    .WithOne(p => p.Transcript)
                    .HasForeignKey<Transcript>(d => d.TranscriptId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Transcript_Activity");
            });

            modelBuilder.Entity<TutorialPost>(entity =>
            {
                entity.HasKey(e => e.TutorialId);

                entity.ToTable("TutorialPost");

                entity.Property(e => e.TutorialId)
                    .HasMaxLength(50)
                    .HasColumnName("tutorialId");

                entity.Property(e => e.CourseNo)
                    .HasMaxLength(50)
                    .HasColumnName("courseNo");

                entity.Property(e => e.LecturerId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("lecturerId");

                entity.Property(e => e.PostDate)
                    .HasColumnType("datetime")
                    .HasColumnName("postDate");

                entity.Property(e => e.Url)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("url");

                entity.HasOne(d => d.Lecturer)
                    .WithMany(p => p.TutorialPosts)
                    .HasForeignKey(d => d.LecturerId)
                    .HasConstraintName("FK_TutorialPost_Lecturer");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
