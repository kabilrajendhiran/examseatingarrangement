using ExamSeatingArrangement2020.Data;
using ExamSeatingArrangement2020.Dtos;
using ExamSeatingArrangement2020.Models;
using ExamSeatingArrangement2020.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ExamSeatingArrangement2020.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly ISeatingRepository _seatingRepository;
        private readonly IIntermediateRepository _intermediateRepository;
        private readonly DataContext _context;
        private readonly IWebHostEnvironment _env;

        public FileController(ISeatingRepository seatingRepository, IIntermediateRepository intermediateRepository, DataContext context, IWebHostEnvironment env)
        {
            _seatingRepository = seatingRepository;
            _intermediateRepository = intermediateRepository;
            _context = context;
            _env = env;
        }

        [HttpPost]
        public async Task<IActionResult> UploadFileAsync([FromForm] List<IFormFile> files)
        {
            try
            {
                long size = files.Sum(f => f.Length);
                int count = 0;
                foreach (var formFile in files)
                {
                    if (formFile.Length > 0)
                    {
                        string filename = formFile.FileName;

                        // var filename = Path.GetRandomFileName();
                        var filePathraw1 = Path.Combine(_env.WebRootPath, "uploads");

                        if (!Directory.Exists(filePathraw1)) 
                        {
                            Directory.CreateDirectory(filePathraw1);
                            Console.WriteLine("The directory was created successfully at {0}.", Directory.GetCreationTime(filePathraw1));
                        }

                        var filePath = Path.Combine(filePathraw1,filename);
                        count = count + 1;

                        filePath = filePath.Replace("\\", "\\\\");

                        Console.WriteLine(filePath.ToString());

                        using (var stream = System.IO.File.Create(filePath))
                        {
                            Console.WriteLine("Path");
                            Console.WriteLine(stream);
                            await formFile.CopyToAsync(stream);
                        }

                        await _context.Database.ExecuteSqlRawAsync(@$"INSERT IGNORE into files (Name, FilePath) VALUES ('{filename}','{filePath.ToString()}');");
                    }
                }
                return Ok(new { count = files.Count, size });
            }
            catch (Exception e)
            {
                return BadRequest(new { error = e.ToString() });
            }

            // Process uploaded files
            // Don't rely on or trust the FileName property without validation.
        }

        [HttpGet("getallfiledetails")]
        public async Task<IActionResult> GetFileDetailsAsync()
        {
            var res = await _seatingRepository.GetFileDetails();
            return Ok(res);
        }

        [HttpPost("readpdf")]
        public async Task<IActionResult> ReadPdf([FromBody] SemDetailsDto details)
        {
            Console.WriteLine(details.Date.ToString("dd-MMM-yyyy"));
            try
            {
                var filepath = await _intermediateRepository.GetFilePath(details.FileId);
                await _intermediateRepository.FillExamTable(filepath);
                await _intermediateRepository.FillSeatingTable(filepath);
                await _intermediateRepository.FillInterMediateTables(filepath);

                var date = details.Date.ToString("dd-MMM-yyyy");
                var sem_month = details.SemesterMonth;

                await _seatingRepository.SetSemester(date, sem_month);

                Console.WriteLine("Finished");
                return Ok(new { res = "Success!!!" });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest(new { res = "Error Occured!" });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> DownloadFileAsync(int id)
        {
            var fileobj = await _context.Files.FirstOrDefaultAsync(x => x.Id == id);
            var file = System.IO.File.ReadAllBytes(fileobj.FilePath);

            return Ok(new { data = File(file, "application/pdf", "pdf.pdf") });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFile(int id)
        {
            try
            {
                var fileobj = await _context.Files.FirstOrDefaultAsync(x => x.Id == id);

                await _context.Database.ExecuteSqlInterpolatedAsync($"DELETE from files where Id={id};");

                if (System.IO.File.Exists(fileobj.FilePath))
                {
                    System.IO.File.Delete(fileobj.FilePath);
                }

                return Ok(new { res = "Successfully Deleted" });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest(new { res = "Error Occured" });
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateFileStatusManually(FileModel fileModel)
        {
            try
            {
                var filedata = await _context.Files.FirstOrDefaultAsync(x => x.Id == fileModel.Id);
                filedata.IsFinished = fileModel.IsFinished;
                await _context.SaveChangesAsync();
                return Ok(new { res = "Updated Succesfully !!!" });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest(new { res = "Error Occured!!!" });
            }
        }
    }
}