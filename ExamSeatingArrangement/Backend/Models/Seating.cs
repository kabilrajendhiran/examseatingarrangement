using System.Collections.Generic;

namespace ExamSeatingArrangement2020.Models
{
    public class Seating
    {
        public int Id { get; set; }
        public string RegisterNumber { get; set; }
        public ICollection<SeatingExam> SeatingExams { get; set; }
        public ICollection<SeatingRoom> SeatingRooms { get; set; }
        //    public ICollection<SeatingDepartment> SeatingDepartments { get; set; }

        public Department Department { get; set; }
        public int DepartmentId { get; set; }
    }
}