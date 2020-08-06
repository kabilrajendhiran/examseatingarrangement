using ExamSeatingArrangement2020.Dtos;
using ExamSeatingArrangement2020.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExamSeatingArrangement2020.Repository
{
    public interface IAuthRepository
    {
        Task<User> Register(User user, string password, string filepath);

        public Task<bool> ResetPassword(string userName, string password, bool isAuthorized);

        Task<User> Login(string username, string password);

        Task<bool> UserExists(string username);

        public string RandomPathGenerator();

        public Task<string> StoreProfilePicPath(IFormFile files);

        public Task<List<User>> Users();

        public byte[] FileFactory(User User);

        public Task<bool> Updateuser(UserForUpdateDto user);

        public Task<bool> DeleteUser(int id);
    }
}