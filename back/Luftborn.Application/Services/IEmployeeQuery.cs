using AutoMapper;
using Luftborn.Application.Behaviour;
using Luftborn.Application.Responses.Employees;
using Luftborn.Application.Responses.Wrappers; 
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Data;
using Luftborn.Core.Entities;
using Luftborn.Infrastructure.Data;

namespace Luftborn.Application.Services
{
    public interface IEmployeeQuery
    {
        Task<List<Employee>> GetAllAsync();
        Task<EmployeesDto> GetByIdAsync(int id);
        Response<DataSourceResult> GetAllWithPager(int page, int pageSize, out int total, string? name, string? email);
    }

    public class EmployeeQuery : IEmployeeQuery
    {
        private readonly LuftbornContext _luftbornContext;
        private readonly IMapper _mapper;

        public EmployeeQuery(LuftbornContext LuftbornContext, IMapper mapper)
        {
            _luftbornContext = LuftbornContext ?? throw new ArgumentNullException(nameof(LuftbornContext));
            _mapper = mapper;
        }

        public async Task<List<Employee>> GetAllAsync()
        {
            return await _luftbornContext.Employees
                                    .AsNoTracking()
                                    .ToListAsync();
        }

        public async Task<EmployeesDto> GetByIdAsync(int id)
        {
            var user = await _luftbornContext.Employees
                                    .Where(a => a.Id == id)
                                    .FirstOrDefaultAsync();

            return _mapper.Map<EmployeesDto>(user);
        }

        public Response<DataSourceResult> GetAllWithPager(int page, int pageSize, out int total, string? name, string? email)
        {
            var employeeDtos = new List<EmployeesDto>();
            try
            {
                var employees = GetAllPaged(page, pageSize, out total, name, email);

                if (employees.Count() > 0)
                    employeeDtos = _mapper.Map<List<EmployeesDto>>(employees);

                var result = new DataSourceResult
                {
                    Data = employeeDtos,
                    TotalItems = total,
                    PageIndex = page
                };

                return result;
            }
            catch (Exception ex)
            {
                total = 0;
                return new Response<DataSourceResult>(ex.Message);
            }
        }

        private IQueryable<Employee> GetAllPaged(int page, int pageSize, out int total, string? name, string? email)
        {
            var employees = _luftbornContext.Employees
                                            .AsNoTracking();

            if (!string.IsNullOrEmpty(name))
                employees = employees.Where(x => x.FirstName.ToLower().Contains(name.Trim().ToLower()));

            if (!string.IsNullOrEmpty(email))
                employees = employees.Where(x => x.EmailAddress.Trim().ToLower() == email.Trim().ToLower());

            total = employees.Count();
            var pagedUsers = employees.OrderByDescending(x => x.Id).Skip(page * pageSize).Take(pageSize);

            return pagedUsers;
        }
    }
}
