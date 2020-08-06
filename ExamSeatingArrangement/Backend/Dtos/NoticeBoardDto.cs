using System.Collections.Generic;

namespace ExamSeatingArrangement2020.Dtos
{
    public class NoticeBoardDto
    {
        public string RoomNumber { get; set; }
        public string Date { get; set; }
        public string Session { get; set; }
        public List<NoticeDetails> Details { get; set; }
    }

    public class NoticeDetails
    {
        public string ShortForm { get; set; }
        public string SubjectCode { get; set; }
        public string Range { get; set; }
        public int Count { get; set; }
    }
}