using System.ComponentModel.DataAnnotations;

namespace AmazeCare.Models
{
    public class Patient
    {
        [Key]
        public int PatientId { get; set; }
        [Required]
        public string PatientName { get; set; }
        public DateTime PatientDOB { get; set; }
        public int PatientAge { get; set; }
        public string Gender { get; set; }
        public string PatientEmail { get; set; }
        public string PatientPhoneNumber { get; set; }
        public string Symptoms { get; set; }
        public ICollection<Appointment> Appointments { get; set; }
        public ICollection<MedicalRecord> MedicalRecords { get; set; }
    }
}
