using ExamSeatingArrangement2020.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExamSeatingArrangement2020.Repository
{
    public interface ISeatingCore
    {
        public List<SeatingPreviewDto> GetSeatingPreview(string date, string session);

        public List<SeatingPreviewDto> newSeatingAlg(string date, string session);

        public Task SetRooms(List<RoomDictDto> roomdetails, string date, string session);

        public Task DeleteSeating(string date, string session);

        public Task<SeatingArrangements> GetFinalSeatingArrangement(string date, string session);

        public Task TestRegNumberMethod(string date, string session);

        public Task<List<RoomsConsolidatedReport>> getConsolidatedReport(string date, string session);

        public Task UpdateExamData(ExamDto examDto);

        public Task<HallTicketDetailsDto> GetHallTicket(string registernumber);
    }
}