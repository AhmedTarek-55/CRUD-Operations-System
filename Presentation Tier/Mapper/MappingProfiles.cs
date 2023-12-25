using AutoMapper;
using Data_Access_Tier.Entities;
using Presentation_Tier.Models;

namespace Presentation_Tier.Mapper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Employee, EmployeeViewModel>().ReverseMap();
            CreateMap<Department, DepartmentViewModel>().ReverseMap();
        }
    }
}
