using Microsoft.AspNetCore.Http;

namespace ExamSeatingArrangement2020.Dtos
{
    public class UserForRegisterDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public IFormFile ProfilePicture { get; set; }
        public string Role { get; set; }
    }
}