using App.BLL.DTO;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using App.Contracts.DAL.Repositories;
using Base.BLL;
using Base.Contracts;

namespace App.BLL.Services;

public class PriceService(IAppUnitOfWork uow, IMapper<Price, DAL.DTO.Price> mapper)
    : BaseEntityService<Price, DAL.DTO.Price, IPriceRepository>(uow.PriceRepository, mapper), IPriceService
{
}