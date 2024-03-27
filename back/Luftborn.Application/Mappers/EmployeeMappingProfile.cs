using AutoMapper;
using Luftborn.Application.Responses;
using Luftborn.Application.Responses.Employees;
using Luftborn.Core.Entities;

namespace Luftborn.Application.Mappers;

public class EmployeeMappingProfile : Profile
{
    public EmployeeMappingProfile()
    { 
        CreateMap<Employee, EmployeesDto>().ReverseMap();
    }
}