using System.Collections.Generic;

namespace ExamSeatingArrangement2020.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string ShortForm { get; set; }
        public ICollection<DepartmentExam> DepartmentExams { get; set; }

        public ICollection<Seating> Seatings { get; set; }

        //   public ICollection<SeatingDepartment> DepartmentSeatings  { get; set; }
    }
}