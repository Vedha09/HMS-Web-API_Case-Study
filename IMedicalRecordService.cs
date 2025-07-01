using AmazeCare.Models;

namespace AmazeCare.Repository.Implementation
{
    public interface IMedicalRecordService
    {
        IEnumerable<MedicalRecord> GetAllRecords();
        MedicalRecord GetRecordById(int id);
        void AddRecord(MedicalRecord record);
        void UpdateRecord(MedicalRecord record);
        void DeleteRecord(int id);
    }
}
