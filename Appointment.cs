using System.ComponentModel.DataAnnotations;

namespace AmazeCare.Models
{
    public class Appointment
    {
        [Key]
        public int AppointmentId { get; set; }
        [Required]
        public DateTime AppointmentDate { get; set; }
        public string Status { get; set; }
        public string ConsultationFeeDetails { get; set; }
        public string Prescription { get; set; }
        public int BillAmount { get; set; }
        public int PatientId { get; set; }
        public Patient Patient { get; set; }
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }
    }
}
