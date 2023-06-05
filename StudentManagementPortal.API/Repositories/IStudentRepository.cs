using StudentManagementPortal.API.Models;

namespace StudentManagementPortal.API.Repositories
{
    public interface IStudentRepository
    {
        Task<List<Student>> GetStudentsAsync();

    }
}
