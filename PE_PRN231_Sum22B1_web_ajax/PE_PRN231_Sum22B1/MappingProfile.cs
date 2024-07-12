using AutoMapper;
using PE_PRN231_Sum22B1.DTO;
using PE_PRN231_Sum22B1.Models;

namespace PE_PRN231_Sum22B1
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<Order,OrderDTO>().ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Customer.CompanyName))
                .ForMember(dest => dest.EmployeeName, opt => opt.MapFrom(src => src.Employee.FirstName + " " + src.Employee.LastName))
                .ForMember(dest => dest.EmployeeDepartmentId, opt => opt.MapFrom(src => src.Employee.DepartmentId))
                .ForMember(dest => dest.EmployeeDepartmentName, opt => opt.MapFrom(src => src.Employee.Department.DepartmentName));
        }
    }
}
