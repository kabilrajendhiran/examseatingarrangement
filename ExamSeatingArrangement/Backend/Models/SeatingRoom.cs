namespace ExamSeatingArrangement2020.Models
{
    public class SeatingRoom
    {
        public int Id { get; set; }
        public int SeatingId { get; set; }
        public Seating Seating { get; set; }

        public int RoomId { get; set; }
        public Room Room { get; set; }
    }
}