using ExamSeatingArrangement2020.Dtos;
using ExamSeatingArrangement2020.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;

namespace ExamSeatingArrangement2020.Helpers
{
    public class MapperApplication : IMapperApplication
    {
        public async Task<List<AllSeatingDetailsDto>> MapAllSeatingDetails(List<dynamic> seats)
        {
            List<AllSeatingDetailsDto> allSeatingDetails = new List<AllSeatingDetailsDto>();

            await Task.Run(() =>
            {
                foreach (var x in seats)
                {
                    SeatingDto s = new SeatingDto()
                    {
                        Id = x.Id,
                        RegisterNumber = x.RegisterNumber
                    };

                    DepartmentDto d = new DepartmentDto()
                    {
                        Id = x.Department.Id,
                        Code = x.Department.Code,
                        Name = x.Department.Name,
                        ShortForm = x.Department.ShortForm
                    };

                    RoomDto r = new RoomDto()
                    {
                        Id = x.Room.Id,
                        RoomNumber = x.Room.RoomNumber
                    };

                    ExamDto e = new ExamDto()
                    {
                        Id = x.Exam.Id,
                        SubjectCode = x.Exam.SubjectCode,
                        SubjectName = x.Exam.SubjectName,
                        Date = x.Exam.Date,
                        Session = x.Exam.Session
                    };

                    AllSeatingDetailsDto allSeatingDetailsDto = new AllSeatingDetailsDto() { Seating = s, Department = d, Exam = e, Room = r };

                    allSeatingDetails.Add(allSeatingDetailsDto);
                }
            });

            return allSeatingDetails;
        }

        public async Task<List<AllocateSeatingDto>> MapAllocateSeatingDetails(List<dynamic> allocationseats)
        {
            List<AllocateSeatingDto> allocationSeatingList = new List<AllocateSeatingDto>();

            await Task.Run(() =>
            {
                foreach (var x in allocationseats)
                {
                    SeatingDto s = new SeatingDto()
                    {
                        Id = x.Id,
                        RegisterNumber = x.RegisterNumber
                    };

                    DepartmentDto d = new DepartmentDto()
                    {
                        Id = x.Department.Id,
                        Code = x.Department.Code,
                        Name = x.Department.Name,
                        ShortForm = x.Department.ShortForm
                    };

                    ExamDto e = new ExamDto()
                    {
                        Id = x.Exam.Id,
                        SubjectCode = x.Exam.SubjectCode,
                        SubjectName = x.Exam.SubjectName,
                        Date = x.Exam.Date,
                        Session = x.Exam.Session
                    };

                    AllocateSeatingDto allocateSeating = new AllocateSeatingDto()
                    {
                        Seating = s,
                        Department = d,
                        Exam = e
                    };

                    allocationSeatingList.Add(allocateSeating);
                }
            });

            return allocationSeatingList;
        }

        public async Task<RectangleModelForPdfReaderDto> RectangleMapper(PdfRectangleModel model)
        {
            RectangleModelForPdfReaderDto modelForPdfReaderDto = null;

            await Task.Run(() =>
            {
                modelForPdfReaderDto = new RectangleModelForPdfReaderDto()
                {
                    Llx = model.Llx,
                    Lly = model.Lly,
                    Urx = model.Urx,
                    Ury = model.Ury
                };
            });

            return modelForPdfReaderDto;
        }

        public async Task<List<DepartmentDto>> DepartmentsDetailsMapper(List<dynamic> depts)
        {
            List<DepartmentDto> departments = new List<DepartmentDto>();
            await Task.Run(() =>
            {
                foreach (var x in depts)
                {
                    DepartmentDto department = new DepartmentDto()
                    {
                        Id = x.Id,
                        Code = x.Code,
                        Name = x.Name,
                        ShortForm = x.ShortForm,
                    };

                    departments.Add(department);
                }
            });

            return departments;
        }

        public async Task<DepartmentWithCountDto> DepartmentWithCountDtoMapper(DbDataReader data)
        {
            DepartmentWithCountDto departmentWithCount = null;

            await Task.Run(() =>
            {
                departmentWithCount = new DepartmentWithCountDto()
                {
                    Id = data.GetInt32(0),
                    Code = data.GetString(1),
                    Name = data.GetString(2),
                    ShortForm = data.GetString(3),
                    Count = data.GetInt32(4)
                };
            });

            return departmentWithCount;
        }

        public async Task<ExamDetailsDto> ExamDetailsDtoMapper(DbDataReader data)
        {
            ExamDetailsDto examDetails = null;

            await Task.Run(() =>
            {
                examDetails = new ExamDetailsDto()
                {
                    Id = data.GetInt32(0),
                    SubjectCode = data.GetString(1),
                    SubjectName = data.GetString(2),
                    Date = data.GetString(3),
                    Session = data.GetString(4),
                    Count = data.GetInt32(5)
                };
            });

            return examDetails;
        }

        public async Task<List<ExamDto>> ExamDtoMapper(List<Exam> examdata)
        {
            List<ExamDto> exams = new List<ExamDto>();
            await Task.Run(() =>
            {
                foreach (var exam in examdata)
                {
                    ExamDto examdto = new ExamDto()
                    {
                        Id = exam.Id,
                        SubjectCode = exam.SubjectCode,
                        SubjectName = exam.SubjectName,
                        Date = exam.Date,
                        Session = exam.Session,
                    };
                    exams.Add(examdto);
                }
            });

            return exams;
        }

        public List<SeatingPreviewDto> SeatingPreviewModelMapper(List<dynamic> seatsModel)
        {
            List<string> SeatNumber = new List<string>() {
           "A1","A2","A3","A4","A5","A6","A7",
                "B7","B6","B5","B4","B3","B2",
                "C2","C3","C4","C5","C6","C7",
                "D7","D6","D5","D4","D3","D2"
            };

            int INDEX = 0;

            List<SeatingPreviewDto> seatingPreviewList = new List<SeatingPreviewDto>();

            foreach (var s in seatsModel)
            {
                string id = "";
                string order = "";
                string registernumber = "";

                string SeatNo = SeatNumber[INDEX % 25];
                INDEX++;

                id = s.Id.ToString();
                order = s.Order.ToString();
                registernumber = s.RegisterNumber.ToString();

                SeatingPreviewDto seatingPreview = new SeatingPreviewDto()
                {
                    Id = id,
                    RegisterNumber = registernumber,
                    Order = order,
                    SeatNumber = SeatNo
                };

                seatingPreviewList.Add(seatingPreview);
            }

            int z = 0;
            foreach (var x in seatingPreviewList)
            {
                //            if (x.Id != "0") {
                z++;
                Console.WriteLine($"Id :{x.Id} RegisterNumber: {x.RegisterNumber} Order :{x.Order} SeatNo. :{x.SeatNumber}");
                //            }
            }
            Console.WriteLine(z);

            return seatingPreviewList;
        }

        public SeatingArrangementFinalDto seatingArrangementFinal(

            string roomnumber,
            string date,
            string session,
            Dictionary<string, string> seatandreg,
            List<RoomConsolidatedReportDto> roomConsolidatedReport

            )

        {
            SeatingArrangementFinalDto seatingArrangementFinalDto = new SeatingArrangementFinalDto()
            {
                RoomNumber = roomnumber,
                Date = date,
                Session = session,
                SeatandRegisterNumber = seatandreg,
                Report = roomConsolidatedReport
            };

            return seatingArrangementFinalDto;
        }
    }
}