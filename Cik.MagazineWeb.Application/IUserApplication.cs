using Cik.MagazineWeb.Application.UserAuthentications.Dtos;

namespace Cik.MagazineWeb.Application
{
    public interface IUserApplication
    {
        UserDto GetUserByUserName(string userName);

        bool ValidateUser(string userName, string password);

        int CreateUser(string userName, string displayName, string password, string email, int role, string createdBy);  
    }
}