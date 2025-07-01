using AmazeCare.Models;

namespace AmazeCare.Repository.Implementation
{
    public interface IAppointmentService
    {
        IEnumerable<Appointment> GetAllAppointments();
        Appointment GetAppointmentById(int id);
        void AddAppointment(Appointment appointment);
        void UpdateAppointment(Appointment appointment);
        void DeleteAppointment(int id);
    }
}
