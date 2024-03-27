using FluentValidation;
using Luftborn.Application.Commands.Employees;

namespace Luftborn.Application.Validators.Employees
{
    public class SaveEmployeeCommandValidator : AbstractValidator<SaveEmployeeCommand>
    {
        public SaveEmployeeCommandValidator()
        {
            RuleFor(a => a.FirstName).NotEmpty().NotNull().WithMessage("First Name is required");
            RuleFor(a => a.LastName).NotEmpty().NotNull().WithMessage("Last Name is required");
            RuleFor(a => a.EmailAddress).NotEmpty().NotNull().WithMessage("Email Address is required");
            RuleFor(a => a.Salary).NotEmpty().NotNull().WithMessage("Salary is required");
        }
    }
}
