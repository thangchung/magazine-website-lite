using AutoMapper;
using Cik.MagazineWeb.Application.UserAuthentications.Dtos;
using Cik.MagazineWeb.Domain.UserMgmt;

namespace Cik.MagazineWeb.Application.UserAuthentications.Profiles
{
    public class UserProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<User, UserDto>();
            Mapper.CreateMap<UserDto, User>();
        }
    }
}