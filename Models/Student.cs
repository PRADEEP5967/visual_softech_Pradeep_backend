namespace WebApplication3.Models
{

    public class Student
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public int Age { get; set; }

        public string Address { get; set; } = string.Empty;

        public string State { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public byte[]? Photo { get; set; } // stored as base64

        public string Subjects { get; set; } = string.Empty; // comma-separated subjects
    }

}
