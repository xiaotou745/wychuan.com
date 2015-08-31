using AC.Service.WeiXin.Common;
using AC.Service.WeiXin.Request;

namespace AC.Service.WeiXin.Response
{

    #region 回复消息基类

    /// <summary>
    /// 回复消息服务类
    /// </summary>
    public abstract class MsgResponse
    {
        public abstract string GetResponse();
    }

    #endregion

    #region 发生错误或暂未实现的消息回复类

    /// <summary>
    /// 错误消息
    /// </summary>
    public class ErrorMsgResponse : MsgResponse
    {
        public override string GetResponse()
        {
            return string.Empty;
        }
    }

    #endregion

    #region 文本消息回复类,处理用户发送过来的文本消息

    /// <summary>
    /// 文本消息回复
    /// </summary>
    public class TextMsgResponse : MsgResponse
    {
        private readonly RequestTextMsg requestTextMsg;

        public TextMsgResponse(RequestMsgBase text)
        {
            this.requestTextMsg = text as RequestTextMsg;
        }

        public override string GetResponse()
        {
            string from = requestTextMsg.ToUserName;
            requestTextMsg.ToUserName = requestTextMsg.FromUserName;
            requestTextMsg.FromUserName = from;

            switch (requestTextMsg.Content.ToLower())
            {
                case "text":
                    //var deserialize = XmlHelper.Deserialize<string[]>("~/Content/data/xiaohua.xml");
                    requestTextMsg.Content = @"从前有个人钓鱼，钓到了只鱿鱼。 
鱿鱼求他：你放了我吧，别把我烤来吃啊。 
那个人说：好的，那么我来考问你几个问题吧。 
鱿鱼很开心说：你考吧你考吧！ 
然后这人就把鱿鱼给烤了.. ";
                    break;
                case "image":

                    break;
            }


            //var deserialize = XmlHelper.Deserialize<string[]>("~/Content/data/xiaohua.xml");
            requestTextMsg.Content = @"从前有个人钓鱼，钓到了只鱿鱼。 
鱿鱼求他：你放了我吧，别把我烤来吃啊。 
那个人说：好的，那么我来考问你几个问题吧。 
鱿鱼很开心说：你考吧你考吧！ 
然后这人就把鱿鱼给烤了.. ";
            return MsgSerializer.Serialize(GetMusicMsg());
        }

        private RequestMsgBase GetTextMsg()
        {
            string from = requestTextMsg.ToUserName;
            requestTextMsg.ToUserName = requestTextMsg.FromUserName;
            requestTextMsg.FromUserName = from;
            requestTextMsg.Content = @"从前有个人钓鱼，钓到了只鱿鱼。 
鱿鱼求他：你放了我吧，别把我烤来吃啊。 
那个人说：好的，那么我来考问你几个问题吧。 
鱿鱼很开心说：你考吧你考吧！ 
然后这人就把鱿鱼给烤了.. ";
            return requestTextMsg;
        }

        private ResponseMusicMsg GetMusicMsg()
        {
            ResponseMusicMsg result = new ResponseMusicMsg();
            result.CreateTime = requestTextMsg.CreateTime;
            result.ToUserName = requestTextMsg.FromUserName;
            result.FromUserName = requestTextMsg.ToUserName;
            result.MsgType = ResponseMsgType.Music;
            result.Music = new MusicInfo()
            {
                Description = "测试音乐消息回复",
                Title = "测试吧少年",
                HQMusicUrl = "http://web.kugou.com/?action=single&filename=%u9648%u6D01%u4EEA__-__%u4ECE%u524D%u7684%u6211%u3010%u897F%u6E38%u8BB0%u4E4B%u5927%u5723%u5F52%u6765%u7247%u5C3E%u66F2%u3011&hash=91611d3c8f1048283f7812b97c5299c8&timelen=272000&microblog=1&chl=kugou",
                MusicUrl = "http://web.kugou.com/?action=single&filename=%u9648%u6D01%u4EEA__-__%u4ECE%u524D%u7684%u6211%u3010%u897F%u6E38%u8BB0%u4E4B%u5927%u5723%u5F52%u6765%u7247%u5C3E%u66F2%u3011&hash=91611d3c8f1048283f7812b97c5299c8&timelen=272000&microblog=1&chl=kugou",
                ThumbMediaId = "",
            };
            return result;
        }
    }

    #endregion
}