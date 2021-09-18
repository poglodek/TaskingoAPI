namespace TaskingoAPI.Dto.User
{
    public class UserDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public bool IsOnline { get; set; }
        public int Phone { get; set; }
        public string Address { get; set; }
    }
}
