using System;

namespace AC.Tests.Template
{
    /// <summary>
    /// 实体类${classname} 。(属性说明自动提取数据库字段的描述信息)
    /// Generate By: ${author}
    /// Generate Time: ${createtime}
    /// </summary>
    public class ${classname}
    {
        public ${classname}() { }

        /// <summary>
        /// 用户ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 后台登录账号
        /// </summary>
        public string LoginName { get; set; }
        /// <summary>
        /// 登录密码
        /// </summary>
        public string LoginPwd { get; set; }
        /// <summary>
        /// 是否有效 1有效 0无效
        /// </summary>
        public bool IsEnable { get; set; }
        /// <summary>
        /// 注册时间
        /// </summary>
        public DateTime RegisterTime { get; set; }

    }
    /// <summary>
    /// 查询对象类UserQueryInfo 。(属性说明自动提取数据库字段的描述信息)
    /// Generate By: mywww2.wychuan.com
    /// Generate Time: 2015-10-12 13:21:34
    /// </summary>
    public class UserQueryInfo
    {
        public UserQueryInfo() { }
    }
}
