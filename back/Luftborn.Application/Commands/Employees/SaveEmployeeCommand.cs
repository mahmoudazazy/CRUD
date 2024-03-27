using Luftborn.Application.Models;
using MediatR;

namespace Luftborn.Application.Commands.Employees
{
    public class SaveEmployeeCommand : IRequest<Result>
    {
        public int Id { get; set; }
        public string FirstName { get; set; } 
        public string EmailAddress { get; set; }
        public string LastName { get; set; }
        public decimal Salary { get; set; } 
    }
}
