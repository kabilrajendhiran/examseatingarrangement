using ExamSeatingArrangement2020.Data;
using ExamSeatingArrangement2020.Services;
using iTextSharp.text.pdf;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExamSeatingArrangement2020.Repository
{
    public class IntermediateRepository : IIntermediateRepository
    {
        private readonly DataContext _context;
        private readonly IPdfReaderService _readerService;

        public IntermediateRepository(DataContext context, IPdfReaderService readerService)
        {
            _context = context;
            _readerService = readerService;
        }

        public async Task<string> GetFilePath(int id)
        {
            var filedetails = await _context.Files.FirstOrDefaultAsync(x => x.Id == id);
            var filepath = filedetails.FilePath;
            return filepath;
        }

        public async Task<int> FillInterMediateTables(string filepath)
        {
            //  string filepath = "E:\\pdf1.pdf";                                       //To be changed
            PdfReader reader = new PdfReader(filepath);
            int n = reader.NumberOfPages;
            List<string> registerNumber = new List<string>();
            string date = "";
            string session = "";
            string subcode = "";
            string deptcode = "";
            int queryResult;

            for (int pgNumber = 1; pgNumber < n; pgNumber++)
            {
                registerNumber = await _readerService.GetRegisterNumberFromPdf(reader, pgNumber);

                deptcode = await _readerService.GetDepartmentCodeFromPdf(reader, pgNumber);
                subcode = await _readerService.GetSubjectCodeFromPdf(reader, pgNumber);
                date = await _readerService.GetDateFromPdf(reader, pgNumber);
                session = await _readerService.GetSessionFromPdf(reader, pgNumber);

                var exam = await _context.Exams.FirstOrDefaultAsync(x => x.SubjectCode == subcode);

                var dept = await _context.Departments.FirstOrDefaultAsync(x => x.Code == deptcode);

                var departmentexams = await _context.Database.ExecuteSqlInterpolatedAsync($"INSERT IGNORE into DepartmentExams(DepartmentId, ExamId) VALUES ({dept.Id},{exam.Id});");

                foreach (var r in registerNumber)
                {
                    var seating = await _context.Seatings.FirstOrDefaultAsync(x => x.RegisterNumber == r);
                    var seatingexams = await _context.Database.ExecuteSqlInterpolatedAsync($"INSERT IGNORE into SeatingExams(SeatingId, ExamId) VALUES ({seating.Id},{exam.Id});");
                }
            }

            queryResult = await _context.SaveChangesAsync();
            return queryResult;
        }

        public async Task FillExamTable(string filepath)
        {
            PdfReader reader = new PdfReader(filepath);
            int n = reader.NumberOfPages;

            string subjectcode = "";
            string subjectname = "";
            string date = "";
            string session = "";

            for (int pgNumber = 1; pgNumber < n; pgNumber++)
            {
                subjectcode = await _readerService.GetSubjectCodeFromPdf(reader, pgNumber);
                subjectname = await _readerService.GetSubjectNameFromPdf(reader, pgNumber);
                date = await _readerService.GetDateFromPdf(reader, pgNumber);
                session = await _readerService.GetSessionFromPdf(reader, pgNumber);

                await _context.Database.ExecuteSqlInterpolatedAsync($"INSERT IGNORE into exams (SubjectCode, SubjectName, Date, Session) VALUES ({subjectcode},{subjectname},{date},{session});");
            }
        }

        public async Task FillSeatingTable(string filepath)
        {
            PdfReader reader = new PdfReader(filepath);
            int n = reader.NumberOfPages;
            var deptcode = "";

            for (int pgNumber = 1; pgNumber < n; pgNumber++)
            {
                List<string> registerNumber = new List<string>();
                deptcode = await _readerService.GetDepartmentCodeFromPdf(reader, pgNumber);
                var dept = await _context.Departments.FirstOrDefaultAsync(x => x.Code == deptcode);
                registerNumber = await _readerService.GetRegisterNumberFromPdf(reader, pgNumber);

                foreach (var regnumber in registerNumber)
                {
                    await _context.Database.ExecuteSqlInterpolatedAsync($"INSERT IGNORE into seatings (RegisterNumber, DepartmentId) VALUES ({regnumber},{dept.Id});");
                }
            }
        }

        public async Task SetFinished(int id)
        {
            await _context.Database.ExecuteSqlInterpolatedAsync($"UPDATE files set IsFinished=true WHERE Id={id};");
        }
    }
}