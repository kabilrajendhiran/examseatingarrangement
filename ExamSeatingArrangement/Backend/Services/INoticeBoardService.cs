using ExamSeatingArrangement2020.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExamSeatingArrangement2020.Services
{
    public interface INoticeBoardService
    {
        public Task<List<string>> GetRoomNumber(string date, string session);

        public Task<List<NBRoomNumberandShortFormDto>> GetShortForm(string date, string session);

        public Task<List<NoticeBoardDto>> GetRegisterNumberForNotice(string date, string session);

        public string roomConsolidatedReportRegFormat(List<string> registernumber);
    }
}