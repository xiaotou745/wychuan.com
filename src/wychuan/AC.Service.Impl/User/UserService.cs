using System;
using System.Collections.Generic;
using System.Linq;
using AC.Cache;
using AC.Dao;
using AC.Dao.User;
using AC.Extension;
using AC.Security;
using AC.Service.DTO.LiCai;
using AC.Service.DTO.User;
using AC.Service.Impl.Cache;
using AC.Service.Impl.LiCai;
using AC.Service.LiCai;
using AC.Service.User;
using AC.Transaction.Common;
using AC.Util;

namespace AC.Service.Impl.User
{
    public class UserService : IUserService
    {
        private readonly UserDao userDao;
        private readonly IItemsService itemsService;
        private readonly ICategoryService categoryService;
        private readonly IMenuService menuService;

        public UserService()
        {
            userDao = new UserDao();
            itemsService = new ItemsService();
            categoryService = new CategoryService();
            menuService = new MenuService();
        }

        #region Logout

        public bool Logout(string userName)
        {
            throw new System.NotImplementedException();
        }

        #endregion

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
            if (userDTO == null) //用户不存在
            {
                return LoginResult.UserNotExists;
            }
            if (userDTO.IsDisable) //用户已禁用
            {
                return LoginResult.UserIsDisable;
            }
            string encrypt = MD5.Encrypt(user.LoginPassword);
            if (!encrypt.Equals(userDTO.LoginPassword)) //密码不正确
            {
                return LoginResult.PasswordError;
            }
            user.Id = userDTO.Id;

            //保存用户菜单权限到缓存
            UserCacheProvider.RefreshUserInCache(userDTO);

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
            if (userDTO != null) //用户名已存在
            {
                return RegisterResult.UserNameExists;
            }
            user.LoginPassword = MD5.Encrypt(user.LoginPassword); //加密密码

            using (IUnitOfWork unitOfWork = UnitOfWorkFactoryProvider.GetUnitOfWork())
            {
                user.Id = userDao.Insert(user);

                //初始化默认数据
                itemsService.InitUserItems(user.Id);

                //初始化默认收支分类
                categoryService.InitUser(user.Id);

                unitOfWork.Complete();
            }

            return RegisterResult.Success;
        }

        #endregion

        #region GetAll

        public IList<UserDTO> GetAll()
        {
            return userDao.GetAll();
        }

        #endregion

        #region Disable

        public void Disable(int userId, bool isDisable)
        {
            userDao.Disable(userId, isDisable);
        }

        #endregion

        #region Remove

        public void Remove(int userId)
        {
            userDao.Delete(userId);
        }

        #endregion

        #region SetRoles

        /// <summary>
        /// 更新用户所属权限
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="roleIds"></param>
        public void SetRoles(int userId, string roleIds)
        {
            userDao.SetRoles(userId, roleIds);
            //如果用户信息已在缓存中，则刷新缓存
            if (UserCacheProvider.UserExistsInCache(userId))
            {
                UserCacheProvider.RefreshUserInCache(userId, roleIds);
            }
        }

        #endregion
    }
}