using System.Collections.Generic;
using AC.Service.DTO.User;

namespace AC.Service.User
{
    /// <summary>
    /// 用户接口
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        LoginResult Login(UserDTO user);

        /// <summary>
        /// 注销
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        bool Logout(string userName);

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        RegisterResult Register(UserDTO user);

        /// <summary>
        /// 获取所有用户
        /// </summary>
        /// <returns></returns>
        IList<UserDTO> GetAll();

        /// <summary>
        /// 禁用
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="isDisable"></param>
        void Disable(int userId, bool isDisable);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="userId"></param>
        void Remove(int userId);

        /// <summary>
        /// 设置权限
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="roleIds"></param>
        void SetRoles(int userId, string roleIds);
    }
}