using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AC.Service.WeiXin.Request;

namespace AC.Service.WeiXin.Response
{
    public class MsgResponseBuilder
    {
        private MsgResponseBuilder()
        {
            
        }

        public static MsgResponse Builder(string requestContent)
        {
            BaseMsg baseMsg = MsgSerializer.Deserialize(requestContent);
            if (baseMsg == null)
            {
                return new ErrorMsgResponse();
            }
            switch (baseMsg.MsgType)
            {
                case MsgType.Text:
                    return new TextMsgResponse(baseMsg);
                case MsgType.Image:
                case MsgType.Voice:
                case MsgType.Video:
                case MsgType.Shortvideo:
                case MsgType.Location:
                case MsgType.Link:
                case MsgType.Event:
                    return new TextMsgResponse(baseMsg);
                default:
                    return new TextMsgResponse(baseMsg);
            }
        }
    }
}
