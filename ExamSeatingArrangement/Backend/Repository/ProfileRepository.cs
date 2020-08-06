using ExamSeatingArrangement2020.Data;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ExamSeatingArrangement2020.Repository
{
    public class ProfileRepository
    {
        private readonly DataContext _context;

        public ProfileRepository(DataContext context)
        {
            _context = context;
        }

        public string RandomPathGenerator()
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[8];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            var finalString = new String(stringChars);
            Console.Write(finalString);
            return finalString;
        }

        public async Task<string> StoreProfilePicPath(List<IFormFile> files)
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
                        var filePathraw = Path.Combine(Directory.GetCurrentDirectory(), "Storage\\ProfilePic", filename + this.RandomPathGenerator());
                        count = count + 1;

                        var filePath = filePathraw.Replace("\\", "\\\\");

                        Console.WriteLine(filePath.ToString());

                        using (var stream = System.IO.File.Create(filePath))
                        {
                            Console.WriteLine("Path");
                            Console.WriteLine(stream);
                            await formFile.CopyToAsync(stream);
                        }
                        return filePath.ToString();
                        //  await _context.Database.ExecuteSqlRawAsync(@$"INSERT IGNORE into files (Name, FilePath) VALUES ('{filename}','{filePath.ToString()}');");
                    }
                }
                //  return Ok(new { count = files.Count, size });
                return "";
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return "";
                //return BadRequest(new { error = e.ToString() });
            }
        }
    }
}