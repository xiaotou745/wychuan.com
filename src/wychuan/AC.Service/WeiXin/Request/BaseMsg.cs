using System.Xml.Serialization;

namespace AC.Service.WeiXin.Request
{
    /// <summary>
    /// 请求消息基类
    /// </summary>
    [XmlRoot("xml")]
    public class BaseMsg
    {
        /// <summary>
        /// 开发者微信号
        /// </summary>
        public string ToUserName { get; set; }

        /// <summary>
        /// 发送方帐号（一个OpenID）
        /// </summary>
        public string FromUserName { get; set; }

        /// <summary>
        /// 消息创建时间 （整型）
        /// </summary>
        public int CreateTime { get; set; }

        /// <summary>
        /// 消息类型，event
        /// </summary>
        public MsgType MsgType { get; set; }

        /// <summary>
        /// 消息id，64位整型
        /// </summary>
        public long MsgId { get; set; }
    }
}