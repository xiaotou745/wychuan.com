namespace AC.Service.WeiXin.Menu
{
    public enum MenuButtonType
    {
        click=1,
        view=2,
        scancode_push=3,
        scancode_waitmsg=4,
        pic_sysphoto=5,
        pic_photo_or_album=6,
        pic_weixin=7,
        location_select=8,
        media_id=9,
        view_limited=10,
    }
    public class MenuButton
    {
        /// <summary>
        /// 菜单的响应动作类型
        /// </summary>
        public string type { get; set; }

        /// <summary>
        /// 菜单标题，不超过16个字节，子菜单不超过40个字节
        /// 
        /// click等点击类型必须
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 网页链接，用户点击菜单可打开链接，不超过256字节
        /// 
        /// view类型必须
        /// </summary>
        public string url { get; set; }

        /// <summary>
        /// media_id类型和view_limited类型必须
        /// 
        /// 调用新增永久素材接口返回的合法media_id
        /// </summary>
        public string media_id { get; set; }

        /// <summary>
        /// 菜单KEY值，用于消息接口推送，不超过128字节
        /// </summary>
        public string key { get; set; }

        /// <summary>
        /// 二级菜单数组，个数应为1~5个
        /// </summary>
        public MenuButton[] sub_button { get; set; }
    }

    public class MenuInfo
    {
        public MenuButton[] button { get; set; }
    }
}
