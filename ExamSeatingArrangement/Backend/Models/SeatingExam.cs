namespace ExamSeatingArrangement2020.Models
{
    public class SeatingExam
    {
        public int Id { get; set; }
        public int SeatingId { get; set; }
        public Seating Seating { get; set; }

        public int ExamId { get; set; }
        public Exam Exam { get; set; }
    }
}