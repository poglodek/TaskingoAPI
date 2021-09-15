namespace TaskingoAPI.Dto.User
{
    public class UserCreatedDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string PasswordHashed { get; set; }
        public int Phone { get; set; }
        public string Address { get; set; }
    }
}
