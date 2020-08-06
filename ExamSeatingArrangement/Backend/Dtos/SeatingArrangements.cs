using System.Collections.Generic;

namespace ExamSeatingArrangement2020.Dtos
{
    public class SeatingArrangements
    {
        public string SemesterMonth { get; set; }
        public List<SeatingArrangementFinalDto> SeatingArrangement { get; set; }
    }
}