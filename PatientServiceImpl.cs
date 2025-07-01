using AmazeCare.DbContexts;
using AmazeCare.Models;

namespace AmazeCare.Repository.Implementation
{
    public class PatientServiceImpl : IPatientService
    {
        private readonly ApplicationDbContext _context;
        public PatientServiceImpl(ApplicationDbContext context) => _context = context;

        public IEnumerable<Patient> GetAllPatients() => _context.Patients.ToList();
        public Patient GetPatientById(int id) => _context.Patients.Find(id);
        public void AddPatient(Patient patient) { _context.Patients.Add(patient); _context.SaveChanges(); }
        public void UpdatePatient(Patient patient) { _context.Patients.Update(patient); _context.SaveChanges(); }
        public void DeletePatient(int id) { var p = _context.Patients.Find(id); if (p != null) { _context.Patients.Remove(p); _context.SaveChanges(); } }
    }
}
