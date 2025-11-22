namespace WebApplication3.DTOs
{
    public class AuthDto
    {
        public record RegisterDto(string Username, string Password);
        public record LoginDto(string Username, string Password);

    }
}
