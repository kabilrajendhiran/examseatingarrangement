namespace ExamSeatingArrangement2020.Models
{
    public class PdfRectangleModel
    {
        public int Id { get; set; }
        public int Llx { get; set; }
        public int Lly { get; set; }
        public int Urx { get; set; }
        public int Ury { get; set; }
        public string Region { get; set; }
        public bool IsActive { get; set; }
    }
}