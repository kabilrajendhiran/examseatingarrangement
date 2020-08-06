namespace ExamSeatingArrangement2020.Dtos
{
    public class ExamDetailsDto
    {
        public int Id { get; set; }
        public string SubjectCode { get; set; }
        public string SubjectName { get; set; }
        public string Date { get; set; }
        public string Session { get; set; }
        public int Count { get; set; }
    }
}