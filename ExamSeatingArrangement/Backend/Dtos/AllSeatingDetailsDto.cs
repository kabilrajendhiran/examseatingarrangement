namespace ExamSeatingArrangement2020.Dtos
{
    public class AllSeatingDetailsDto
    {
        public AllSeatingDetailsDto()
        {
        }

        public SeatingDto Seating { get; set; }
        public DepartmentDto Department { get; set; }
        public RoomDto Room { get; set; }
        public ExamDto Exam { get; set; }
    }
}