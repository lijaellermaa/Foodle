using App.BLL.DTO;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using App.Contracts.DAL.Repositories;
using Base.BLL;
using Base.Contracts;

namespace App.BLL.Services;

public class LocationService(IAppUnitOfWork uow, IMapper<Location, DAL.DTO.Location> mapper)
    : BaseEntityService<Location, DAL.DTO.Location, ILocationRepository>(uow.LocationRepository, mapper), ILocationService
{
}