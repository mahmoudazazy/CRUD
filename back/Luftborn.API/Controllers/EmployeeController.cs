using Luftborn.Application.Commands.Employees;
using Luftborn.Application.Models;
using Luftborn.Application.Responses.Employees;
using Luftborn.Application.Services;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Net;

namespace Luftborn.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : BaseController
    {

        private readonly IMediator _mediator;
        private readonly IEmployeeQuery _employeeQuery;
        public EmployeeController(IMediator mediator, IEmployeeQuery employeeQuery) : base()
        {
            _mediator = mediator;
            _employeeQuery = employeeQuery;
        }

        [Route("Save")]
        [HttpPost]
        public async Task<Result> Create([FromBody] SaveEmployeeCommand saveEmployeeCommand)
        {
            var result = await _mediator.Send(saveEmployeeCommand);
            if (result.Succeeded)
                return result;

            return base.Problem(result.Errors[0]);
        }

        [Route("{id}/Edit")]
        [HttpPost]
        public async Task<Result> Edit(int id, [FromBody] SaveEmployeeCommand saveEmployeeCommand)
        {
            saveEmployeeCommand.Id = id;
            var result = await _mediator.Send(saveEmployeeCommand);
            if (result.Succeeded)
                return result;

            return base.Problem(result.Errors[0]);
        }

        [HttpGet]
        [Route("all")]
        public async Task<ActionResult<List<EmployeesDto>>> GetAll()
        {
            return Ok(await _employeeQuery.GetAllAsync());
        }

        [HttpGet]
        [Route("GetAllWithPager")]
        public IActionResult GetAllWithPager(int page, int pageSize, string? name = null, string? email = null)
        {
            int total = 0;
            return Ok(_employeeQuery.GetAllWithPager(page, pageSize, out total, name, email));
        }

        [HttpGet]
        [ProducesResponseType(typeof(EmployeesDto), (int)HttpStatusCode.OK)]
        [Route("{id:int}/Get")]
        public async Task<ActionResult<EmployeesDto>> GetById(int id)
        {
            var user = await _employeeQuery.GetByIdAsync(id);
            if (user == null)
                return NotFound();

            return Ok(user);
        }

        [Route("{id}/Remove")]
        [HttpDelete]
        public async Task<Result> Remove(int id)
        {
            var result = await _mediator.Send(new DeleteEmployeeCommand(id));
            if (result.Succeeded)
                return result;

            return base.Problem(result.Errors[0]);
        }
    }
}
