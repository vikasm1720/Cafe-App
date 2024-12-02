using CafeOps.DAL;
using CafeOps.DAL.Repositories.Interfaces;
using MediatR;

namespace CafeOps.Logic
{
    public record UpdateEmployeeCommand(string Id, string Name, string EmailAddress, string PhoneNumber, string Gender, DateTime StartDate, Guid? AssignedCafeId) : IRequest<Unit>;
    public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand, Unit>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ICafeRepository _cafeRepository;

        public UpdateEmployeeCommandHandler(IEmployeeRepository employeeRepository, 
            ICafeRepository cafeRepository)
        {
            _employeeRepository = employeeRepository;
            _cafeRepository = cafeRepository;
        }

        public async Task<Unit> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employee = await _employeeRepository.GetByIdAsync(request.Id);
            if (employee == null)
            {
                throw new KeyNotFoundException($"Employee with ID '{request.Id}' not found.");
            }

            var cafe = request.AssignedCafeId != null
                ? await _cafeRepository.GetByIdAsync(request.AssignedCafeId)
                : null;

            if (request.AssignedCafeId != null && cafe == null)
            {
                throw new KeyNotFoundException($"Cafe with ID '{request.AssignedCafeId}' not found.");
            }

            employee.Name = request.Name;
            employee.EmailAddress = request.EmailAddress;
            employee.PhoneNumber = request.PhoneNumber;
            employee.Gender = request.Gender;
            employee.StartDate = request.StartDate;
            employee.CafeId = request.AssignedCafeId;

            await _employeeRepository.UpdateAsync(employee);
            return Unit.Value;
        }
    }

}
