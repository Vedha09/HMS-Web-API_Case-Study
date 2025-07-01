using AmazeCare.DbContexts;
using AmazeCare.Models;
using Microsoft.EntityFrameworkCore;

namespace AmazeCare.Repository.Implementation
{
    public class AppointmentServiceImpl : IAppointmentService
    {
    private readonly ApplicationDbContext _context;
        public AppointmentServiceImpl(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Appointment> GetAllAppointments() => _context.Appointments.Include(a => a.Patient).Include(a => a.Doctor).ToList();
        public Appointment GetAppointmentById(int id) => _context.Appointments.Find(id);
        public void AddAppointment(Appointment appt) { _context.Appointments.Add(appt); _context.SaveChanges(); }
        public void UpdateAppointment(Appointment appt) { _context.Appointments.Update(appt); _context.SaveChanges(); }
        public void DeleteAppointment(int id)
        {
            var a = _context.Appointments.Find(id);
            if (a != null) { _context.Appointments.Remove(a); _context.SaveChanges(); }
        }
    }
}
