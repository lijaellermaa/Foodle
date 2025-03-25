using AutoMapper;
using Base.DAL;

namespace App.BLL.Mappers;

public class LocationMapper : BaseMapper<DTO.Location, App.DAL.DTO.Location>
{
    public LocationMapper(IMapper mapper) : base(mapper)
    {
    }
}