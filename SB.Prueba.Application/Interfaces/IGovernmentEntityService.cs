using SB.Prueba.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SB.Prueba.Application.Interfaces
{
    public interface IGovernmentEntityService
    {
        Task<List<GovernmentEntity>> GetAllAsync();
        Task<GovernmentEntity> GetByIdAsync(int id);
        Task AddAsync(GovernmentEntity entity);
        Task UpdateAsync(GovernmentEntity entity);
        Task DeleteAsync(int id);
    }
}
