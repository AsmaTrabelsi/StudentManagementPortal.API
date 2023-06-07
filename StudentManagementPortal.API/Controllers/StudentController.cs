using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StudentManagementPortal.API.Models;
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
            
            return Ok(mapper.Map<List<Models.Student>>(students));
        }
        [HttpPut]
        [Route("[controller]/{studentId:guid}")]
        public async Task<IActionResult> UpdateStudentAsync([FromRoute] Guid studentId, [FromBody] UpdateStudentRequest request)
        {
            if(await studentRepository.Existe(studentId))
            {
              var updatedStudent= await  studentRepository.UpdateStudent(studentId,mapper.Map<Models.Student>(request));
                if (updatedStudent != null)
                {
                    return Ok(mapper.Map<Models.Student>(updatedStudent));
                }

            }
          
                return NotFound();
           
        }

        [HttpGet]
        [Route("[controller]/{studentId:guid}")]
        public async Task<IActionResult> GetStudentByIdAsync([FromRoute] Guid studentId)
        {
            var student = await studentRepository.GetStudentByIdAsync(studentId);
            if (student == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<Models.Student>(student));
        }
        [HttpDelete]
        [Route("[controller]/{studentId:guid}")]
        public async Task<IActionResult> DeleteStudentAsync([FromRoute] Guid studentId)
        {
            if (await studentRepository.Existe(studentId))
            {
                var student = await studentRepository.DeleteStudent(studentId);
                return Ok(mapper.Map<Models.Student>(student));
            }

            return NotFound();
        }

        [HttpPost]
        [Route("[controller]/Add")]
        public async Task<IActionResult> AddStudentAsync([FromBody] AddStudentRequest request)
        {
            var student = await studentRepository.AddStudent(mapper.Map<Models.Student>(request));
            return CreatedAtAction(nameof(GetStudentByIdAsync), new { studentId = student.Id },
                mapper.Map<Models.Student>(student));
        }
        /*
        [HttpPost]
        [Route("[controller]/{studentId:guid}/upload-image")]
        public async Task<IActionResult> UploadImage([FromRoute] Guid studentId, IFormFile profileImage)
        {
            var validExtensions = new List<string>
            {
               ".jpeg",
               ".png",
               ".gif",
               ".jpg"
            };

            if (profileImage != null && profileImage.Length > 0)
            {
                var extension = Path.GetExtension(profileImage.FileName);
                if (validExtensions.Contains(extension))
                {
                    if (await studentRepository.Existe(studentId))
                    {
                        var fileName = Guid.NewGuid() + Path.GetExtension(profileImage.FileName);

                        var fileImagePath = await imageRepository.Upload(profileImage, fileName);

                        if (await studentRepository.UpdateProfileImage(studentId, fileImagePath))
                        {
                            return Ok(fileImagePath);
                        }

                        return StatusCode(StatusCodes.Status500InternalServerError, "Error uploading image");
                    }
                }

                return BadRequest("This is not a valid Image format");
            }

            return NotFound();
        }*/
    }
}

