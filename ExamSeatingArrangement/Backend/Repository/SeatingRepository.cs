using ExamSeatingArrangement2020.Data;
using ExamSeatingArrangement2020.Dtos;
using ExamSeatingArrangement2020.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ExamSeatingArrangement2020.Repository
{
    public class SeatingRepository : ISeatingRepository
    {
        private readonly DataContext _context;
        private readonly IMapperApplication _mapper;

        public SeatingRepository(DataContext context, IMapperApplication mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable> GetSeatings(string date, string session)
        {
            List<AllSeatingDetailsDto> allSeatingDetails = new List<AllSeatingDetailsDto>();

            var seats = await _context.Seatings
                .Join(_context.Departments,
                        x => x.DepartmentId,
                        y => y.Id,
                        (x, y) => new { x.Id, x.RegisterNumber, x.Department })

                .Join(_context.SeatingRooms,
                        x => x.Id,
                        y => y.SeatingId,
                        (x, y) => new { x.Id, x.RegisterNumber, x.Department, y.Room })

                .Join(_context.SeatingExams,
                        x => x.Id,
                        y => y.SeatingId,
                        (x, y) => new { x.Id, x.RegisterNumber, x.Department, x.Room, y.Exam })

                .Where(x => x.Exam.Session == session && x.Exam.Date == date)

                .ToListAsync<dynamic>();

            Console.WriteLine("Types " + seats.GetType());

            return await _mapper.MapAllSeatingDetails(seats);
        }

        public async Task<IEnumerable> getDetailsForAllocation(string date, string session)
        {
            List<AllocateSeatingDto> allSeatingDetails = new List<AllocateSeatingDto>();
            var allocationseats = await _context.Seatings
                .Join(_context.Departments,
                    x => x.DepartmentId,
                    y => y.Id,
                    (x, y) => new { x.Id, x.RegisterNumber, x.Department })
                .Join(_context.SeatingExams,
                    x => x.Id,
                    y => y.SeatingId,
                    (x, y) => new { x.Id, x.RegisterNumber, x.Department, y.Exam })

                .Where(x => x.Exam.Session == session && x.Exam.Date == date)
                .ToListAsync<dynamic>();

            return await _mapper.MapAllocateSeatingDetails(allocationseats);
        }

        public async Task<List<DepartmentDto>> GetDepartmentDetails(string date, string session)
        {
            var depts = await _context.Departments
                    .Join(_context.DepartmentExams,
                            x => x.Id,
                            y => y.DepartmentId,
                            (x, y) => new { x.Id, x.Code, x.Name, x.ShortForm, y.Exam })
                    .Join(_context.Seatings, x => x.Id, y => y.DepartmentId, (x, y) => new { x.Id, x.Code, x.Name, x.ShortForm, x.Exam, y.DepartmentId })

                    .Where(x => x.Exam.Date == date && x.Exam.Session == session)

                .ToListAsync<dynamic>();

            foreach (var a in depts)
            {
                Console.WriteLine(a.Code);
            }

            return await _mapper.DepartmentsDetailsMapper(depts);
        }

        public async Task<List<DepartmentWithCountDto>> GetDeparmtmentDetailsWithCount(string date, string session)
        {
            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = $@"select d.Id,d.Code,d.Name,d.ShortForm,count(ExamId) as Count from seatings
                                        inner join departments d on seatings.DepartmentId = d.Id
                                        inner join seatingexams s on seatings.Id = s.SeatingId
                                        inner join exams e on s.ExamId = e.Id
                                        where e.Date='{date}' and e.Session='{session}'
                                        group by ExamId;";

                Console.WriteLine($@"select d.Id,d.Code,d.Name,d.ShortForm,count(ExamId) as Count from seatings
                                        inner join departments d on seatings.DepartmentId = d.Id
                                        inner join seatingexams s on seatings.Id = s.SeatingId
                                        inner join exams e on s.ExamId = e.Id
                                        where e.Date='{date}' and e.Session='{session}'
                                        group by ExamId;");

                command.CommandType = CommandType.Text;

                await _context.Database.OpenConnectionAsync();

                using (var result = await command.ExecuteReaderAsync())
                {
                    var deptswithcountlist = new List<DepartmentWithCountDto>();
                    while (await result.ReadAsync())
                    {
                        var deptswithcount = await _mapper.DepartmentWithCountDtoMapper(result);
                        deptswithcountlist.Add(deptswithcount);
                    }
                    return deptswithcountlist;
                }
            }
        }

        public async Task<List<ExamDetailsDto>> GetExamDetails(string date, string session)
        {
            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = $@"select e.Id,e.SubjectCode,e.SubjectName,e.Date,e.Session,count(ExamId) as Count from seatings
                                        inner join departments d on seatings.DepartmentId = d.Id
                                        inner join seatingexams s on seatings.Id = s.SeatingId
                                        inner join exams e on s.ExamId = e.Id
                                        where e.Date = '{date}' and e.Session = '{session}'
                                        group by ExamId;";

                command.CommandType = CommandType.Text;

                await _context.Database.OpenConnectionAsync();
                using (var result = await command.ExecuteReaderAsync())
                {
                    var examDetailsList = new List<ExamDetailsDto>();
                    while (await result.ReadAsync())
                    {
                        var deptswithcount = await _mapper.ExamDetailsDtoMapper(result);
                        examDetailsList.Add(deptswithcount);
                    }
                    return examDetailsList;
                }
            }
        }

        public async Task<List<ExamDto>> GetAllExams()
        {
            var examsobj = await _context.Exams.ToListAsync();
            var exams = await _mapper.ExamDtoMapper(examsobj);

            return exams;
        }

        public async Task<IEnumerable> GetAllExamDates(string session)         // This method doesn't need Mapper
        {
            List<string> dates = null;

            dates = await _context.Exams.GroupBy(x => x.Date).Select(x => x.Key).ToListAsync();

            return dates;
        }

        public async Task<IEnumerable> AllExamDates()
        {
            List<MinExamDto> examDtos = new List<MinExamDto>();

            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "select id, date from exams group by date;";

                command.CommandType = CommandType.Text;

                await _context.Database.OpenConnectionAsync();
                using (var result = await command.ExecuteReaderAsync())
                {
                    while (await result.ReadAsync())
                    {
                        MinExamDto minExamDto = new MinExamDto()
                        {
                            Id = result.GetInt32(0),
                            Date = result.GetString(1)
                        };

                        examDtos.Add(minExamDto);
                    }
                    return examDtos;
                }
            }
        }

        public async Task<List<MinExamDto>> GetExamDateswithSession()
        {
            List<MinExamDto> examList = new List<MinExamDto>();
            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT id,date,session from exams group by date,session;";
                command.CommandType = CommandType.Text;

                await _context.Database.OpenConnectionAsync();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        MinExamDto minExamDto = new MinExamDto()
                        {
                            Id = reader.GetInt32(0),
                            Date = reader.GetString(1),
                            Session = reader.GetString(2)
                        };
                        examList.Add(minExamDto);
                    }
                }
            }
            return examList;
        }

        public async Task<int> SetOrders(Dictionary<int, int> data)
        {
            int res = 0;
            foreach (var d in data)
            {
                res = await _context.Database.ExecuteSqlInterpolatedAsync($"update exams e set e.Order={d.Value} where e.Id={d.Key};");
            }
            return res;
        }

        public async Task<int> AddSeatingAsync(AddSeating addSeating)
        {
            Console.WriteLine($"register number {addSeating.RegisterNumber} dept id {addSeating.DepartmentId}");
            var res = await _context.Database.ExecuteSqlInterpolatedAsync($"INSERT IGNORE into Seatings(registernumber, departmentid) VALUES ({addSeating.RegisterNumber},{addSeating.DepartmentId});");
            return res;
        }

        public void GetSeatingPreview()
        {
            var sp = _context.Exams.Where(x => x.Session == "FN" && x.Date == "17-Nov-2018")
                .Join(_context.SeatingExams, x => x.Id, y => y.ExamId, (x, y) => new { x.Order, y.SeatingId })
                .Join(_context.Seatings, x => x.SeatingId, y => y.Id, (x, y) => new { x.Order, y.Id, y.RegisterNumber }).OrderBy(x => x.Order)
                .ToList();

            foreach (var s in sp)
            {
                Console.WriteLine($"Id :{s.Id} Order :{s.Order} Register No :{s.RegisterNumber}");
            }
        }

        public async Task<IEnumerable> GetRooms()
        {
            List<MinRoomDetailsDto> Rooms = new List<MinRoomDetailsDto>();

            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = @"select Id,RoomNumber from rooms group by RoomNumber;";
                command.CommandType = CommandType.Text;

                await _context.Database.OpenConnectionAsync();

                using (var result = await command.ExecuteReaderAsync())
                {
                    while (await result.ReadAsync())
                    {
                        MinRoomDetailsDto minRoomDetailsDto = new MinRoomDetailsDto()
                        {
                            Id = result.GetInt32(0),
                            RoomName = result.GetString(1)
                        };

                        Rooms.Add(minRoomDetailsDto);
                    }
                }
            }

            return Rooms;
        }

        public async Task<IEnumerable> GetRectangleModels()
        {
            var rectangleModels = await _context.PdfRectangleModels.Where(x => x.IsActive).ToListAsync();
            return rectangleModels;
        }

        public async Task<RectangleModelForPdfReaderDto> GetRegisterRectangleAsync()
        {
            var registerRect = await _context.PdfRectangleModels.FirstOrDefaultAsync(x => x.IsActive && x.Region == "registernumber");
            var rectangleModel = await _mapper.RectangleMapper(registerRect);
            return rectangleModel;
        }

        public async Task<RectangleModelForPdfReaderDto> GetSubjectRectangleAsync()
        {
            var subjectRect = await _context.PdfRectangleModels.FirstOrDefaultAsync(x => x.IsActive && x.Region == "subject");
            var rectangleModel = await _mapper.RectangleMapper(subjectRect);
            return rectangleModel;
        }

        public async Task<RectangleModelForPdfReaderDto> GetDepartmentRectangleAsync()
        {
            var departmentRect = await _context.PdfRectangleModels.FirstOrDefaultAsync(x => x.IsActive && x.Region == "department");
            var rectangleModel = await _mapper.RectangleMapper(departmentRect);
            return rectangleModel;
        }

        public async Task<RectangleModelForPdfReaderDto> GetDateRectangleAsync()
        {
            var dateRect = await _context.PdfRectangleModels.FirstOrDefaultAsync(x => x.IsActive && x.Region == "date");
            var rectangleModel = await _mapper.RectangleMapper(dateRect);
            return rectangleModel;
        }

        public async Task<RectangleModelForPdfReaderDto> GetSessionRectangleAsync()
        {
            var sessionRect = await _context.PdfRectangleModels.FirstOrDefaultAsync(x => x.IsActive && x.Region == "session");
            var rectangleModel = await _mapper.RectangleMapper(sessionRect);
            return rectangleModel;
        }

        public async Task<List<FileDetailsDto>> GetFileDetails()
        {
            List<FileDetailsDto> fileDetailsDtos = new List<FileDetailsDto>();

            var fileDetails = await _context.Files.ToListAsync();

            foreach (var data in fileDetails)
            {
                FileDetailsDto fileDetailsDto = new FileDetailsDto()
                {
                    Id = data.Id,
                    FileName = data.Name,
                    IsFinished = data.IsFinished
                };

                Console.WriteLine($"Filename :{data.Name}");

                fileDetailsDtos.Add(fileDetailsDto);
            }

            return fileDetailsDtos;
        }

        public async Task SetSemester(string date, string sem_month)
        {
            await _context.Database.ExecuteSqlInterpolatedAsync($"INSERT IGNORE into semester (Date, SemesterMonths) VALUES ({date},{sem_month});");
        }

        public async Task UpdateExamDate(MinExamDto minExamDto)
        {
            Console.WriteLine($"{minExamDto.Id}   {minExamDto.Date}");
            var data = await _context.Exams.FirstOrDefaultAsync(x => x.Id == minExamDto.Id);
            await _context.Database.ExecuteSqlInterpolatedAsync($"update exams set Date={minExamDto.Date} where Date={data.Date};");
            await _context.Database.ExecuteSqlInterpolatedAsync($"update semester set Date={minExamDto.Date} where  Date={data.Date};");
        }

        public async Task CreateRoom(string roomName)     // Bulk Row Insert Query was manually generated and then executed;
        {
            var seatNumbers = await _context.Rooms.GroupBy(x => x.SeatNumber).Select(x => x.Key).ToListAsync();
            string query = "insert ignore into rooms(roomnumber, seatnumber) VALUES ";
            string values = "";

            foreach (var seatNumber in seatNumbers)
            { values = values + $"('{roomName}','{seatNumber}'),"; }

            values = values.Substring(0, values.Length - 1) + ";";
            string finalQuery = query + values;

            await _context.Database.ExecuteSqlCommandAsync(finalQuery);
        }

        public async Task DeleteRoom(string roomNumber)
        {
            await _context.Database.ExecuteSqlInterpolatedAsync($"DELETE from rooms where RoomNumber={roomNumber};");
        }

        public async Task UpdateRoom(MinRoomDetailsDto minRoomDetailsDto)
        {
            var room = await _context.Rooms.FirstOrDefaultAsync(x => x.Id == minRoomDetailsDto.Id);
            await _context.Database.ExecuteSqlInterpolatedAsync($"UPDATE rooms set RoomNumber={minRoomDetailsDto.RoomName} where RoomNumber={room.RoomNumber};");
        }

        public async Task<List<MinRoomDetailsDto>> GetRoomsDateAndSessionWise(string date, string session)
        {
            List<MinRoomDetailsDto> Rooms = new List<MinRoomDetailsDto>();
            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = $@"select RoomId,RoomNumber,Date,Session from roomexams
                                            join rooms r on roomexams.RoomId = r.Id
                                            join exams e on roomexams.ExamId = e.Id AND Date='{date}' AND Session='{session}'
                                        group by RoomNumber;";
                Console.WriteLine("3");

                command.CommandType = CommandType.Text;

                await _context.Database.OpenConnectionAsync();

                using (var result = await command.ExecuteReaderAsync())
                {
                    while (await result.ReadAsync())
                    {
                        MinRoomDetailsDto minRoomDetailsDto = new MinRoomDetailsDto()
                        {
                            Id = result.GetInt32(0),
                            RoomName = result.GetString(1)
                        };

                        Rooms.Add(minRoomDetailsDto);
                    }
                }
            }
            return Rooms;
        }
    }
}