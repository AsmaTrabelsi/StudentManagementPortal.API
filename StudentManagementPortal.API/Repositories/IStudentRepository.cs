using StudentManagementPortal.API.Models;

namespace StudentManagementPortal.API.Repositories
{
    public interface IStudentRepository
    {
        Task<List<Student>> GetStudentsAsync();

        Task<Student> GetStudentByIdAsync(Guid id);

        Task<List<Gender>> GetGendersAsync();

        Task<bool> Existe(Guid id);

        Task<Student> UpdateStudent(Guid id, Student student);
        Task<Student> DeleteStudent(Guid studentId);
        Task<Student> AddStudent(Student request);
        Task<bool> UpdateProfileImage(Guid studentId, string profileImageUrl);
    }
}
