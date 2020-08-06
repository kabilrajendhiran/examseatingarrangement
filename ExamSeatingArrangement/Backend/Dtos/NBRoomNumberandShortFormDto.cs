using System.Collections.Generic;

namespace ExamSeatingArrangement2020.Dtos
{
    public class NBRoomNumberandShortFormDto
    {
        public string RoomNumber { get; set; }
        public List<string> ShortFormList { get; set; }
        public List<string> Subjectcodes { get; set; }
        public List<int> CountList { get; set; }
    }
}