using AmazeCare.DbContexts;
using AmazeCare.DTOs;
using AmazeCare.Models;
using AmazeCare.Repository.Implementation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AmazeCare.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AppointmentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAppointments()
        {
            var appointments = await _context.Appointments
                .Include(a => a.Patient)
                .Include(a => a.Doctor)
                .ToListAsync();

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
                    a.Patient.PatientName
                },
                Doctor = new
                {
                    a.Doctor.DoctorId,
                    a.Doctor.DoctorName
                }
            });

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAppointmentById(int id)
        {
            var appointment = await _context.Appointments
                .Include(a => a.Patient)
                .Include(a => a.Doctor)
                .FirstOrDefaultAsync(a => a.AppointmentId == id);

            if (appointment == null)
                return NotFound("Appointment not found.");

            return Ok(new
            {
                appointment.AppointmentId,
                appointment.AppointmentDate,
                appointment.Status,
                appointment.Prescription,
                appointment.BillAmount,
                Patient = new
                {
                    appointment.Patient.PatientId,
                    appointment.Patient.PatientName
                },
                Doctor = new
                {
                    appointment.Doctor.DoctorId,
                    appointment.Doctor.DoctorName
                }
            });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAppointment(int id, [FromBody] Appointment updatedAppointment)
        {
            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment == null)
                return NotFound("Appointment not found.");

            appointment.Status = updatedAppointment.Status;
            appointment.ConsultationFeeDetails = updatedAppointment.ConsultationFeeDetails;
            appointment.Prescription = updatedAppointment.Prescription;
            appointment.BillAmount = updatedAppointment.BillAmount;
            await _context.SaveChangesAsync();

            return Ok("Appointment updated successfully.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAppointment(int id)
        {
            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment == null)
                return NotFound("Appointment not found.");

            _context.Appointments.Remove(appointment);
            await _context.SaveChangesAsync();

            return Ok("Appointment deleted successfully.");
        }
    }
}
