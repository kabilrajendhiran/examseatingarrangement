using System.Collections.Generic;

namespace ExamSeatingArrangement2020.Models
{
    public class Exam
    {
        public int Id { get; set; }
        public string SubjectCode { get; set; }
        public string SubjectName { get; set; }

        //    public int Semester { get; set; }
        public string Date { get; set; }

        public string Session { get; set; }
        public string Order { get; set; }
        public ICollection<SeatingExam> ExamSeatings { get; set; }
        public ICollection<DepartmentExam> ExamDepartments { get; set; }
        public ICollection<RoomExam> ExamRooms { get; set; }
    }
}