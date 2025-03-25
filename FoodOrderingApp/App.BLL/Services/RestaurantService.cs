using App.BLL.DTO;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using App.Contracts.DAL.Repositories;
using Base.BLL;
using Base.Contracts;

namespace App.BLL.Services;

public class RestaurantService(IAppUnitOfWork uow, IMapper<Restaurant, DAL.DTO.Restaurant> mapper)
    : BaseEntityService<Restaurant, DAL.DTO.Restaurant, IRestaurantRepository>(uow.RestaurantRepository, mapper), IRestaurantService
{
}