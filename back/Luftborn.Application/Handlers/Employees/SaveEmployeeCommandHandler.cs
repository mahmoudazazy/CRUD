using Luftborn.Application.Commands.Employees;
using Luftborn.Application.Models; 
using MediatR;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Luftborn.Application.Behaviour;
using Luftborn.Infrastructure.Data;
using Luftborn.Core.Entities;

namespace Luftborn.Application.Handlers.Employees
{
    public class SaveEmployeeCommandHandler : IRequestHandler<SaveEmployeeCommand, Result>
    {
        private readonly ILogger<SaveEmployeeCommandHandler> _logger;
        private readonly LuftbornContext _context;

        public SaveEmployeeCommandHandler(ILogger<SaveEmployeeCommandHandler> logger, LuftbornContext context)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _context = context; 
        }

        public async Task<Result> Handle(SaveEmployeeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation(" Adding new Employee");
                if (request.Id == 0)
                {
                    var employee = new Employee(request.FirstName, request.LastName, request.EmailAddress, request.Salary);
                    var updatedCustomer = await _context.Employees.AddAsync(employee);
                    await _context.SaveChangesAsync();
                    _logger.LogInformation(" Employee saved successfully");
                }
                else
                {
                    var current = await _context.Employees
                                                .FirstOrDefaultAsync(a => a.Id == request.Id);

                    if (current != null)
                    {
                        current.Update( request.FirstName,request.LastName, request.EmailAddress,request.Salary);
                        _context.Employees.Update(current);
                        _logger.LogInformation($" Employee with Id {request.Id} updated");
                    }
                }

                await _context.SaveChangesAsync();
                return Result.Success();
            }
            catch (Exception ex)
            {
                await _context.RollbackTransactionAsync();
                _logger.LogError($" Failed to save Employee: {ex.Message} ");
                return Result.Failure(" Employee couldn't be save");
            }
        }

    }
}
