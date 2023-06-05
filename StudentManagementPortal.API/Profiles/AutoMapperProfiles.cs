using AutoMapper;
using StudentManagementPortal.API.Models;
using dtoModel =StudentManagementPortal.API.Models.DomainModels;

namespace StudentManagementPortal.API.Profiles
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Models.Student,dtoModel.Student>().ReverseMap();
            CreateMap<Models.Gender,dtoModel.Gender>().ReverseMap();
            CreateMap<Models.Address,dtoModel.Address>().ReverseMap();
        }
    }
}
