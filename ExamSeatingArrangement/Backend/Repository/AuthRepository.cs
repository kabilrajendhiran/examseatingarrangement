using ExamSeatingArrangement2020.Data;
using ExamSeatingArrangement2020.Dtos;
using ExamSeatingArrangement2020.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace ExamSeatingArrangement2020.Repository
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _context;
        private readonly IWebHostEnvironment _env;

        public AuthRepository(DataContext context,IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<User> Login(string username, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserName == username);

            if (user == null)
                return null;

            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return null;

            return user;
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i]) return false;
                }
            }
            return true;
        }

        public async Task<User> Register(User user, string password, string filepath)
        {
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            user.ImagePath = filepath;

            await _context.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public async Task<bool> ResetPassword(string userName, string password, bool isAuthorized)
        {
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);
            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserName == userName);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            user.IsAuthorized = isAuthorized;
            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> UserExists(string username)
        {
            if (await _context.Users.AnyAsync(x => x.UserName == username))
                return true;

            return false;
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

        public async Task<string> StoreProfilePicPath(IFormFile files)
        {
            try
            {
                var formFile = files;
                if (formFile != null)
                {
                    string filename = formFile.FileName;

                    // var filename = Path.GetRandomFileName();

                    var filePathraw1 = Path.Combine(_env.WebRootPath, "userpics");

                    if (!Directory.Exists(filePathraw1))
                    {
                        Directory.CreateDirectory(filePathraw1);
                    }

                    var filePath = Path.Combine(filePathraw1, this.RandomPathGenerator() + filename);

                  //  var filePathraw = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Storage\\ProfilePic", this.RandomPathGenerator() + filename);

                  //  var filePath = filePathraw.Replace("\\", "\\\\");

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

        public byte[] FileFactory(User User)
        {
            byte[] file = null;

            if (User.ImagePath.Trim() != "" && User.ImagePath != null)
            {
                file = File.ReadAllBytes(User.ImagePath);
            }
            else
            {
                var filePathraw1 = Path.Combine(_env.WebRootPath, "userpics");

                if (!Directory.Exists(filePathraw1))
                {
                    Directory.CreateDirectory(filePathraw1);
                }

                var filePathraw = Path.Combine(filePathraw1, "avatar.png");

              //  var filePathraw = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Storage\\ProfilePic", "avatar.png");
              //  var filePath = filePathraw.Replace("\\", "\\\\");
                file = File.ReadAllBytes(filePathraw);
            }

            return file;
        }

        public async Task<List<User>> Users()
        {
            var users = await _context.Users.ToListAsync();
            return users;
        }

        public async Task<bool> Updateuser(UserForUpdateDto user)
        {
            var User = await _context.Users.FirstOrDefaultAsync(x => x.Id == user.Id);
            User.IsAuthorized = user.IsAuthorized;
            User.Role = user.Role;
            User.UserName = user.UserName;

            var res = await _context.SaveChangesAsync();
            if (res > 0)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteUser(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);

            if (File.Exists(user.ImagePath))
            {
                File.Delete(user.ImagePath);
            }

            _context.Users.Remove(user);
            var res = await _context.SaveChangesAsync();
            if (res > 0)
            {
                return true;
            }
            return false;
        }
    }
}