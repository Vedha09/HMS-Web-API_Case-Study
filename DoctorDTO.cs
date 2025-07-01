using AmazeCare.Models;

namespace AmazeCare.DTOs
{
    public class DoctorDTO
    {
        public int DoctorId { get; set; }
        public string DoctorName { get; set; }
        public int Experience { get; set; }
        public string Qualification { get; set; }
        public string Specialization { get; set; }
    }
}
