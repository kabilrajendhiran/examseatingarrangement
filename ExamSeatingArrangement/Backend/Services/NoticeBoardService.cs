using ExamSeatingArrangement2020.Data;
using ExamSeatingArrangement2020.Dtos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;

namespace ExamSeatingArrangement2020.Services
{
    public class NoticeBoardService : INoticeBoardService
    {
        private readonly DataContext _context;

        public NoticeBoardService(DataContext context)
        {
            _context = context;
        }

        public async Task<List<string>> GetRoomNumber(string date, string session)
        {
            var roomnumbers = await _context.Rooms
                .Join(_context.RoomExams, x => x.Id, y => y.RoomId, (x, y) => new { x.Id, x.RoomNumber, y.ExamId })
                .Join(_context.Exams, x => x.ExamId, y => y.Id, (x, y) => new { x.RoomNumber, x.Id, y.Date, y.Session })
                .Where(x => x.Date == date && x.Session == session).Select(x => x.RoomNumber).Distinct()
                .ToListAsync();

            return roomnumbers;
        }

        public async Task<List<NBRoomNumberandShortFormDto>> GetShortForm(string date, string session)
        {
            var roomnumbers = await this.GetRoomNumber(date, session);
            List<NBRoomNumberandShortFormDto> NBRoomNumberandShortFormList = new List<NBRoomNumberandShortFormDto>();

            foreach (var roomnumber in roomnumbers)
            {
                List<string> ShortForm = new List<string>();
                List<int> Countlist = new List<int>();
                List<string> SubjectCodes = new List<string>();

                using (var command = _context.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = @$"select ShortForm,SubjectCode,Count(s.SeatingId) as Count from seatings
                                        join seatingexams s on seatings.Id = s.SeatingId
                                        join exams e on s.ExamId = e.Id
                                        join seatingrooms s2 on s.SeatingId = s2.SeatingId
                                        join rooms r on s2.RoomId = r.Id
                                        join roomexams r2 on s.ExamId = r2.ExamId and s2.RoomId = r2.RoomId
                                        join departments d2 on seatings.DepartmentId = d2.Id
                                        where e.Date = '{date}' AND e.Session = '{session}' and RoomNumber = '{roomnumber}'
                                            group by ShortForm,SubjectCode; ";

                    command.CommandType = CommandType.Text;

                    await _context.Database.OpenConnectionAsync();

                    using (var result = await command.ExecuteReaderAsync(CommandBehavior.CloseConnection))
                    {
                        while (await result.ReadAsync())
                        {
                            Console.WriteLine(result.GetString(0));
                            var shortform = result.GetString(0);
                            var subjectcode = result.GetString(1);
                            var count = Int32.Parse(result.GetString(2));
                            ShortForm.Add(shortform);
                            Countlist.Add(count);
                            SubjectCodes.Add(subjectcode);
                        }

                        NBRoomNumberandShortFormDto obj = new NBRoomNumberandShortFormDto()
                        {
                            RoomNumber = roomnumber,
                            ShortFormList = ShortForm,
                            Subjectcodes = SubjectCodes,
                            CountList = Countlist
                        };

                        NBRoomNumberandShortFormList.Add(obj);
                    }
                }
            }

            return NBRoomNumberandShortFormList;
        }

        public async Task<List<NoticeBoardDto>> GetRegisterNumberForNotice(string date, string session)
        {
            List<NBRoomNumberandShortFormDto> data = await this.GetShortForm(date, session);
            List<NoticeBoardDto> noticeBoard = new List<NoticeBoardDto>();

            foreach (var d in data)
            {
                string nb_roomnumber = d.RoomNumber;

                Dictionary<string, string> shortFormCountPair = new Dictionary<string, string>();

                List<int> CountList = d.CountList;
                List<string> SubjectCodeList = d.Subjectcodes;
                List<NoticeDetails> details = new List<NoticeDetails>();

                int x = 0;

                foreach (var sf in d.ShortFormList)
                {
                    List<string> registernumberlist = new List<string>();

                    using (var command = _context.Database.GetDbConnection().CreateCommand())
                    {
                        command.CommandText = @$"select RegisterNumber from seatings
                                            join seatingexams s on seatings.Id = s.SeatingId
                                            join exams e on s.ExamId = e.Id
                                            join seatingrooms s2 on s.SeatingId = s2.SeatingId
                                            join rooms r on s2.RoomId = r.Id
                                            join roomexams r2 on s.ExamId = r2.ExamId and s2.RoomId=r2.RoomId
                                            join departments d2 on seatings.DepartmentId = d2.Id
                                            where e.Date='{date}' AND e.Session='{session}' and RoomNumber='{nb_roomnumber}' and ShortForm='{sf}'
                                        order by RegisterNumber;";

                        command.CommandType = CommandType.Text;

                        await _context.Database.OpenConnectionAsync();

                        using (var reader = await command.ExecuteReaderAsync(CommandBehavior.CloseConnection))
                        {
                            while (await reader.ReadAsync())
                            {
                                registernumberlist.Add(reader.GetString(0));
                            }
                            await reader.CloseAsync();
                            string range = this.roomConsolidatedReportRegFormat(registernumberlist);
                            /*  ranges.Add(range);*/

                            NoticeDetails detail = new NoticeDetails()
                            {
                                ShortForm = sf,
                                SubjectCode = SubjectCodeList[x],
                                Range = range,
                                Count = CountList[x]
                            };
                            details.Add(detail);

                            x++;
                        }
                    }
                }

                NoticeBoardDto obj = new NoticeBoardDto()
                {
                    Date = date,
                    Session = session,
                    RoomNumber = nb_roomnumber,
                    Details = details
                };

                noticeBoard.Add(obj);
            }

            return noticeBoard;
        }

        public string roomConsolidatedReportRegFormat(List<string> registernumber)
        {
            int flag = 0;

            List<BigInteger> sourcelist = registernumber.ConvertAll(BigInteger.Parse);

            List<string> data = new List<string>();

            var o = new List<int>();

            for (int i = 0; i < sourcelist.Count - 1; i++)
            {
                if (sourcelist[i] + 1 == sourcelist[i + 1])
                {
                    // Console.WriteLine(sourcelist[i+1]);
                    if (flag == 0)
                    {
                        data.Add(sourcelist[i].ToString());
                        flag = 1;
                    }
                }
                else
                {
                    data.Add("-");
                    data.Add(sourcelist[i].ToString());
                    data.Add(", ");
                    flag = 0;
                }
            }

            data.Add("-");

            string range = "";

            data.Add(sourcelist[sourcelist.Count - 1].ToString());

            foreach (var a in data)
            {
                range = range + a;
            }

            range = range.Replace(", -", ", ");
            if (range[0] == '-')
            {
                range = range.Substring(1);
            }

            return range;

            /* int start = 0;
             int end = 0;
             for(int i=0;i<data.Count;i++)
             {
                 if(data[i]==",")
                 {
                     end = i - 1;
                     Console.WriteLine($"Start{start}  end{end}");
                     Console.WriteLine(i);
                     start = i+1;
                 }
             }*/
        }
    }
}