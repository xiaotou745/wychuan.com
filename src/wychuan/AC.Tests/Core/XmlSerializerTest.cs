using System.Collections.Generic;
using System.Xml.Serialization;
using AC.Helper;
using AC.Service.WeiXin.Request;
using AC.Web.Common.Config;
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

            XmlSerializer serializer = new XmlSerializer(typeof (RequestTextMsg));

            using (System.IO.StringReader reader = new System.IO.StringReader(s))
            {
                object deserialize = serializer.Deserialize(reader);
                if (deserialize == null)
                {
                    logger.Info("is null?");
                }
                logger.Info(Json.JsonSerializer.Serialize(deserialize));
                //var xmlSerializer = new XmlSerializer(typeof (text));
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

        [Test]
        public void StringArrayTest()
        {
            string[] arrays = new string[]
            {
                @"从前有个人钓鱼，钓到了只鱿鱼。 
鱿鱼求他：你放了我吧，别把我烤来吃啊。 
那个人说：好的，那么我来考问你几个问题吧。 
鱿鱼很开心说：你考吧你考吧！ 
然后这人就把鱿鱼给烤了.. ",
                "我曾经得过精神分裂症，但现在我们已经康复了。 ",
                @"一留学生在美国考驾照，前方路标提示左转，他不是很确定，问考官： 
“turn left?” 
答：“right” 
于是……挂了.. ",
                @"有一天绿豆自杀从5楼跳下来，流了很多血，变成了红豆；一直流脓，又变成了黄豆；伤口结了疤，最后成了黑豆。 ",
                @"小明理了头发，第二天来到学校，同学们看到他的新发型，笑道：小明，你的头型好像个风筝哦！小明觉得很委屈，就跑到外面哭。哭着哭着～他就飞起来了…………",
                @"有个人长的像洋葱，走着走着就哭了…….",
                @"小企鹅有一天问他奶奶，“奶奶奶奶，我是不是一只企鹅啊？”“是啊，你当然是企鹅。”小企鹅又问爸爸，“爸爸爸爸，我是不是一只企鹅啊？”“是啊，你是企鹅啊，怎么了?”“可是，可是我怎么觉得那么冷呢？” ",
                @"有一对玉米相爱了… 
于是它们决定结婚… 
结婚那天… 
一个玉米找不到另一个玉米了… 
这个玉米就问身旁的爆米花：你看到我们家玉米了吗? 
爆米花：亲爱的，人家穿婚纱了嘛……. ",
                @"音乐课上 老师弹了一首贝多芬的曲子 
小明问小华：“你懂音乐吗？” 
小华：“是的” 
小明：“那你知道老师在弹什麼吗？” 
小华: “钢琴。” ",
                @"Q：有两个人掉到陷阱里了，死的人叫死人，活人叫什么? 
A:叫救命啦! ",
                @"提问：布和纸怕什么？ 
回答：布怕一万，纸怕万一。 
原因：不(布)怕一万，只(纸)怕万一。 ",
                @"有一天有个婆婆坐车… 
坐到中途婆婆不认识路了…. 
婆婆用棍子打司机屁股说：这是哪？ 
司机：这是我的屁股….. ",
              @"主持人问：猫是否会爬树？老鹰抢答：会！主持人：举例说明！老鹰含泪：那年，我睡熟了，猫爬上了树…后来就有了猫头鹰… ",
              @"俩屎壳螂讨论福利彩票，甲说:我要中了大奖就把方圆50里的厕所都买下来，每天吃个够！乙说:你丫太俗了！我要是中了大奖就包一活人，每天吃新鲜的！",
              @"why the chicken cross the street 
答案 to get another side ",
              @"甲：那个人在干什么？ 
乙：他在发抖。 
甲：他为什么要发抖呢？ 
乙：他冷呀。 
甲：哦，原来发抖就不会冷拉。 
甲：…… ",
              @"有个香蕉先生和女朋友约会，走在街上，天气很热，香蕉先生就把衣服脱掉了，之后他的女朋友就摔倒了……… ",
              @"一个香肠被关在冰箱里 
感觉很冷,然后看了看身边的另一根,有了点安慰,说：“看你都冻成这样了,全身都是冰！”结果那根说：“对不起，我是冰棒。",
              @"从前有一个棉花糖去打了球打了很长时间.他说:好累啊,我觉得我整个人都软下来了………. ",
            };

            AC.Helper.XmlHelper.ToXml("xiaohua.xml", arrays);
        }

        [Test]
        public void MenuSerializeTest()
        {
            List<MenuInfo> menus = new List<MenuInfo>();
            MenuInfo menuInfo = new MenuInfo()
            {
                Href = string.Empty,
                Icon = "fa-th-large",
                Text = "菜单管理",
                Childs = new MenuInfo[]
                {
                    new MenuInfo()
                    {
                        Href = "/home/index",
                        Text = "菜单查询",
                        Icon = "text",
                    },
                    new MenuInfo()
                    {
                        Href = "/home/index",
                        Text = "菜单查询",
                        Icon = "text",
                    },
                }
            };
            MenuInfo menuInfo2 = new MenuInfo()
            {
                Href = string.Empty,
                Icon = "fa-th-large",
                Text = "菜单管理",
                Childs = new MenuInfo[]
                {
                    new MenuInfo()
                    {
                        Href = "/home/index",
                        Text = "菜单查询",
                        Icon = "text",
                    },
                    new MenuInfo()
                    {
                        Href = "/home/index",
                        Text = "菜单查询",
                        Icon = "text",
                    },
                }
            };
            menus.Add(menuInfo);
            menus.Add(menuInfo2);
            XmlHelper.ToXml("menuOfWeixin.xml", menus.ToArray());
        }

        [Test]
        public void KeyValueTest()
        {
            var accountType = new List<KeyValuePair<int, string>>
            {
                new KeyValuePair<int, string>(1, "现金"),
                new KeyValuePair<int, string>(2, "储蓄卡"),
                new KeyValuePair<int, string>(1, "信用卡"),
                new KeyValuePair<int, string>(1, "投资账户"),
                new KeyValuePair<int, string>(1, "虚拟账户"),
                new KeyValuePair<int, string>(1, "网络账户"),
                new KeyValuePair<int, string>(1, "储值卡")
            };

            XmlHelper.ToXml("lc_accounttype.config", accountType);

        }
    }
}