using Luftborn.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luftborn.Application.Commands.Employees
{
    public record DeleteEmployeeCommand(int Id) : IRequest<Result>;
}
