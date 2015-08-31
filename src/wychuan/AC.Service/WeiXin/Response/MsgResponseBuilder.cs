using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AC.Service.WeiXin.Common;
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
            RequestMsgBase requestMsgBase = MsgSerializer.Deserialize(requestContent);
            if (requestMsgBase == null)
            {
                return new ErrorMsgResponse();
            }
            switch (requestMsgBase.MsgType)
            {
                case RequestMsgType.Text:
                    return new TextMsgResponse(requestMsgBase);
                case RequestMsgType.Image:
                case RequestMsgType.Voice:
                case RequestMsgType.Video:
                case RequestMsgType.Shortvideo:
                case RequestMsgType.Location:
                case RequestMsgType.Link:
                case RequestMsgType.Event:
                    return new TextMsgResponse(requestMsgBase);
                default:
                    return new TextMsgResponse(requestMsgBase);
            }
        }
    }
}
