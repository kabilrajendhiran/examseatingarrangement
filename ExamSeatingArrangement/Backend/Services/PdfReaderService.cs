using ExamSeatingArrangement2020.Repository;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExamSeatingArrangement2020.Services
{
    public class PdfReaderService : IPdfReaderService
    {
        private readonly ISeatingRepository _seatingRepository;

        public PdfReaderService(ISeatingRepository seatingRepository)
        {
            _seatingRepository = seatingRepository;
        }

        public List<string> getTextByCoOrdinate(PdfReader reader, int pageNumber, int cordinate1, int coordinate2, int coordinate3, int coordinate4)
        {
            List<string> data = new List<string>();

            iTextSharp.text.Rectangle rect = new iTextSharp.text.Rectangle(cordinate1, coordinate2, coordinate3, coordinate4);
            RenderFilter[] renderFilter = new RenderFilter[1];
            renderFilter[0] = new RegionTextRenderFilter(rect);
            ITextExtractionStrategy textExtractionStrategy = new FilteredTextRenderListener(new LocationTextExtractionStrategy(), renderFilter);

            string text = PdfTextExtractor.GetTextFromPage(reader, pageNumber, textExtractionStrategy);
            string[] words = text.Split('\n');

            foreach (var x in words)
            {
                if (!string.IsNullOrWhiteSpace(x))
                {
                    data.Add(x.Trim());
                }
            }

            foreach (var y in data) { Console.WriteLine(y); }

            return data;
        }

        public async Task<List<string>> GetRegisterNumberFromPdf(PdfReader reader, int pageNumber)
        {
            var model = await _seatingRepository.GetRegisterRectangleAsync();
            List<string> registerNumber = this.getTextByCoOrdinate(reader, pageNumber, model.Llx, model.Lly, model.Urx, model.Ury);
            return registerNumber;
        }

        public async Task<string> GetDepartmentCodeFromPdf(PdfReader reader, int pageNumber)
        {
            List<string> deptCode = new List<string>();
            var model = await _seatingRepository.GetDepartmentRectangleAsync();
            List<string> data = this.getTextByCoOrdinate(reader, pageNumber, model.Llx, model.Lly, model.Urx, model.Ury);

            foreach (var x in data)
            {
                deptCode.Add(x.Trim().Substring(0, 3).Trim());
            }

            return deptCode[0];
        }

        public async Task<string> GetDepartmentNameFromPdf(PdfReader reader, int pageNumber)
        {
            List<string> deptName = new List<string>();
            var model = await _seatingRepository.GetDepartmentRectangleAsync();
            List<string> data = this.getTextByCoOrdinate(reader, pageNumber, model.Llx, model.Lly, model.Urx, model.Ury);

            foreach (var x in data)
            {
                deptName.Add(x.Trim().Substring(6).Trim());
            }

            return deptName[0];
        }

        public async Task<string> GetSubjectCodeFromPdf(PdfReader reader, int pageNumber)
        {
            List<string> subjectCode = new List<string>();
            var model = await _seatingRepository.GetSubjectRectangleAsync();
            List<string> data = this.getTextByCoOrdinate(reader, pageNumber, model.Llx, model.Lly, model.Urx, model.Ury);

            foreach (var x in data)
            {
                subjectCode.Add(x.Trim().Substring(0, 6).Trim());
            }

            return subjectCode[0];
        }

        public async Task<string> GetSubjectNameFromPdf(PdfReader reader, int pageNumber)
        {
            List<string> subjectName = new List<string>();
            var model = await _seatingRepository.GetSubjectRectangleAsync();
            List<string> data = this.getTextByCoOrdinate(reader, pageNumber, model.Llx, model.Lly, model.Urx, model.Ury);

            foreach (var x in data)
            {
                subjectName.Add(x.Trim().Substring(8).Trim());
            }

            return subjectName[0];
        }

        public async Task<string> GetDateFromPdf(PdfReader reader, int pageNumber)
        {
            List<string> examDate = new List<string>();
            var model = await _seatingRepository.GetDateRectangleAsync();
            List<string> data = this.getTextByCoOrdinate(reader, pageNumber, model.Llx, model.Lly, model.Urx, model.Ury);

            foreach (var x in data)
            {
                examDate.Add(x.Trim());
            }

            return examDate[0];
        }

        public async Task<string> GetSessionFromPdf(PdfReader reader, int pageNumber)
        {
            List<string> examSession = new List<string>();
            var model = await _seatingRepository.GetSessionRectangleAsync();
            List<string> data = this.getTextByCoOrdinate(reader, pageNumber, model.Llx, model.Lly, model.Urx, model.Ury);

            foreach (var x in data)
            {
                examSession.Add(x.Trim());
            }

            return examSession[0];
        }
    }
}