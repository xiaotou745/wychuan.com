using System;
using System.IO;
using System.Xml.Serialization;
using AC.Service.WeiXin.Request;

namespace AC.Service.WeiXin.Common
{
    /// <summary>
    /// 消息序列化类
    /// </summary>
    public class MsgSerializer
    {
        /// <summary>
        /// 序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string Serialize<T>(T obj) where T : class
        {
            var xmlSerializer = new XmlSerializer(typeof (T));
            using (var sw = new StringWriter())
            {
                var xmlNameSpace = new XmlSerializerNamespaces();
                xmlNameSpace.Add("", "");
                xmlSerializer.Serialize(sw, obj, xmlNameSpace);
                return sw.ToString();
            }
        }

        /// <summary>
        /// 反序列化
        /// </summary>
        /// <param name="msgStr"></param>
        /// <returns></returns>
        public static RequestMsgBase Deserialize(string msgStr)
        {
            RequestMsgType? requestMsgType = GetMsgType(msgStr);
            if (requestMsgType == null)
            {
                return null;
            }
            using (var sr = new StringReader(msgStr))
            {
                var xmlSerializer = new XmlSerializer(GetMessageType(requestMsgType.Value));
                object deserialize = xmlSerializer.Deserialize(sr);
                if (deserialize == null)
                {
                    return null;
                }
                return (deserialize as RequestMsgBase);
            }
        }

        private static Type GetMessageType(RequestMsgType requestMsgType)
        {
            switch (requestMsgType)
            {
                case RequestMsgType.Text:
                    return typeof (RequestTextMsg);
                case RequestMsgType.Image:
                    return typeof (RequestImageMsg);
                case RequestMsgType.Voice:
                    return typeof (RequestVoiceMsg);
                case RequestMsgType.Video:
                    return typeof (RequestVideoMsg);
                case RequestMsgType.Shortvideo:
                    return typeof (RequestShortVideoMsg);
                case RequestMsgType.Location:
                    return typeof (RequestLocationMsg);
                case RequestMsgType.Link:
                    return typeof (RequestLinkMsg);
                case RequestMsgType.Event:
                    return typeof (Event);
                default:
                    throw new ArgumentOutOfRangeException("requestMsgType");
            }
        }

        private static RequestMsgType? GetMsgType(string msgStr)
        {
            using (StringReader sr = new StringReader(msgStr))
            {
                var xmlSerializer = new XmlSerializer(typeof (RequestMsgBase));
                object deserialize = xmlSerializer.Deserialize(sr);
                if (deserialize == null)
                {
                    return null;
                }
                return (deserialize as RequestMsgBase).MsgType;
            }
        }
    }
}