using System.Collections.Generic;

namespace ExamSeatingArrangement2020.Dtos
{
    public class RoomsConsolidatedReport
    {
        public string RoomNumber { get; set; }
        public List<RoomConsolidatedReportDto> RoomConsolidatedReportDtos { get; set; }
    }
}