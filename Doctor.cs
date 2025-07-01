using System.ComponentModel.DataAnnotations;

namespace AmazeCare.Models
{
    public class Doctor
    {
        [Key]
        public int DoctorId { get; set; }
        [Required]
        public string DoctorName { get; set; }
        public int Experience { get; set; }
        public string Qualification { get; set; }
        public string Specialization { get; set; }
        public ICollection<Appointment> Appointments { get; set; }
    }
}
