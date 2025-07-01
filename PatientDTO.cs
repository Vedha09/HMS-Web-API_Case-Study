namespace AmazeCare.DTOs
{
    public class PatientDTO
    {
        public int PatientId { get; set; }
        public string PatientName { get; set; }
        public DateTime PatientDOB { get; set; }
        public int PatientAge { get; set; }
        public string Gender { get; set; }
        public string PatientEmail { get; set; }
        public string PatientPhoneNumber { get; set; }
        public string Symptoms { get; set; }
    }
}
