using CafeOps.DAL.Entities;

namespace CafeOps.DAL.Repositories.Interfaces
{
    public interface ICafeRepository
    {
        Task AddAsync(Cafe cafe);
        Task<List<Cafe>> GetByLocationAsync(string? location);
        Task<Cafe?> GetByIdAsync(Guid? id);
        Task UpdateCafe(Cafe cafe);
        Task DeleteCafe(Cafe cafe);
    }
}
