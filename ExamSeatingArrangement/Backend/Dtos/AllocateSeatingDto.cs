namespace ExamSeatingArrangement2020.Dtos
{
    public class AllocateSeatingDto
    {
        public SeatingDto Seating { get; set; }
        public DepartmentDto Department { get; set; }
        public ExamDto Exam { get; set; }
    }
}