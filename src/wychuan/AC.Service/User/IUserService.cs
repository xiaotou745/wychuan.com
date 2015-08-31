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
    }
}