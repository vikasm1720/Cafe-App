using CafeOps.Logic;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CafeOps.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EmployeesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetEmployees([FromQuery] string? cafe)
        {
            var result = await _mediator.Send(new GetEmployeesQuery(cafe));
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmployee([FromBody] CreateEmployeeCommand command)
        {
            var validationResult = ValidateEmployeeInput(command.Gender, command.PhoneNumber, command.EmailAddress);
            if (validationResult != null)
            {
                return validationResult;
            }

            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(CreateEmployee), new { id = result }, result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateEmployee([FromBody] UpdateEmployeeCommand command)
        {
            var validationResult = ValidateEmployeeInput(command.Gender, command.PhoneNumber, command.EmailAddress);
            if (validationResult != null)
            {
                return validationResult;
            }

            await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(string id)
        {
            await _mediator.Send(new DeleteEmployeeCommand(id));
            return NoContent();
        }

        private IActionResult ValidateEmployeeInput(string gender, string phoneNumber, string emailAddress)
        {
            if (!IsValidGender(gender))
            {
                return BadRequest(new { Error = "Gender must be either 'Male' or 'Female'." });
            }

            if (!IsValidPhoneNumber(phoneNumber))
            {
                return BadRequest(new { Error = "Phone number must start with '9' or '8' and be exactly 8 digits." });
            }

            if (!IsValidEmail(emailAddress))
            {
                return BadRequest(new { Error = "Email address is invalid." });
            }

            return null;
        }


        private bool IsValidGender(string gender)
        {
            return gender.Equals("Male", StringComparison.OrdinalIgnoreCase) ||
                   gender.Equals("Female", StringComparison.OrdinalIgnoreCase);
        }
        private bool IsValidPhoneNumber(string phoneNumber)
        {
            return phoneNumber.Length == 8 && (phoneNumber.StartsWith("9") || phoneNumber.StartsWith("8"));
        }

        private bool IsValidEmail(string email)
        {            
            var emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return !string.IsNullOrEmpty(email) && System.Text.RegularExpressions.Regex.IsMatch(email, emailPattern);
        }
    }
}
