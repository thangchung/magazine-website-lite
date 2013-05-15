using Cik.MagazineWeb.Domain.UserMgmt;

namespace Cik.MagazineWeb.Application.UserAuthentications.Dtos
{
    public class UserDto : DtoBase
    {
        public string UserName { get; set; }

        public Role Role { get; set; }
    }
}