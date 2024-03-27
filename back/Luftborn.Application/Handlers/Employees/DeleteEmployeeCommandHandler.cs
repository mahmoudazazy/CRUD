using Luftborn.Application.Commands.Employees;
using Luftborn.Application.Models;
using Luftborn.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Luftborn.Application.Handlers.Employees
{

    public class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand, Result>
    {
        private readonly ILogger<DeleteEmployeeCommandHandler> _logger;
        private readonly LuftbornContext _context; 
        public DeleteEmployeeCommandHandler(ILogger<DeleteEmployeeCommandHandler> logger, LuftbornContext context)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _context = context; 
        }

        public async Task<Result> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation(" Delete Employee");
                var current = await _context.Employees
                                            .FirstOrDefaultAsync(a => a.Id == request.Id);

                if (current != null)
                {
                    _context.Employees.Remove(current);
                    _logger.LogInformation($" Employee with Id {request.Id} has been deleted");
                    await _context.SaveChangesAsync();
                }

                return Result.Success();
            }
            catch (Exception ex)
            {
                _logger.LogError($" Failed to delete Employee : {ex.Message} ");
                return Result.Failure(" Employee couldn't be deleted");
            }
        }
    }
}
