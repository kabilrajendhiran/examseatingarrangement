namespace ExamSeatingArrangement2020.Models
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string Role { get; set; }
        public string ImagePath { get; set; }
        public bool IsAuthorized { get; set; }
    }
}