namespace Q2.DTOs
{
    public class StudentDetailDTO
    {
        public int Id { get; set; }
        public string FullName { get; set; } = null!;
        public string Gender { get; set; }
        public string Dob { get; set; }

        public string LecturerName { get; set; }
        public int Age { get; set; }

        public List<string> Subjects { get; set; }
        public List<string> Classes { get; set; }

        public double CPA { get; set; }

    }
}
