using System.Collections.Generic;

namespace ExamSeatingArrangement2020.Models
{
    public class Room
    {
        public int Id { get; set; }
        public string RoomNumber { get; set; }
        public string SeatNumber { get; set; }
        public ICollection<SeatingRoom> RoomSeatings { get; set; }
        public ICollection<RoomExam> RoomExams { get; set; }
    }
}