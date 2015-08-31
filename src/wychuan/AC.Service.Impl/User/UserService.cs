using AC.Dao.User;
using AC.Security;
using AC.Service.DTO.User;
using AC.Service.User;
using AC.Util;

namespace AC.Service.Impl.User
{
    public class UserService : IUserService
    {
        private readonly UserDao userDao;

        public UserService()
        {
            userDao = new UserDao();
        }

        public bool Logout(string userName)
        {
            throw new System.NotImplementedException();
        }

        #region µÇÂ¼

        public LoginResult Login(UserDTO user)
        {
            if (user == null || string.IsNullOrEmpty(user.LoginName))
            {
                return LoginResult.UserNameIsNull;
            }
            if (string.IsNullOrEmpty(user.LoginPassword))
            {
                return LoginResult.PasswordIsNull;
            }
            UserDTO userDTO = userDao.GetByName(user.LoginName);
            if (userDTO == null)
            {
                return LoginResult.UserNotExists;
            }
            string encrypt = MD5.Encrypt(user.LoginPassword);
            if (!encrypt.Equals(userDTO.LoginPassword))
            {
                return LoginResult.PasswordError;
            }
            user.Id = userDTO.Id;
            return LoginResult.Success;
        }

        #endregion

        #region ×¢²á
        public RegisterResult Register(UserDTO user)
        {
            if (user == null || string.IsNullOrEmpty(user.LoginName) || string.IsNullOrEmpty(user.LoginPassword))
            {
                return RegisterResult.UserNameIsNull;
            }

            UserDTO userDTO = userDao.GetByName(user.LoginName);
            if (userDTO != null)
            {
                return RegisterResult.UserNameExists;
            }
            user.LoginPassword = MD5.Encrypt(user.LoginPassword);
            user.Id = userDao.Insert(user);
            return RegisterResult.Success;
        }
        #endregion
    }
}