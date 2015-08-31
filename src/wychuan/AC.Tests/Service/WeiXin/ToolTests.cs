using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AC.Service.WeiXin.Common;
using AC.Service.WeiXin.Response;
using Common.Logging;
using NUnit.Framework;

namespace AC.Tests.Service.WeiXin
{
    [TestFixture]
    public class ToolTests
    {
        private ILog logger = LogManager.GetCurrentClassLogger();

        [Test]
        public void SerializeTest()
        {
            ResponseImageMsg imageMsg = new ResponseImageMsg();
            imageMsg.FromUserName = "chuan";
            imageMsg.ToUserName = "chuan";
            imageMsg.MsgType = ResponseMsgType.Image;
            imageMsg.Image = new ImageInfo() {MediaId = "url"};
            string serialize = MsgSerializer.Serialize<ResponseImageMsg>(imageMsg);
            logger.Info(serialize);

            ResponseNewsMsg newsMsg = new ResponseNewsMsg();
            newsMsg.FromUserName = "chuan";
            newsMsg.ToUserName = "chuan";
            newsMsg.MsgType = ResponseMsgType.News;
            newsMsg.ArticleCount = 2;
            newsMsg.Articles = new ArticlesInfo[2]
            {
                new ArticlesInfo() {Title = "title", Description = "des", Url = "url", PicUrl = "picurl"},
                new ArticlesInfo() {Title = "title", Description = "des", Url = "url", PicUrl = "picurl"},
            };
            string newsMsgXml = MsgSerializer.Serialize(newsMsg);
            logger.Info(newsMsgXml);
        }
    }
}