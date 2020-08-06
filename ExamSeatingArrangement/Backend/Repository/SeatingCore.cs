using ExamSeatingArrangement2020.Data;
using ExamSeatingArrangement2020.Dtos;
using ExamSeatingArrangement2020.Helpers;
using ExamSeatingArrangement2020.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ExamSeatingArrangement2020.Repository
{
    public class SeatingCore : ISeatingCore
    {
        private readonly DataContext _context;
        private readonly IMapperApplication _mapper;

        public SeatingCore(DataContext context, IMapperApplication mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<SeatingPreviewDto> GetSeatingPreview(string date, string session)
        {
            /* MockObject */

            MockObject batchOneMockObject = new MockObject()
            {
                Order = "0",
                Id = "0",
                RegisterNumber = "BatchOne"
            };

            MockObject batchTwoMockObject = new MockObject()
            {
                Order = "0",
                Id = "0",
                RegisterNumber = "BatchTwo"
            };

            List<List<dynamic>> seatingAlg = new List<List<dynamic>>();

            var seatingorder = _context.Exams.Where(x => x.Session == session && x.Date == date)
                .OrderBy(x => x.Order)
                .Select(x => x.Order)
                .ToList();

            foreach (var ord in seatingorder)
            {
                var seatingbatch = _context.Exams.Where(x => x.Session == session && x.Date == date && x.Order == ord)
                .Join(_context.SeatingExams, x => x.Id, y => y.ExamId, (x, y) => new { x.Order, y.SeatingId })
                .Join(_context.Seatings, x => x.SeatingId, y => y.Id, (x, y) => new { x.Order, y.Id, y.RegisterNumber }).OrderBy(x => x.Order)
                .ToList<dynamic>();

                seatingAlg.Add(seatingbatch);
            }

            int c1 = 0;
            int c2 = 0;
            int flag = 0;

            List<dynamic> seat_details = new List<dynamic>();

            var batch_1 = seatingAlg[0];
            var batch_2 = seatingAlg[1];
            var length = 0;

            if (batch_1.Count <= batch_2.Count)
            {
                length = batch_1.Count;
                flag = 1;
            }
            else if (batch_2.Count < batch_1.Count)
            {
                length = batch_2.Count;
                flag = 2;
            }

            int order = 0;

            for (int i = 0; i < seatingAlg.Count; i++)
            {
                for (int s = 0; s < length; s++)
                {
                    if (c1 < length && batch_1.Count > c1)
                    {
                        seat_details.Add(batch_1[c1]);
                        c1++;
                    }
                    else if (flag == 1)
                    {
                        if (order < seatingAlg.Count - 1)
                        {
                            order++;
                            batch_1 = seatingAlg[order];

                            var sublist1 = batch_2.GetRange(c1, batch_2.Count - c1);
                            c1 = 0;

                            if (batch_1.Count < batch_2.Count - c1)
                            {
                                flag = 1;
                                length = batch_2.Count - c1;
                            }
                            else if (batch_1.Count > batch_2.Count - c1)
                            {
                                flag = 2;
                                length = batch_1.Count;
                            }
                        }
                        else
                        {
                            seat_details.Add(batchOneMockObject);
                        }
                    }

                    if (c2 < length && batch_2.Count > c2)
                    {
                        seat_details.Add(batch_2[c2]);
                        c2++;
                    }
                    else if (flag == 2)
                    {
                        if (order < seatingAlg.Count - 1)
                        {
                            order++;
                            batch_2 = seatingAlg[order];

                            var sublist2 = batch_1.GetRange(c2, batch_1.Count - c2);
                            Console.WriteLine(sublist2.Count);
                            c2 = 0;
                            if (batch_2.Count < batch_1.Count - c2)
                            {
                                flag = 2;
                                length = batch_1.Count - c2;
                            }
                            else if (batch_2.Count > batch_1.Count - c2)
                            {
                                flag = 1;
                                length = batch_2.Count;
                            }
                        }
                        else
                        {
                            seat_details.Add(batchTwoMockObject);
                        }
                    }
                }

                /*if (order < seatingAlg.Count)
                {
                    if(flag==1)
                    {
                        order++;
                        batch_1 = seatingAlg[order];
                        c1 = 0;
                    }
                    else if(flag==2)
                    {
                        order++;
                        batch_2 = seatingAlg[order];
                        c2 = 0;
                    }
                }
                else if(order>seatingAlg.Count)
                {
                    if (flag == 1)
                    {
                        int n = batch_2.Count - c1;
                        for(int j=0;j<n;j++)
                        {
                            batch_1.Add("Empty");
                        }
                    }
                    else if(flag == 2)
                    {
                        int n = batch_1.Count - c2;
                        for(int j=0;j<n;j++)
                        {
                            batch_2.Add("Empty");
                        }
                    }
                }*/
            }

            Console.WriteLine(seat_details.Count);

            List<SeatingPreviewDto> seatingPreviews = _mapper.SeatingPreviewModelMapper(seat_details);

            return seatingPreviews;
        }

        public async Task SetRooms(List<RoomDictDto> roomdetails, string date, string session)
        {
            var SeatingandExam = _context.Seatings.Join(_context.SeatingExams, x => x.Id, y => y.SeatingId, (x, y) => new { x.Id, x.RegisterNumber, y.ExamId })
                                               .Join(_context.Exams, x => x.ExamId, y => y.Id, (x, y) => new { x.Id, x.RegisterNumber, x.ExamId, y.Date, y.Session });

            foreach (var room in roomdetails)
            {
                var SeatingIdandExamId = await _context.Seatings.Join(_context.SeatingExams, x => x.Id, y => y.SeatingId, (x, y) => new { x.Id, x.RegisterNumber, y.ExamId })
                                               .Join(_context.Exams, x => x.ExamId, y => y.Id, (x, y) => new { x.Id, x.RegisterNumber, x.ExamId, y.Date, y.Session })
                                               .FirstOrDefaultAsync(x => x.RegisterNumber == room.RegisterNumber && x.Date == date && x.Session == session);

                if (SeatingIdandExamId != null && SeatingIdandExamId.Id != 0 && SeatingIdandExamId.ExamId != 0 && room.Hall != null)
                {
                    var Room = await _context.Rooms.FirstOrDefaultAsync(y => y.RoomNumber == room.Hall && y.SeatNumber == room.SeatNumber);

                    await _context.Database.ExecuteSqlInterpolatedAsync($"INSERT IGNORE into seatingrooms (SeatingId, RoomId) VALUES ({SeatingIdandExamId.Id},{Room.Id});");
                    await _context.Database.ExecuteSqlInterpolatedAsync($"INSERT IGNORE into roomexams(RoomId, ExamId) VALUES ({Room.Id},{SeatingIdandExamId.ExamId});");
                }
            }
        }

        public async Task DeleteSeating(string date, string session)
        {
            await _context.Database.ExecuteSqlInterpolatedAsync($"DELETE from exams where Date={date} and Session={session};");
        }

        public List<SeatingPreviewDto> newSeatingAlg(string date, string session)
        {
            // Intentionally left blank

            List<List<dynamic>> seatingAlg = new List<List<dynamic>>();
            List<dynamic> batch1 = new List<dynamic>();
            List<dynamic> batch2 = new List<dynamic>();
            List<dynamic> seatDetails = new List<dynamic>();

            MockObject mockObject = new MockObject()
            {
                Order = "0",
                Id = "0",
                RegisterNumber = "Empty"
            };

            var seatingorder = _context.Exams.Where(x => x.Session == session && x.Date == date)
                .OrderBy(x => x.Order)
                .Select(x => x.Order)
                .ToList();

            foreach (var ord in seatingorder)
            {
                var seatingbatch = _context.Exams.Where(x => x.Session == session && x.Date == date && x.Order == ord)
                .Join(_context.SeatingExams, x => x.Id, y => y.ExamId, (x, y) => new { x.Order, y.SeatingId })
                .Join(_context.Seatings, x => x.SeatingId, y => y.Id, (x, y) => new { x.Order, y.Id, y.RegisterNumber }).OrderBy(x => x.Order)
                .ToList<dynamic>();

                seatingAlg.Add(seatingbatch);
            }

            for (int i = 0; i < seatingAlg.Count; i++)
            {
                if (i % 2 == 0)
                {
                    batch1.AddRange(seatingAlg[i]);
                }
                else
                {
                    batch2.AddRange(seatingAlg[i]);
                }
            }

            int n1 = batch1.Count;
            int n2 = batch2.Count;
            int extra = 0;
            if (n1 > n2)                   //batch-1 is bigger
            {
                extra = n1 - n2;

                for (int x = 0; x < extra; x++)
                {
                    batch2.Add(mockObject);
                }
                n2 = n1;
            }
            else if (n2 > n1)            //batch-2 is bigger
            {
                extra = n2 - n1;
                for (int y = 0; y < extra; y++)
                {
                    batch1.Add(mockObject);
                }
                n1 = n2;
            }
            else if (n1 == n2)
            {
                extra = 0;
            }

            int a = 0, b = 0;
            while (a < n1 && b < n2)
            {
                seatDetails.Add(batch1[a++]);
                seatDetails.Add(batch2[b++]);
            }

            foreach (var s in seatDetails)
            {
                Console.WriteLine(s);
            }

            List<SeatingPreviewDto> seatingPreviews = _mapper.SeatingPreviewModelMapper(seatDetails);

            return seatingPreviews;
        }

        public async Task<SemesterModel> GetSemesterMonths(string date)
        {
            var sem_month = await _context.Semester.FirstOrDefaultAsync(x => x.Date == date);
            return sem_month;
        }

        public async Task<SeatingArrangements> GetFinalSeatingArrangement(string date, string session)
        {
            var roomnumbers = await _context.Rooms
                .Join(_context.RoomExams, x => x.Id, y => y.RoomId, (x, y) => new { x.Id, x.RoomNumber, y.ExamId })
                .Join(_context.Exams, x => x.ExamId, y => y.Id, (x, y) => new { x.RoomNumber, x.Id, y.Date, y.Session })
                .Where(x => x.Date == date && x.Session == session).Select(x => x.RoomNumber).Distinct()
                .ToListAsync();

            List<SeatingArrangementFinalDto> FinalHallAllocationReport = new List<SeatingArrangementFinalDto>();

            foreach (var roomnumber in roomnumbers)
            {
                Console.WriteLine($"RoomNumber : {roomnumber}");

                Dictionary<string, string> SeatandRegNumberPair = new Dictionary<string, string>();

                using (var command = _context.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = $@"select RegisterNumber,SeatNumber from seatings
                                                right join seatingrooms on seatings.Id = seatingrooms.SeatingId
                                                left join rooms r on seatingrooms.RoomId = r.Id AND RoomNumber='{roomnumber}'
                                                left join roomexams r2 on r.Id = r2.RoomId
                                                left join exams e on r2.ExamId = e.Id AND e.Date='{date}' AND Session='{session}'
                                            WHERE RoomNumber='{roomnumber}' and e.Date='{date}' and e.Session='{session}'GROUP BY SeatNumber;";

                    command.CommandType = CommandType.Text;

                    await _context.Database.OpenConnectionAsync();

                    using (var result = await command.ExecuteReaderAsync())
                    {
                        while (await result.ReadAsync())
                        {
                            var RegNumber = result.GetString(0);
                            var SeatNumber = result.GetString(1);
                            SeatandRegNumberPair.Add(SeatNumber, RegNumber);
                        }
                    }
                }

                List<RoomConsolidatedReportDto> roomConsolidatedReport = new List<RoomConsolidatedReportDto>();

                using (var command = _context.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = $@"select SubjectCode,ShortForm,RoomNumber,count(r2.ExamId) as Count from seatings
                                                join seatingexams s on seatings.Id = s.SeatingId
                                                join exams e on s.ExamId = e.Id
                                                join seatingrooms s2 on s.SeatingId = s2.SeatingId
                                                join rooms r on s2.RoomId = r.Id
                                                join roomexams r2 on s.ExamId = r2.ExamId and s2.RoomId=r2.RoomId
                                                join departments d2 on seatings.DepartmentId = d2.Id
                                                where e.Date='{date}' AND e.Session='{session}' AND RoomNumber='{roomnumber}'
                                            group by DepartmentId,SubjectCode,RoomNumber
                                            order by RoomNumber;";

                    command.CommandType = CommandType.Text;

                    await _context.Database.OpenConnectionAsync();

                    using (var result = await command.ExecuteReaderAsync())
                    {
                        while (await result.ReadAsync())
                        {
                            var SubjectCode = result.GetString(0);
                            var ShortForm = result.GetString(1);
                            var RoomNumber = result.GetString(2);
                            var Count = result.GetInt32(3);
                            RoomConsolidatedReportDto roomConsolidatedReportDto = new RoomConsolidatedReportDto()
                            {
                                SubjectCode = SubjectCode,
                                ShortForm = ShortForm,
                                Count = Count
                            };

                            roomConsolidatedReport.Add(roomConsolidatedReportDto);

                            Console.WriteLine($"SubjectCode : {SubjectCode} Shortform :{ShortForm} RoomNumber :{RoomNumber} Count :{Count}");
                        }
                    }
                }

                SeatingArrangementFinalDto data = _mapper.seatingArrangementFinal(roomnumber, date, session, SeatandRegNumberPair, roomConsolidatedReport);
                Console.WriteLine(data.RoomNumber + "ds");

                FinalHallAllocationReport.Add(data);
            }

            var sem_month = await _context.Semester.FirstOrDefaultAsync(x => x.Date == date);

            SeatingArrangements seatingArrangements = new SeatingArrangements()
            {
                SemesterMonth = sem_month.SemesterMonths,
                SeatingArrangement = FinalHallAllocationReport
            };

            return seatingArrangements;
        }

        public async Task TestRegNumberMethod(string date, string session)
        {
            var roomnumbers = await _context.Rooms
               .Join(_context.RoomExams, x => x.Id, y => y.RoomId, (x, y) => new { x.Id, x.RoomNumber, y.ExamId })
               .Join(_context.Exams, x => x.ExamId, y => y.Id, (x, y) => new { x.RoomNumber, x.Id, y.Date, y.Session })
               .Where(x => x.Date == date && x.Session == session).Select(x => x.RoomNumber).Distinct()
               .ToListAsync();

            foreach (var roomnumber in roomnumbers)
            {
                Console.WriteLine($"RoomNumber : {roomnumber}");

                var seatingdetails = _context.SeatingExams
                    .Join(_context.SeatingRooms, x => x.SeatingId, y => y.SeatingId, (x, y) => new { x.SeatingId, x.ExamId, y.RoomId })
                    .Join(_context.Seatings, x => x.SeatingId, y => y.Id, (x, y) => new { x.SeatingId, x.ExamId, x.RoomId, y.RegisterNumber })
                    .Join(_context.Exams, x => x.ExamId, y => y.Id, (x, y) => new { x.SeatingId, x.ExamId, x.RoomId, x.RegisterNumber, y.Date, y.Session })
                    .Join(_context.Rooms, x => x.RoomId, y => y.Id, (x, y) => new { x.SeatingId, x.ExamId, x.RoomId, x.RegisterNumber, x.Date, x.Session, y.SeatNumber, y.RoomNumber })
                    .Where(x => x.RoomNumber == roomnumber && x.Date == date && x.Session == session).Distinct().ToList();

                Dictionary<string, string> SeatandRegNumberPair = new Dictionary<string, string>();
                Console.WriteLine(seatingdetails.Count);

                int count = 0;
                foreach (var seatreg in seatingdetails)
                {
                    Console.WriteLine($"{seatreg.SeatingId} { seatreg.RegisterNumber} {seatreg.SeatNumber}");
                    //  SeatandRegNumberPair.Add(seatreg.SeatNumber, seatreg.RegisterNumber);

                    count++;
                }
            }
        }

        public async Task<List<RoomsConsolidatedReport>> getConsolidatedReport(string date, string session)
        {
            List<RoomsConsolidatedReport> roomsConsolidatedReport = new List<RoomsConsolidatedReport>();

            var roomnumbers = await _context.Rooms
                .Join(_context.RoomExams, x => x.Id, y => y.RoomId, (x, y) => new { x.Id, x.RoomNumber, y.ExamId })
                .Join(_context.Exams, x => x.ExamId, y => y.Id, (x, y) => new { x.RoomNumber, x.Id, y.Date, y.Session })
                .Where(x => x.Date == date && x.Session == session).Select(x => x.RoomNumber).Distinct()
                .ToListAsync();

            foreach (var roomnumber in roomnumbers)
            {
                List<RoomConsolidatedReportDto> roomConsolidatedReport = new List<RoomConsolidatedReportDto>();

                using (var command = _context.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = $@"select SubjectCode,ShortForm,RoomNumber,count(r2.ExamId) as Count from seatings
                                                join seatingexams s on seatings.Id = s.SeatingId
                                                join exams e on s.ExamId = e.Id
                                                join seatingrooms s2 on s.SeatingId = s2.SeatingId
                                                join rooms r on s2.RoomId = r.Id
                                                join roomexams r2 on s.ExamId = r2.ExamId and s2.RoomId=r2.RoomId
                                                join departments d2 on seatings.DepartmentId = d2.Id
                                                where e.Date='{date}' AND e.Session='{session}' AND RoomNumber='{roomnumber}'
                                            group by DepartmentId,SubjectCode,RoomNumber
                                            order by RoomNumber;";

                    command.CommandType = CommandType.Text;

                    await _context.Database.OpenConnectionAsync();

                    using (var result = await command.ExecuteReaderAsync())
                    {
                        while (await result.ReadAsync())
                        {
                            var SubjectCode = result.GetString(0);
                            var ShortForm = result.GetString(1);
                            var RoomNumber = result.GetString(2);
                            var Count = result.GetInt32(3);
                            RoomConsolidatedReportDto roomConsolidatedReportDto = new RoomConsolidatedReportDto()
                            {
                                SubjectCode = SubjectCode,
                                ShortForm = ShortForm,
                                Count = Count
                            };

                            roomConsolidatedReport.Add(roomConsolidatedReportDto);

                            Console.WriteLine($"SubjectCode : {SubjectCode} Shortform :{ShortForm} RoomNumber :{RoomNumber} Count :{Count}");
                        }
                    }
                }

                RoomsConsolidatedReport report = new RoomsConsolidatedReport()
                {
                    RoomNumber = roomnumber,
                    RoomConsolidatedReportDtos = roomConsolidatedReport
                };

                roomsConsolidatedReport.Add(report);
            }

            return roomsConsolidatedReport;
        }

        public async Task UpdateExamData(ExamDto examDto)
        {
            var exam = await _context.Exams.FirstOrDefaultAsync(x => x.Id == examDto.Id);
            exam.SubjectCode = examDto.SubjectCode;
            exam.SubjectName = examDto.SubjectName;
            exam.Date = examDto.Date;
            exam.Session = examDto.Session;

            await _context.SaveChangesAsync();
        }

        public async Task<HallTicketDetailsDto> GetHallTicket(string registernumber)
        {
            CandidateDetailsDto candidateDetails = null;
            List<HallDetailsDto> hallDetails = new List<HallDetailsDto>();

            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = $@"select RegisterNumber,d.Code,d.Name,d.ShortForm,e.SubjectCode,e.SubjectName,e.Date,e.Session from seatings
                                                right join seatingexams s on seatings.Id = s.SeatingId left join exams e on s.ExamId = e.Id
                                                right join seatingrooms s2 on seatings.Id = s2.SeatingId left join rooms r on s2.RoomId = r.Id
                                                left join departments d on seatings.DepartmentId = d.Id
                                                where RegisterNumber={registernumber};";

                command.CommandType = CommandType.Text;

                await _context.Database.OpenConnectionAsync();

                using (var result = await command.ExecuteReaderAsync())
                {
                    while (await result.ReadAsync())
                    {
                        candidateDetails = new CandidateDetailsDto()
                        {
                            RegisterNumber = result.GetString(0),
                            DepartmentCode = result.GetString(1),
                            DepartmentName = result.GetString(2),
                            ShortForm = result.GetString(3)
                        };

                        HallDetailsDto hallDetailsDto = new HallDetailsDto()
                        {
                            SubjectCode = result.GetString(4),
                            SubjectName = result.GetString(5),
                            Date = result.GetString(6),
                            Session = result.GetString(7)
                        };

                        hallDetails.Add(hallDetailsDto);
                    }
                }
            }

            HallTicketDetailsDto hallTicketDetails = new HallTicketDetailsDto()
            {
                CandidateDetails = candidateDetails,
                HallDetails = hallDetails
            };

            return hallTicketDetails;
        }
    }
}