﻿using AC.Service.WeiXin.Request;

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
            textMsg.Content = "hello world.";
            return MsgSerializer.Serialize<TextMsg>(textMsg);
        }
    }
}
