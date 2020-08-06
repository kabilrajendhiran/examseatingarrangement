using ExamSeatingArrangement2020.Dtos;

using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExamSeatingArrangement2020.Repository
{
    public interface ISeatingRepository
    {
        public Task<IEnumerable> GetSeatings(string date, string session);

        public Task<IEnumerable> getDetailsForAllocation(string date, string session);

        public Task<List<DepartmentDto>> GetDepartmentDetails(string date, string session);

        public Task<List<DepartmentWithCountDto>> GetDeparmtmentDetailsWithCount(string date, string session);

        public Task<List<ExamDetailsDto>> GetExamDetails(string date, string session);

        public Task<List<ExamDto>> GetAllExams();

        public Task<IEnumerable> GetAllExamDates(string session);

        public Task<IEnumerable> AllExamDates();

        public Task<List<MinExamDto>> GetExamDateswithSession();

        public Task<int> SetOrders(Dictionary<int, int> data);

        public Task<int> AddSeatingAsync(AddSeating addSeating);

        public void GetSeatingPreview();

        public Task<IEnumerable> GetRooms();

        public Task<IEnumerable> GetRectangleModels();

        public Task<RectangleModelForPdfReaderDto> GetRegisterRectangleAsync();

        public Task<RectangleModelForPdfReaderDto> GetSubjectRectangleAsync();

        public Task<RectangleModelForPdfReaderDto> GetDepartmentRectangleAsync();

        public Task<RectangleModelForPdfReaderDto> GetDateRectangleAsync();

        public Task<RectangleModelForPdfReaderDto> GetSessionRectangleAsync();

        public Task<List<FileDetailsDto>> GetFileDetails();

        public Task SetSemester(string date, string sem_month);

        public Task UpdateExamDate(MinExamDto minExamDto);

        public Task CreateRoom(string roomName);

        public Task DeleteRoom(string roomNumber);

        public Task UpdateRoom(MinRoomDetailsDto minRoomDetailsDto);

        public Task<List<MinRoomDetailsDto>> GetRoomsDateAndSessionWise(string date, string session);
    }
}