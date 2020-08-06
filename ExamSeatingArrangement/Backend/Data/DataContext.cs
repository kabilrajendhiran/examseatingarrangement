using ExamSeatingArrangement2020.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace ExamSeatingArrangement2020.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        


        public DbSet<FileModel> Files { get; set; }
        public DbSet<Seating> Seatings { get; set; }
        public DbSet<Exam> Exams { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Department> Departments { get; set; }

        //------------------------------------------------------------------

        public DbSet<SeatingExam> SeatingExams { get; set; }
        public DbSet<SeatingRoom> SeatingRooms { get; set; }
        public DbSet<DepartmentExam> DepartmentExams { get; set; }
        public DbSet<RoomExam> RoomExams { get; set; }
        public DbSet<PdfRectangleModel> PdfRectangleModels { get; set; }
        public DbSet<SemesterModel> Semester { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FileModel>().HasKey(x => x.Id);
            modelBuilder.Entity<Seating>().HasKey(x => x.Id);
            modelBuilder.Entity<Exam>().HasKey(x => x.Id);
            modelBuilder.Entity<Room>().HasKey(x => x.Id);
            modelBuilder.Entity<Department>().HasKey(x => x.Id);
            modelBuilder.Entity<PdfRectangleModel>().HasKey(x => x.Id);
            modelBuilder.Entity<SeatingExam>().HasKey(x => x.Id);
            modelBuilder.Entity<SeatingRoom>().HasKey(x => x.Id);
            modelBuilder.Entity<DepartmentExam>().HasKey(x => x.Id);
            modelBuilder.Entity<RoomExam>().HasKey(x => x.Id);
            modelBuilder.Entity<SemesterModel>().HasKey(x => x.Id);

            modelBuilder.Entity<FileModel>().HasAlternateKey(x => x.Name);
            modelBuilder.Entity<Exam>().HasAlternateKey(x => x.SubjectCode);
            modelBuilder.Entity<Seating>().HasAlternateKey(x => x.RegisterNumber);
            modelBuilder.Entity<SemesterModel>().HasAlternateKey(x => x.Date);

            modelBuilder.Entity<SeatingExam>().HasAlternateKey(x => new { x.SeatingId, x.ExamId });
            modelBuilder.Entity<SeatingExam>().HasOne(x => x.Seating).WithMany(m => m.SeatingExams).HasForeignKey(y => y.SeatingId);
            modelBuilder.Entity<SeatingExam>().HasOne(x => x.Exam).WithMany(m => m.ExamSeatings).HasForeignKey(y => y.ExamId);

            modelBuilder.Entity<SeatingRoom>().HasAlternateKey(x => new { x.SeatingId, x.RoomId });
            modelBuilder.Entity<SeatingRoom>().HasOne(x => x.Seating).WithMany(m => m.SeatingRooms).HasForeignKey(y => y.SeatingId);
            modelBuilder.Entity<SeatingRoom>().HasOne(x => x.Room).WithMany(m => m.RoomSeatings).HasForeignKey(y => y.RoomId);

            modelBuilder.Entity<DepartmentExam>().HasAlternateKey(x => new { x.DepartmentId, x.ExamId });
            modelBuilder.Entity<DepartmentExam>().HasOne(x => x.Department).WithMany(m => m.DepartmentExams).HasForeignKey(y => y.DepartmentId);
            modelBuilder.Entity<DepartmentExam>().HasOne(x => x.Exam).WithMany(m => m.ExamDepartments).HasForeignKey(y => y.ExamId);

            modelBuilder.Entity<RoomExam>().HasAlternateKey(x => new { x.RoomId, x.ExamId });
            modelBuilder.Entity<RoomExam>().HasOne(x => x.Room).WithMany(m => m.RoomExams).HasForeignKey(y => y.RoomId);
            modelBuilder.Entity<RoomExam>().HasOne(x => x.Exam).WithMany(m => m.ExamRooms).HasForeignKey(y => y.ExamId);
        }
    }
}