namespace ExamSeatingArrangement2020.Models
{
    public class RoomExam
    {
        public int Id { get; set; }
        public int RoomId { get; set; }
        public Room Room { get; set; }
        public int ExamId { get; set; }
        public Exam Exam { get; set; }
    }
}