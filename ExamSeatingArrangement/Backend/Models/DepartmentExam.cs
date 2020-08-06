namespace ExamSeatingArrangement2020.Models
{
    public class DepartmentExam
    {
        public int Id { get; set; }
        public int DepartmentId { get; set; }
        public Department Department { get; set; }

        public int ExamId { get; set; }
        public Exam Exam { get; set; }
    }
}