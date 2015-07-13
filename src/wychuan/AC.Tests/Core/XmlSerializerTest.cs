using System.Xml.Serialization;
using AC.Service.WeiXin.Request;
using Common.Logging;
using NUnit.Framework;

namespace AC.Tests.Core
{
    [TestFixture]
    public class XmlSerializerTest
    {
        private readonly ILog logger = LogManager.GetCurrentClassLogger();

        [Test]
        public void Test()
        {
            string s = @"<xml>
 <ToUserName><![CDATA[toUser]]></ToUserName>
 <FromUserName><![CDATA[fromUser]]></FromUserName> 
 <CreateTime>1348831860</CreateTime>
 <MsgType><![CDATA[text]]></MsgType>
 <Content><![CDATA[this is a test]]></Content>
 <MsgId>1234567890123456</MsgId>
 </xml>";

            XmlSerializer serializer = new XmlSerializer(typeof (TextMsg));

            using (System.IO.StringReader reader = new System.IO.StringReader(s))
            {
                object deserialize = serializer.Deserialize(reader);
                if (deserialize == null)
                {
                    logger.Info("is null?");
                }
                logger.Info(Json.JsonSerializer.Serialize(deserialize));
                //var xmlSerializer = new XmlSerializer(typeof (TextMsg));
                //using (System.IO.StringWriter sw = new System.IO.StringWriter())
                //{
                //    var xmlNameSpace = new XmlSerializerNamespaces();
                //    xmlNameSpace.Add("", "");
                //    //xmlSerializer.Serialize(sw, deserialize);
                //    xmlSerializer.Serialize(sw, deserialize, xmlNameSpace);
                //    logger.Info(sw.ToString());
                //}
            }
        }
    }
}