using Microsoft.AspNetCore.Mvc;

namespace ExamSeatingArrangement2020.Dtos
{
    public class ReturnFullDetailsOfUser
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Role { get; set; }
        public FileContentResult ProfilePic { get; set; }
        public string Token { get; set; }
        public bool IsAuthorized { get; set; }
    }
}