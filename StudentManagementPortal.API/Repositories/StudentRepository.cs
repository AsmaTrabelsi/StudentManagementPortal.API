using Microsoft.EntityFrameworkCore;
using StudentManagementPortal.API.Data;
using StudentManagementPortal.API.Models;

namespace StudentManagementPortal.API.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly StudentAdminContext context;
        public StudentRepository(StudentAdminContext context)
        {
            this.context = context;
        }
        async Task<List<Student>> IStudentRepository.GetStudentsAsync()
        {
           return await context.Student.Include(nameof(Gender)).Include(nameof(Address)).ToListAsync();
        }
    }
}
