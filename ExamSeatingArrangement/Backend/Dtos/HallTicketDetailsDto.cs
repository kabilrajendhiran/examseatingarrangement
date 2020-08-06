using System.Collections.Generic;

namespace ExamSeatingArrangement2020.Dtos
{
    public class HallTicketDetailsDto
    {
        public CandidateDetailsDto CandidateDetails { get; set; }
        public List<HallDetailsDto> HallDetails { get; set; }
    }
}