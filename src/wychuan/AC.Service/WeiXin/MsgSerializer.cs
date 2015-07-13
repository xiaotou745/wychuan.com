using System;
using System.IO;
using System.Xml.Serialization;
using AC.Service.WeiXin.Request;

namespace AC.Service.WeiXin
{
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
        public static BaseMsg Deserialize(string msgStr)
        {
            MsgType? msgType = GetMsgType(msgStr);
            if (msgType == null)
            {
                return null;
            }
            using (var sr = new StringReader(msgStr))
            {
                var xmlSerializer = new XmlSerializer(GetMessageType(msgType.Value));
                object deserialize = xmlSerializer.Deserialize(sr);
                if (deserialize == null)
                {
                    return null;
                }
                return (deserialize as BaseMsg);
            }
        }

        private static Type GetMessageType(MsgType msgType)
        {
            switch (msgType)
            {
                case MsgType.Text:
                    return typeof (TextMsg);
                case MsgType.Image:
                    return typeof (ImageMsg);
                case MsgType.Voice:
                    return typeof (VoiceMsg);
                case MsgType.Video:
                    return typeof (VideoMsg);
                case MsgType.Shortvideo:
                    return typeof (ShortVideoMsg);
                case MsgType.Location:
                    return typeof (LocationMsg);
                case MsgType.Link:
                    return typeof (LinkMsg);
                case MsgType.Event:
                    return typeof (EventMsg);
                default:
                    throw new ArgumentOutOfRangeException("msgType");
            }
        }

        private static MsgType? GetMsgType(string msgStr)
        {
            using (StringReader sr = new StringReader(msgStr))
            {
                var xmlSerializer = new XmlSerializer(typeof (BaseMsg));
                object deserialize = xmlSerializer.Deserialize(sr);
                if (deserialize == null)
                {
                    return null;
                }
                return (deserialize as BaseMsg).MsgType;
            }
        }
    }
}