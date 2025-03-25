using AutoMapper;
using Base.DAL;

namespace App.DAL.DTO.Mappers;

public class LocationMapper : BaseMapper<App.Domain.Location, Location>
{
    public LocationMapper(IMapper mapper) : base(mapper)
    {
    }
}