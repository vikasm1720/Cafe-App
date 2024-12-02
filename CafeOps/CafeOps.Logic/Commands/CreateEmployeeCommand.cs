using CafeOps.DAL.Entities;
using CafeOps.DAL.Repositories.Interfaces;
using CafeOps.Logic.Helper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeOps.Logic
{
    public record CreateEmployeeCommand(string Id, string Name, string EmailAddress, string PhoneNumber, string Gender, Guid? CafeId, DateTime StartDate) : IRequest<string>;

    public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, string>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ICafeRepository _cafeRepository;
        private static readonly Random _random = new Random();

        public CreateEmployeeCommandHandler(IEmployeeRepository employeeRepository,
            ICafeRepository cafeRepository)
        {
            _employeeRepository = employeeRepository;
            _cafeRepository = cafeRepository;
        }

        public async Task<string> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            Cafe? assignedCafe = null;
            if (request.CafeId !=null)
            {
                assignedCafe = await _cafeRepository.GetByIdAsync(request.CafeId);
                if (assignedCafe == null)
                {
                    throw new KeyNotFoundException($"Café with ID '{request.CafeId}' not found.");
                }
            }
            var newEmployeeId = EmployeeIdGenerator.GenerateEmployeeId();
            var employee = new Employee
            {
                Id = newEmployeeId,
                Name = request.Name,
                EmailAddress = request.EmailAddress,
                PhoneNumber = request.PhoneNumber,
                Gender = request.Gender,
                CafeId = request.CafeId,
                StartDate = request.StartDate
            };

            await _employeeRepository.AddAsync(employee);
            return employee.Id;
        }
    }

}
