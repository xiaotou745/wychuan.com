using System.Xml.Serialization;

namespace AC.Service.WeiXin
{
    /// <summary>
    /// 微信消息类型
    /// </summary>
    public enum MsgType
    {
        /// <summary>
        /// 文本消息
        /// </summary>
        [XmlEnum("text")]
        Text,

        /// <summary>
        /// 图片消息
        /// </summary>
        [XmlEnum("image")]
        Image,

        /// <summary>
        /// 语音消息
        /// </summary>
        [XmlEnum("voice")]
        Voice,

        /// <summary>
        /// 视频消息
        /// </summary>
        [XmlEnum("video")]
        Video,

        /// <summary>
        /// 小视频消息
        /// </summary>
        [XmlEnum("shortvideo")]
        Shortvideo,

        /// <summary>
        /// 地理位置消息
        /// </summary>
        [XmlEnum("location")]
        Location,

        /// <summary>
        /// 链接消息
        /// </summary>
        [XmlEnum("link")]
        Link,

        /// <summary>
        /// 事件
        /// </summary>
        [XmlEnum("event")]
        Event,
    }
}