using AmazeCare.DbContexts;
using AmazeCare.DTOs;
using AmazeCare.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AmazeCare.Repository.Implementation;

namespace AmazeCare.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PatientsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("MedicalRecords/{patientId}")]
        public async Task<IActionResult> GetMedicalRecords(int patientId)
        {
            var records = await _context.MedicalRecords
                .Include(r => r.Patient)
                .Where(r => r.PatientId == patientId)
                .ToListAsync();

            if (records == null || !records.Any())
                return NotFound("No medical records found for this patient.");

            var result = records.Select(r => new
            {
                r.MedicalRecordId,
                r.Treatment,
                Patient = new
                {
                    r.Patient.PatientId,
                    r.Patient.PatientName,
                    r.Patient.Symptoms,
                    r.Patient.PatientPhoneNumber
                }
            });

            return Ok(result);
        }

        [HttpGet("Appointments/{patientId}")]
        public async Task<IActionResult> GetAppointments(int patientId)
        {
            var appointments = await _context.Appointments
                .Include(a => a.Doctor)
                .Where(a => a.PatientId == patientId)
                .ToListAsync();

            if (appointments == null || !appointments.Any())
                return NotFound("No appointments found for this patient.");

            var result = appointments.Select(a => new
            {
                a.AppointmentId,
                a.AppointmentDate,
                a.Status,
                a.Prescription,
                Doctor = new
                {
                    a.Doctor.DoctorId,
                    a.Doctor.DoctorName,
                    a.Doctor.Specialization
                }
            });

            return Ok(result);
        }
    }
}
