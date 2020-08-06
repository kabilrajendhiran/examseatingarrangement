using iTextSharp.text.pdf;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExamSeatingArrangement2020.Services
{
    public interface IPdfReaderService
    {
        public List<string> getTextByCoOrdinate(PdfReader reader, int pageNumber, int cordinate1, int coordinate2, int coordinate3, int coordinate4);

        public Task<List<string>> GetRegisterNumberFromPdf(PdfReader reader, int pageNumber);

        public Task<string> GetDepartmentCodeFromPdf(PdfReader reader, int pageNumber);

        public Task<string> GetDepartmentNameFromPdf(PdfReader reader, int pageNumber);

        public Task<string> GetSubjectCodeFromPdf(PdfReader reader, int pageNumber);

        public Task<string> GetSubjectNameFromPdf(PdfReader reader, int pageNumber);

        public Task<string> GetDateFromPdf(PdfReader reader, int pageNumber);

        public Task<string> GetSessionFromPdf(PdfReader reader, int pageNumber);
    }
}