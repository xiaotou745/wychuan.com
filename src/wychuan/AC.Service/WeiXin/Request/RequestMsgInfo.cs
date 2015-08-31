using System.Xml.Serialization;

namespace AC.Service.WeiXin.Request
{

    #region 接收消息类型

    /// <summary>
    /// 微信消息类型
    /// </summary>
    public enum RequestMsgType
    {
        /// <summary>
        /// 文本消息
        /// </summary>
        [XmlEnum("text")] Text,

        /// <summary>
        /// 图片消息
        /// </summary>
        [XmlEnum("image")] Image,

        /// <summary>
        /// 语音消息
        /// </summary>
        [XmlEnum("voice")] Voice,

        /// <summary>
        /// 视频消息
        /// </summary>
        [XmlEnum("video")] Video,

        /// <summary>
        /// 小视频消息
        /// </summary>
        [XmlEnum("shortvideo")] Shortvideo,

        /// <summary>
        /// 地理位置消息
        /// </summary>
        [XmlEnum("location")] Location,

        /// <summary>
        /// 链接消息
        /// </summary>
        [XmlEnum("link")] Link,

        /// <summary>
        /// 事件
        /// </summary>
        [XmlEnum("event")] Event,
    }

    #endregion

    #region 请求消息基类

    /// <summary>
    /// 请求消息基类
    /// </summary>
    [XmlRoot("xml")]
    public class RequestMsgBase
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
        public RequestMsgType MsgType { get; set; }

        /// <summary>
        /// 消息id，64位整型
        /// </summary>
        public long MsgId { get; set; }
    }

    #endregion

    #region 请求文本消息体

    /// <summary>
    /// 文本消息
    /// </summary>
    [XmlRoot("xml")]
    public class RequestTextMsg : RequestMsgBase
    {
        /// <summary>
        /// 文本消息内容
        /// </summary>
        public string Content { get; set; }
    }

    #endregion

    #region 请求图片消息体

    [XmlRoot("xml")]
    public class RequestImageMsg : RequestMsgBase
    {
        /// <summary>
        /// 图片链接
        /// </summary>
        public string PicUrl { get; set; }

        /// <summary>
        /// 图片消息媒体id，可以调用多媒体文件下载接口拉取数据。
        /// </summary>
        public string MediaId { get; set; }
    }

    #endregion

    #region 请求语音消息体

    [XmlRoot("xml")]
    public class RequestVoiceMsg : RequestMsgBase
    {
        /// <summary>
        /// 语音消息媒体id，可以调用多媒体文件下载接口拉取数据。
        /// </summary>
        public string MediaId { get; set; }

        /// <summary>
        /// 语音格式，如amr，speex等
        /// </summary>
        public string Format { get; set; }
    }

    #endregion

    #region 请求视频消息体

    [XmlRoot("xml")]
    public class RequestVideoMsg : RequestMsgBase
    {
        /// <summary>
        /// 视频消息媒体id，可以调用多媒体文件下载接口拉取数据。
        /// </summary>
        public string MediaId { get; set; }

        /// <summary>
        /// 视频消息缩略图的媒体id，可以调用多媒体文件下载接口拉取数据。
        /// </summary>
        public string ThumbMediaId { get; set; }
    }

    #endregion

    #region 请求小视频消息体

    [XmlRoot("xml")]
    public class RequestShortVideoMsg : RequestMsgBase
    {
        /// <summary>
        /// 视频消息媒体id，可以调用多媒体文件下载接口拉取数据。
        /// </summary>
        public string MediaId { get; set; }

        /// <summary>
        /// 视频消息缩略图的媒体id，可以调用多媒体文件下载接口拉取数据。
        /// </summary>
        public string ThumbMediaId { get; set; }
    }

    #endregion

    #region 请求地址消息体

    [XmlRoot("xml")]
    public class RequestLocationMsg : RequestMsgBase
    {
        /// <summary>
        /// 地理位置维度
        /// </summary>
        [XmlAttribute("Location_X")]
        public double LocationX { get; set; }

        /// <summary>
        /// 地理位置经度
        /// </summary>
        [XmlAttribute("Location_Y")]
        public double LocationY { get; set; }

        /// <summary>
        /// 地图缩放大小
        /// </summary>
        public double Scale { get; set; }

        /// <summary>
        /// 地理位置信息
        /// </summary>
        public string Label { get; set; }
    }

    #endregion

    #region 请求链接消息体

    [XmlRoot("xml")]
    public class RequestLinkMsg : RequestMsgBase
    {
        /// <summary>
        /// 消息标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 消息描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 消息链接
        /// </summary>
        public string Url { get; set; }
    }

    #endregion

    [XmlRoot("xml")]
    public class Event : RequestMsgBase
    {
    }
}