using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StudentManagementPortal.API.Models.DomainModels;
using StudentManagementPortal.API.Repositories;

namespace StudentManagementPortal.API.Controllers
{
    [ApiController]
    
    public class StudentController : Controller
    {

        private readonly IStudentRepository studentRepository;
        private readonly IMapper mapper;

        public StudentController(IStudentRepository studentRepository,IMapper mapper)
        {
            this.studentRepository = studentRepository;
            this.mapper = mapper;
        }
        [HttpGet]
        [Route("[controller]")]
        public async Task<IActionResult> GetStudentsAsync()
        {
           var students =  await studentRepository.GetStudentsAsync();
            
            return Ok(mapper.Map<List<Student>>(students));
        }
    }
}
