using System.Collections.Generic;

namespace ExamSeatingArrangement2020.Dtos
{
    public class SeatingArrangementFinalDto
    {
        public string RoomNumber { get; set; }
        public List<RoomConsolidatedReportDto> Report { get; set; }
        public string Date { get; set; }
        public string Session { get; set; }
        public Dictionary<string, string> SeatandRegisterNumber { get; set; }
    }
}