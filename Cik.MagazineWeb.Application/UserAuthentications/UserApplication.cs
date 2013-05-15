using System;
using System.Linq;
using Cik.MagazineWeb.Application.UserAuthentications.Dtos;
using Cik.MagazineWeb.Domain.UserMgmt;
using Cik.MagazineWeb.Utilities;
using Cik.MagazineWeb.Utilities.Encyption;
using Cik.MagazineWeb.Utilities.Extensions;
using SharpLite.Domain.DataInterfaces;

namespace Cik.MagazineWeb.Application.UserAuthentications
{
    public class UserApplication : IUserApplication
    {
        private readonly IEncrypting _encryptor;
        private readonly IRepository<User> _userRepository;

        public UserApplication(IEncrypting encryptor, IRepository<User> userRepository)
        {
            Guard.ArgumentNotNull(encryptor, "SimpleEncryptor");
            Guard.ArgumentNotNull(userRepository, "UserRepository");

            _encryptor = encryptor;
            _userRepository = userRepository;
        }

        public UserDto GetUserByUserName(string userName)
        {
            var currentUser = GetUser(userName);

            return currentUser == null ? null : currentUser.MapTo<UserDto>();
        }

        public bool ValidateUser(string userName, string password)
        {
            var currentUser = GetUser(userName);

            if (currentUser == null)
                return false;

            return currentUser.UserName.Equals(userName, StringComparison.InvariantCulture)
                   && currentUser.Password.Equals(password, StringComparison.InvariantCulture); 
        }

        public int CreateUser(string userName, string displayName, string password, string email, int role, string createdBy)
        {
            var hashPassword = _encryptor.Encode(password);

            return _userRepository.SaveOrUpdate(UserFactory.Create(userName, displayName, hashPassword, email, role, createdBy)).Id; 
        }

        private User GetUser(string userName)
        {
            return _userRepository.GetAll()
                                  .FirstOrDefault(
                                      u => u.UserName.Equals(
                                          userName,
                                          StringComparison.InvariantCulture));
        }
    }
}