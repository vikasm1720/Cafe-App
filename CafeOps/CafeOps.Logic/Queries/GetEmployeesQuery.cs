using CafeOps.DAL;
using CafeOps.DAL.Entities;
using CafeOps.Logic.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeOps.Logic
{
    public record GetEmployeesQuery(string? Cafe) : IRequest<List<EmployeeDto>>;
    public class GetEmployeesQueryHandler : IRequestHandler<GetEmployeesQuery, List<EmployeeDto>>
    {
        private readonly ApplicationDbContext _context;

        public GetEmployeesQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<EmployeeDto>> Handle(GetEmployeesQuery request, CancellationToken cancellationToken)
        {
            var query = _context.Employees.Include(e => e.Cafe).AsQueryable();

            if (!string.IsNullOrWhiteSpace(request.Cafe))
            {
                query = query.Where(e => e.Cafe != null && e.Cafe.Name == request.Cafe);
            }

            var employees = await query
                .Select(e => new EmployeeDto
                {
                    Id = e.Id,
                    Name = e.Name,
                    EmailAddress = e.EmailAddress,
                    PhoneNumber = e.PhoneNumber,
                    DaysWorked = EF.Functions.DateDiffDay(e.StartDate, DateTime.Now),
                    CafeId = e.Cafe != null ? e.Cafe.Id : null,
                    Cafe= e.Cafe != null ? e.Cafe.Name : null,
                    StartDate= e.StartDate.ToString("yyyy-MM-dd"),
                })
                .OrderByDescending(e => e.DaysWorked) 
                .ToListAsync(cancellationToken);

            return employees;
        }
    }

}
