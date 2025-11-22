namespace WebApplication3.DTOs
{
    public class StudentDto
    {
        public string Name { get; set; } = string.Empty;

        public int Age { get; set; }

        public string Address { get; set; } = string.Empty;

        public string State { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public string Subjects { get; set; } = string.Empty;

        public IFormFile? Photo { get; set; }
    }

}
