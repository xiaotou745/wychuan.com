using System;
using AC.Helper;
using AC.Service.WeiXin.Common;
using AC.Service.WeiXin.Request;

namespace AC.Service.WeiXin.Response
{
    /// <summary>
    /// 文本消息回复
    /// </summary>
    public class TextMsgResponse : MsgResponse
    {
        private readonly TextMsg textMsg;
        public TextMsgResponse(BaseMsg textMsg)
        {
            this.textMsg = textMsg as TextMsg;
        }

        public override string GetResponse()
        {
            string from = textMsg.ToUserName;
            textMsg.ToUserName = textMsg.FromUserName;
            textMsg.FromUserName = from;

            switch (textMsg.Content.ToLower())
            {
                case "text":
                    //var deserialize = XmlHelper.Deserialize<string[]>("~/Content/data/xiaohua.xml");
                    textMsg.Content = @"从前有个人钓鱼，钓到了只鱿鱼。 
鱿鱼求他：你放了我吧，别把我烤来吃啊。 
那个人说：好的，那么我来考问你几个问题吧。 
鱿鱼很开心说：你考吧你考吧！ 
然后这人就把鱿鱼给烤了.. ";
                    break;
                case "image":

                    break;
            }

            //var deserialize = XmlHelper.Deserialize<string[]>("~/Content/data/xiaohua.xml");
            textMsg.Content = @"从前有个人钓鱼，钓到了只鱿鱼。 
鱿鱼求他：你放了我吧，别把我烤来吃啊。 
那个人说：好的，那么我来考问你几个问题吧。 
鱿鱼很开心说：你考吧你考吧！ 
然后这人就把鱿鱼给烤了.. ";
            return MsgSerializer.Serialize<TextMsg>(textMsg);
        }
    }
}
