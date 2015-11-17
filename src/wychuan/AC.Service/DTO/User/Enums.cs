using System.ComponentModel;

namespace AC.Service.DTO.User
{
    public enum RegisterResult
    {
        /// <summary>
        /// 用户名不允许为空
        /// </summary>
        [Description("用户名不允许为空")]
        UserNameIsNull = 1,
        [Description("用户名已存在")]
        UserNameExists=2,
        [Description("注册成功")]
        Success=0,
    }

    /// <summary>
    /// 登录结果
    /// </summary>
    public enum LoginResult
    {
        /// <summary>
        /// 登录成功
        /// </summary>
        [Description("登录成功")]
        Success = 0,

        /// <summary>
        /// 用户名不允许为空
        /// </summary>
        [Description("用户名不允许为空")]
        UserNameIsNull = 1,

        /// <summary>
        /// 密码不允许为空
        /// </summary>
        [Description("密码不允许为空")]
        PasswordIsNull = 2,

        /// <summary>
        /// 用户名不存在
        /// </summary>
        [Description("用户名不存在")]
        UserNotExists = 3,

        /// <summary>
        /// 密码错误
        /// </summary>
        [Description("密码错误")]
        PasswordError = 4,

        [Description("用户被禁用")]
        UserIsDisable=5,
    }
}