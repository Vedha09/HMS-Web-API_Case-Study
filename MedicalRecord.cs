using System.ComponentModel.DataAnnotations;

namespace AmazeCare.Models
{
    public class MedicalRecord
    {
        [Key]
        public int MedicalRecordId { get; set; }
        [Required]
        public string Treatment { get; set; }
        public int PatientId { get; set; }
        public Patient Patient { get; set; }
    }
}
