namespace TaskingoAPI.Dto.User
{
    public class UserLoginDto
    {
        public string Email { get; set; }
        public string PasswordHashed { get; set; }
    }
}
