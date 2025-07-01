using AmazeCare.DbContexts;
using AmazeCare.Models;
using Microsoft.EntityFrameworkCore;

namespace AmazeCare.Repository.Implementation
{
    public class MedicalRecordServiceImpl : IMedicalRecordService
    {
        private readonly ApplicationDbContext _context;
        public MedicalRecordServiceImpl(ApplicationDbContext context) => _context = context;

        public IEnumerable<MedicalRecord> GetAllRecords() => _context.MedicalRecords.Include(r => r.Patient).ToList();
        public MedicalRecord GetRecordById(int id) => _context.MedicalRecords.Find(id);
        public void AddRecord(MedicalRecord rec) { _context.MedicalRecords.Add(rec); _context.SaveChanges(); }
        public void UpdateRecord(MedicalRecord rec) { _context.MedicalRecords.Update(rec); _context.SaveChanges(); }
        public void DeleteRecord(int id) { var r = _context.MedicalRecords.Find(id); if (r != null) { _context.MedicalRecords.Remove(r); _context.SaveChanges(); } }
    }
}
