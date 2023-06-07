using Microsoft.EntityFrameworkCore;
using StudentManagementPortal.API.Data;
using StudentManagementPortal.API.Models;
using StudentManagementPortal.API.Repositories;

namespace StudentManagementPortal.API.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly StudentAdminContext context;
        public StudentRepository(StudentAdminContext context)
        {
            this.context = context;
        }

        public  async Task<bool> Existe(Guid id)
        {
            return await context.Student.AnyAsync(x => x.Id == id);
        }

        public async Task<List<Gender>> GetGendersAsync()
        {
            return await context.Gender.ToListAsync();
        }

        public async Task<Student> GetStudentByIdAsync(Guid id)
        {
            return await context.Student.Include(nameof(Gender)).Include(nameof(Address)).
                FirstOrDefaultAsync(x=> x.Id == id);
        }

        public async Task<List<Student>> GetStudentsAsync()
        {
           return await context.Student.Include(nameof(Gender)).Include(nameof(Address)).ToListAsync();
        }

        public  async Task<Student> UpdateStudent(Guid id, Student student)
        {
            var existingStudent = await GetStudentByIdAsync(id);
            if(existingStudent != null)
            {
                existingStudent.FirstName = student.FirstName;
                existingStudent.LastName = student.LastName;
                existingStudent.DateOfBirth = student.DateOfBirth;
                existingStudent.GenderId = student.GenderId;
                existingStudent.Mobile = student.Mobile;
                existingStudent.Email = student.Email;
                existingStudent.Address.PhysicalAddress= student.Address.PhysicalAddress;
                existingStudent.Address.PostalAddress= student.Address.PostalAddress;
                await context.SaveChangesAsync();
                return existingStudent;
            }
            return null;
        }

        public async Task<Student> DeleteStudent(Guid studentId)
        {
            var student = await GetStudentByIdAsync(studentId);

            if (student != null)
            {
                context.Student.Remove(student);
                await context.SaveChangesAsync();
                return student;
            }

            return null;
        }

        public async Task<Student> AddStudent(Student request)
        {
            var student = await context.Student.AddAsync(request);
            await context.SaveChangesAsync();
            return student.Entity;
        }

        public async Task<bool> UpdateProfileImage(Guid studentId, string profileImageUrl)
        {
            var student = await GetStudentByIdAsync(studentId);

            if (student != null)
            {
                student.ProfileImageUrl = profileImageUrl;
                await context.SaveChangesAsync();
                return true;
            }

            return false;
        }
    }
}
