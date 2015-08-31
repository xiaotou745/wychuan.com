using System.Xml.Serialization;

namespace AC.Service.WeiXin.Response
{
    #region 回复消息类型

    /// <summary>
    /// 回复消息类型
    /// </summary>
    public enum ResponseMsgType
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
        /// 音乐消息
        /// </summary>
        [XmlEnum("music")] Music,

        /// <summary>
        /// 图文消息
        /// </summary>
        [XmlEnum("news")] News,
    }

    #endregion

    #region 回复消息基类

    /// <summary>
    /// 回复消息基类
    /// </summary>
    [XmlRoot("xml")]
    public class ResponseMsgBase
    {
        /// <summary>
        /// 接收方帐号（收到的OpenID）
        /// </summary>
        public string ToUserName { get; set; }

        /// <summary>
        /// 开发者微信号
        /// </summary>
        public string FromUserName { get; set; }

        /// <summary>
        /// 消息创建时间 （整型）
        /// </summary>
        public int CreateTime { get; set; }

        /// <summary>
        /// 消息类型
        /// </summary>
        public ResponseMsgType MsgType { get; set; }
    }

    #endregion

    #region 回复文本消息

    [XmlRoot("xml")]
    public class ResponseTextMsg : ResponseMsgBase
    {
        /// <summary>
        /// 文本消息内容
        /// </summary>
        public string Content { get; set; }
    }

    #endregion

    #region 回复图片消息

    public class ImageInfo
    {
        public string MediaId { get; set; }
    }

    [XmlRoot("xml")]
    public class ResponseImageMsg : ResponseMsgBase
    {
        [XmlElement("Image")]
        public ImageInfo Image { get; set; }
    }

    #endregion

    #region 回复语音消息

    public class VoiceInfo
    {
        public string MediaId { get; set; }
    }

    [XmlRoot("xml")]
    public class ResponseVoiceMsg : ResponseMsgBase
    {
        [XmlElement("Voice")]
        public VoiceInfo Voice { get; set; }
    }

    #endregion

    #region 回复视频消息

    public class VideoInfo
    {
        /// <summary>
        /// 通过素材管理接口上传多媒体文件，得到的id
        /// </summary>
        public string MediaId { get; set; }

        /// <summary>
        /// 视频消息的标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 视频消息的描述
        /// </summary>
        public string Description { get; set; }
    }

    [XmlRoot("xml")]
    public class ResponseVideoMsg : ResponseMsgBase
    {
        [XmlElement("Video")]
        public VideoInfo Video { get; set; }
    }

    #endregion

    #region 回复音乐消息

    public class MusicInfo
    {
        /// <summary>
        /// 音乐标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 音乐描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 音乐链接
        /// </summary>
        public string MusicUrl { get; set; }

        /// <summary>
        /// 高质量音乐链接，WIFI环境优先使用该链接播放音乐
        /// </summary>
        public string HQMusicUrl { get; set; }

        /// <summary>
        /// 缩略图的媒体id，通过素材管理接口上传多媒体文件，得到的id
        /// </summary>
        public string ThumbMediaId { get; set; }
    }

    [XmlRoot("xml")]
    public class ResponseMusicMsg : ResponseMsgBase
    {
        [XmlElement("Music")]
        public MusicInfo Music { get; set; }
    }

    #endregion

    #region 回复图文消息

    public class ArticlesInfo
    {
        /// <summary>
        /// 图文消息标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 图文消息描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 图片链接，支持JPG、PNG格式，较好的效果为大图360*200，小图200*200
        /// </summary>
        public string PicUrl { get; set; }

        /// <summary>
        /// 击图文消息跳转链接
        /// </summary>
        public string Url { get; set; }
    }

    [XmlRoot("xml")]
    public class ResponseNewsMsg : ResponseMsgBase
    {
        /// <summary>
        /// 图文消息个数，限制为10条以内
        /// </summary>
        public int ArticleCount { get; set; }

        //[XmlElement("Articles")]
        [XmlArrayItem("item")]
        public ArticlesInfo[] Articles { get; set; }
    }

    #endregion
}