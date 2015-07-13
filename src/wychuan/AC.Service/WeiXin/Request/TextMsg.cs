using System.Xml.Serialization;

namespace AC.Service.WeiXin.Request
{
    /// <summary>
    /// 文本消息
    /// </summary>
    [XmlRoot("xml")]
    public class TextMsg : BaseMsg
    {
        /// <summary>
        /// 文本消息内容
        /// </summary>
        public string Content { get; set; }
    }

    [XmlRoot("xml")]
    public class ImageMsg : BaseMsg
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

    [XmlRoot("xml")]
    public class VoiceMsg : BaseMsg
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

    [XmlRoot("xml")]
    public class VideoMsg : BaseMsg
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

    [XmlRoot("xml")]
    public class ShortVideoMsg : BaseMsg
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

    [XmlRoot("xml")]
    public class LocationMsg : BaseMsg
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

    [XmlRoot("xml")]
    public class LinkMsg : BaseMsg
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

    [XmlRoot("xml")]
    public class EventMsg : BaseMsg
    {
        
    }
}
