using AmazeCare.DbContexts;
using AmazeCare.Models;

namespace AmazeCare.Repository.Implementation
{
    public class DoctorServiceImpl : IDoctorService
    {
        private readonly ApplicationDbContext _context;
        public DoctorServiceImpl(ApplicationDbContext context) => _context = context;

        public IEnumerable<Doctor> GetAllDoctors() => _context.Doctors.ToList();
        public Doctor GetDoctorById(int id) => _context.Doctors.Find(id);
        public void AddDoctor(Doctor doctor) { _context.Doctors.Add(doctor); _context.SaveChanges(); }
        public void UpdateDoctor(Doctor doctor) { _context.Doctors.Update(doctor); _context.SaveChanges(); }
        public void DeleteDoctor(int id) { var doc = _context.Doctors.Find(id); if (doc != null) { _context.Doctors.Remove(doc); _context.SaveChanges(); } }
    }
}
