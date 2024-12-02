using CafeOps.DAL.Entities;

namespace CafeOps.DAL.Repositories.Interfaces
{
    public interface IEmployeeRepository
    {
        Task AddAsync(Employee employee);
        Task<Employee?> GetByIdAsync(string id);
        Task<List<Employee>> GetByCafeAsync(Guid cafeId);
        Task UpdateAsync(Employee employee);
        Task DeleteAsync(Employee employee);
    }

}
