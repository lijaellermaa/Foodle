using App.DAL.DTO.Identity;
using Base.Domain;

namespace App.BLL.DTO.Identity;

public class AppRefreshToken : BaseRefreshToken
{
    public Guid AppUserId { get; set; }
    public AppUser? AppUser { get; set; }
}