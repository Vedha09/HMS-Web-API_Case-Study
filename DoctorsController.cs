using AmazeCare.DbContexts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AmazeCare.Repository.Implementation;

namespace AmazeCare.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DoctorsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("Appointments/{doctorId}")]
        public async Task<IActionResult> GetAppointments(int doctorId)
        {
            var appointments = await _context.Appointments
                .Include(a => a.Patient)
                .Include(a => a.Doctor)
                .Where(a => a.DoctorId == doctorId)
                .ToListAsync();

            if (appointments == null || !appointments.Any())
                return NotFound("No appointments found for the given doctor.");

            var result = appointments.Select(a => new
            {
                a.AppointmentId,
                a.AppointmentDate,
                a.Status,
                a.ConsultationFeeDetails,
                a.Prescription,
                a.BillAmount,
                Patient = new
                {
                    a.Patient.PatientId,
                    a.Patient.PatientName,
                    a.Patient.Gender,
                    a.Patient.Symptoms
                },
                Doctor = new
                {
                    a.Doctor.DoctorId,
                    a.Doctor.DoctorName,
                    a.Doctor.Specialization
                }
            });

            return Ok(result);
        }

        [HttpPut("Update-Consultation")]
        public async Task<IActionResult> UpdateConsultation(int appointmentId, string details, string prescription)
        {
            var appointment = await _context.Appointments.FindAsync(appointmentId);
            if (appointment == null)
            {
                return NotFound("Appointment not found.");
            }

            appointment.ConsultationFeeDetails = details;
            appointment.Prescription = prescription;
            appointment.Status = "Completed";

            await _context.SaveChangesAsync();

            return Ok("Consultation updated successfully.");
        }
    }
}
