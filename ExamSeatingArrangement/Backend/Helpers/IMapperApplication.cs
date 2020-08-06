using ExamSeatingArrangement2020.Dtos;
using ExamSeatingArrangement2020.Models;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;

namespace ExamSeatingArrangement2020.Helpers
{
    public interface IMapperApplication
    {
        public Task<List<AllSeatingDetailsDto>> MapAllSeatingDetails(List<dynamic> seats);

        public Task<List<AllocateSeatingDto>> MapAllocateSeatingDetails(List<dynamic> allocationseats);

        public Task<RectangleModelForPdfReaderDto> RectangleMapper(PdfRectangleModel model);

        public Task<List<DepartmentDto>> DepartmentsDetailsMapper(List<dynamic> depts);

        public Task<DepartmentWithCountDto> DepartmentWithCountDtoMapper(DbDataReader data);

        public Task<ExamDetailsDto> ExamDetailsDtoMapper(DbDataReader data);

        public Task<List<ExamDto>> ExamDtoMapper(List<Exam> examdata);

        public List<SeatingPreviewDto> SeatingPreviewModelMapper(List<dynamic> seatsModel);

        public SeatingArrangementFinalDto seatingArrangementFinal(string roomnumber, string date, string session, Dictionary<string, string> seatandreg, List<RoomConsolidatedReportDto> roomConsolidatedReport);
    }
}