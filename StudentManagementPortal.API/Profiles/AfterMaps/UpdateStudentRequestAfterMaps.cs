using AutoMapper;
using StudentManagementPortal.API.Models.DomainModels;

namespace StudentManagementPortal.API.Profiles.AfterMaps
{
    public class UpdateStudentRequestAfterMaps : IMappingAction<UpdateStudentRequest, Models.Student>
    {
        void IMappingAction<UpdateStudentRequest, Models.Student>.Process(UpdateStudentRequest source, Models.Student destination, ResolutionContext context)
        {
            destination.Address = new Models.Address()
            {
                PhysicalAddress = source.PhysicalAddress,
                PostalAddress = source.PostalAddress,
            };
          
        }
    }
}
