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
            if (userDTO == null) //�û�������
            {
                return LoginResult.UserNotExists;
            }
            if (userDTO.IsDisable) //�û��ѽ���
            {
                return LoginResult.UserIsDisable;
            }
            string encrypt = MD5.Encrypt(user.LoginPassword);
            if (!encrypt.Equals(userDTO.LoginPassword)) //���벻��ȷ
            {
                return LoginResult.PasswordError;
            }
            user.Id = userDTO.Id;

            //�����û��˵�Ȩ�޵�����
            UserCacheProvider.RefreshUserInCache(userDTO);

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
            if (userDTO != null) //�û����Ѵ���
            {
                return RegisterResult.UserNameExists;
            }
            user.LoginPassword = MD5.Encrypt(user.LoginPassword); //��������

            using (IUnitOfWork unitOfWork = UnitOfWorkFactoryProvider.GetUnitOfWork())
            {
                user.Id = userDao.Insert(user);

                //��ʼ��Ĭ������
                itemsService.InitUserItems(user.Id);

                //��ʼ��Ĭ����֧����
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
        /// �����û�����Ȩ��
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="roleIds"></param>
        public void SetRoles(int userId, string roleIds)
        {
            userDao.SetRoles(userId, roleIds);
            //����û���Ϣ���ڻ����У���ˢ�»���
            if (UserCacheProvider.UserExistsInCache(userId))
            {
                UserCacheProvider.RefreshUserInCache(userId, roleIds);
            }
        }

        #endregion
    }
}