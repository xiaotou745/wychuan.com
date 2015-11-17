using System.Collections.Generic;
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
        private readonly UserDao userDao;
        //private readonly IMemberService memberService;
        //private readonly IBillBookService bookService;
        //private readonly IProjectService projectService;
        private readonly IItemsService itemsService;
        private readonly ICategoryService categoryService;

        public UserService()
        {
            userDao = new UserDao();
            //bookService = new BillBookService();
            //memberService = new MemberService();
            //projectService = new ProjectService();
            itemsService = new ItemsService();
            categoryService = new CategoryService();
        }

        public bool Logout(string userName)
        {
            throw new System.NotImplementedException();
        }

        #region 登录
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
            if (userDTO.IsDisable)
            {
                return LoginResult.UserIsDisable;
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

        #region 注册
        public RegisterResult Register(UserDTO user)
        {
            if (user == null || string.IsNullOrEmpty(user.LoginName) || string.IsNullOrEmpty(user.LoginPassword))
            {
                return RegisterResult.UserNameIsNull;
            }

            UserDTO userDTO = userDao.GetByName(user.LoginName);
            if (userDTO != null)//用户名已存在
            {
                return RegisterResult.UserNameExists;
            }
            user.LoginPassword = MD5.Encrypt(user.LoginPassword);//加密密码
            user.Id = userDao.Insert(user);

            //初始化默认数据
            itemsService.InitUserItems(user.Id);

            //初始化默认收支分类
            categoryService.InitUser(user.Id);

            return RegisterResult.Success;
        }
        #endregion

        #region GetAll
        public IList<UserDTO> GetAll()
        {
            return userDao.GetAll();
        }

        public void Disable(int userId, bool isDisable)
        {
            userDao.Disable(userId, isDisable);
        }

        public void Remove(int userId)
        {
            userDao.Delete(userId);
        }

        public void SetRoles(int userId, string roleIds)
        {
            userDao.SetRoles(userId, roleIds);
        }

        #endregion
    }
}