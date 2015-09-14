using AC.Dao.User;
using AC.Security;
using AC.Service.DTO.LiCai;
using AC.Service.DTO.User;
using AC.Service.Impl.LiCai;
using AC.Service.LiCai;
using AC.Service.User;
using AC.Util;

namespace AC.Service.Impl.User
{
    public class UserService : IUserService
    {
        private readonly IBillBookService bookService;
        private readonly UserDao userDao;

        public UserService()
        {
            userDao = new UserDao();
            bookService = new BillBookService();
        }

        public bool Logout(string userName)
        {
            throw new System.NotImplementedException();
        }

        #region ��¼

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

        #region ע��
        public RegisterResult Register(UserDTO user)
        {
            if (user == null || string.IsNullOrEmpty(user.LoginName) || string.IsNullOrEmpty(user.LoginPassword))
            {
                return RegisterResult.UserNameIsNull;
            }

            UserDTO userDTO = userDao.GetByName(user.LoginName);
            if (userDTO != null)//�û����Ѵ���
            {
                return RegisterResult.UserNameExists;
            }
            user.LoginPassword = MD5.Encrypt(user.LoginPassword);//��������
            user.Id = userDao.Insert(user);

            //���Ĭ���˱�
            BillBooksDTO defaultBook = BillBooksDTO.Default();
            defaultBook.UserId = user.Id;
            defaultBook.CreateBy = user.LoginName;

            bookService.Create(defaultBook);

            return RegisterResult.Success;
        }
        #endregion
    }
}