using AspNetCoreCRUD.Models.ViewModels;

namespace AspNetCoreCRUD.Repository.Interfaces
{
    public interface IStudentRepository
    {
        Task<List<StudentRegistrationDto>> GetListAsync();
        Task<bool> CreateOrUpdateAsync(StudentRegistrationDto studentData);
        Task<StudentRegistrationDto?> GetByIdAsync(int id);
        Task<bool> RemoveByIdAsync(int id);
    }
}
