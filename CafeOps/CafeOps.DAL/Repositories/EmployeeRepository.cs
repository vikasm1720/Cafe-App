using CafeOps.DAL.Entities;
using CafeOps.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeOps.DAL.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext _context;

        public EmployeeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Employee employee)
        {
            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();
        }

        public async Task<Employee?> GetByIdAsync(string id)
        {
            return await _context.Employees
                                 .Include(e => e.Cafe)
                                 .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<List<Employee>> GetByCafeAsync(Guid cafeId)
        {
            return await _context.Employees
                                 .Include(e => e.Cafe)
                                 .Where(e => e.CafeId == cafeId)
                                 .ToListAsync();
        }

        public async Task UpdateAsync(Employee employee)
        {
            _context.Employees.Update(employee);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Employee employee)
        {           
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();

        }
    }
}
