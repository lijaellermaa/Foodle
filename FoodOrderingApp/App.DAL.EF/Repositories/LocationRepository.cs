using App.Contracts.DAL.Repositories;
using App.DAL.DTO;
using Base.Contracts;
using Base.DAL.EF;

namespace App.DAL.EF.Repositories;

public class LocationRepository(AppDbContext dataContext, IMapper<App.Domain.Location, Location> mapper)
    : BaseEntityRepository<App.Domain.Location, Location, AppDbContext>(dataContext, mapper), ILocationRepository;