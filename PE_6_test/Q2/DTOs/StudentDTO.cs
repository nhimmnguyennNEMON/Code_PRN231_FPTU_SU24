using System.ComponentModel.DataAnnotations;

namespace Q2.DTOs
{
    public class StudentDTO
    {
        [Key]
        public int Id { get; set; }
        public string FullName { get; set; } = null!;
        public string Gender { get; set; }
        public string Dob { get; set; }

        public string LecturerName { get; set; }
        public int Age { get; set; }

    }
}
