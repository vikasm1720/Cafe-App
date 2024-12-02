using Azure.Core;
using CafeOps.DAL.Entities;
using CafeOps.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace CafeOps.DAL.Repositories
{
    public class CafeRepository : ICafeRepository
    {
        private readonly ApplicationDbContext _context;

        public CafeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Cafe cafe)
        {
            _context.Cafes.Add(cafe);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Cafe>> GetByLocationAsync(string? location)
        {

            var data = _context.Cafes.ToList();
            return string.IsNullOrEmpty(location)
                ? await _context.Cafes.Include(c => c.Employees).ToListAsync()
                : await _context.Cafes.Include(c => c.Employees)
                                       .Where(c => c.Location == location)
                                       .OrderByDescending(c => c.Employees.Count)
                                       .ToListAsync();
        }

        public async Task<Cafe?> GetByIdAsync(Guid? id)
        {
            return await _context.Cafes.Include(c => c.Employees)
                                       .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task UpdateCafe(Cafe cafe)
        {
            _context.Cafes.Update(cafe);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCafe(Cafe cafe)
        {
            _context.Cafes.Remove(cafe);
            await _context.SaveChangesAsync();
        }
    }
}
