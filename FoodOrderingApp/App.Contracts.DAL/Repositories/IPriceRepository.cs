using App.DAL.DTO;
using Base.Contracts.DAL.Repositories;

namespace App.Contracts.DAL.Repositories;

public interface IPriceRepository : IBaseRepository<Price>
{
    Task<ICollection<Price>> GetAllForProductAsync(Guid productId, Guid? userId = default, Boolean noTracking = true);
    Task<Price?> FirstLatestForProductAsync(Guid productId, Guid? userId = default, Boolean noTracking = true);
}