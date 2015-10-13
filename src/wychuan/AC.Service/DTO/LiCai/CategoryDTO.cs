using System.Collections.Generic;
using System.ComponentModel;

namespace AC.Service.DTO.LiCai
{
    public enum CategoryType
    {
        /// <summary>
        /// 收入
        /// </summary>
        [Description("收入")]
        Income=2,
        /// <summary>
        /// 支出
        /// </summary>
        [Description("支出")]
        Expend = 1,
    }

    public class CategoryDTO
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }

        public int UserId { get; set; }

        /// <summary>
        /// 父类型ID
        /// </summary>
        public int ParentId { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 收支类型(1支出 2 收入)
        /// </summary>
        public CategoryType InOutType { get; set; }

        /// <summary>
        /// 是否常用
        /// </summary>
        public int IsCommonUse { get; set; }

        public int OrderBy { get; set; }

        public int InitKey { get; set; }

        public static IList<CategoryDTO> Default()
        {
            IList<CategoryDTO> result = new List<CategoryDTO>();

            //工资 薪水 收入
            result.Add(Get(0, 1, "薪资", CategoryType.Income, 1,1000));
            result.Add(Get(1, 0, "工资", CategoryType.Income, 1, 100));
            result.Add(Get(1, 0, "奖金", CategoryType.Income, 0, 99));
            result.Add(Get(1, 0, "公积金", CategoryType.Income, 1,99));
            result.Add(Get(1, 0, "加班", CategoryType.Income, 0, 98));
            result.Add(Get(1, 0, "报销款", CategoryType.Income, 1,97));

            //理财收入
            result.Add(Get(0, 2, "理财", CategoryType.Income, 1,990));
            result.Add(Get(2, 0, "P2P", CategoryType.Income, 1, 90));
            result.Add(Get(2, 0, "理财通", CategoryType.Income, 1, 90));
            result.Add(Get(2, 0, "余额宝", CategoryType.Income, 1, 90));
            result.Add(Get(2, 0, "基金", CategoryType.Income, 0, 90));
            result.Add(Get(2, 0, "股票", CategoryType.Income, 0, 90));
            result.Add(Get(2, 0, "利息", CategoryType.Income, 0, 90));
           
            //人情收入
            result.Add(Get(0, 3, "人情", CategoryType.Income, 1, 980));
            result.Add(Get(3, 0, "微信红包", CategoryType.Income, 1, 80));
            result.Add(Get(3, 0, "礼金", CategoryType.Income, 0, 80));

            //应收款
            result.Add(Get(0, 4, "应收款", CategoryType.Income, 1, 970));
            result.Add(Get(4, 0, "外债", CategoryType.Income, 0, 70));
            result.Add(Get(4, 0, "退款", CategoryType.Income, 0, 70));
            result.Add(Get(4, 0, "返利", CategoryType.Income, 0, 70));
            result.Add(Get(4, 0, "赔付", CategoryType.Income, 0, 70));

            //兼职
            result.Add(Get(0, 5, "兼职外快", CategoryType.Income, 0, 960));

            //其他
            result.Add(Get(0, 6, "其他", CategoryType.Income, 0));
            result.Add(Get(6, 0, "分红", CategoryType.Income, 0, 70));
            result.Add(Get(6, 0, "漏记款", CategoryType.Income, 0, 70));

            //支出 餐饮
            result.Add(Get(0, 7, "餐饮", CategoryType.Expend, 1, 800));
            result.Add(Get(7, 0, "早餐", CategoryType.Expend, 1));
            result.Add(Get(7, 0, "午餐", CategoryType.Expend, 1));
            result.Add(Get(7, 0, "晚餐", CategoryType.Expend, 1));
            result.Add(Get(7, 0, "买菜", CategoryType.Expend, 0));
            result.Add(Get(7, 0, "水果", CategoryType.Expend, 0));
            result.Add(Get(7, 0, "饮料", CategoryType.Expend, 0));
            result.Add(Get(7, 0, "烟酒", CategoryType.Expend, 0));
            result.Add(Get(7, 0, "奶粉", CategoryType.Expend, 0));
            result.Add(Get(7, 0, "零食", CategoryType.Expend, 0));
            result.Add(Get(7, 0, "夜宵", CategoryType.Expend, 0));
            result.Add(Get(7, 0, "餐饮其他", CategoryType.Expend, 0));

            //交通
            result.Add(Get(0, 8, "交通", CategoryType.Expend, 1, 790));
            result.Add(Get(8, 0, "打车", CategoryType.Expend, 1));
            result.Add(Get(8, 0, "公交", CategoryType.Expend, 1));
            result.Add(Get(8, 0, "火车", CategoryType.Expend, 0));
            result.Add(Get(8, 0, "自行车", CategoryType.Expend, 0));
            result.Add(Get(8, 0, "飞机", CategoryType.Expend, 0));
            result.Add(Get(8, 0, "加油", CategoryType.Expend, 0));
            result.Add(Get(8, 0, "停车费", CategoryType.Expend, 0));
            result.Add(Get(8, 0, "保养费", CategoryType.Expend, 0));
            result.Add(Get(8, 0, "过路费", CategoryType.Expend, 0));
            result.Add(Get(8, 0, "罚款", CategoryType.Expend, 0));
            result.Add(Get(8, 0, "交通其他", CategoryType.Expend, 0));

            result.Add(Get(0, 9, "购物", CategoryType.Expend, 1, 790));
            result.Add(Get(9, 0, "服饰鞋包", CategoryType.Expend, 0));
            result.Add(Get(9, 0, "家居百货", CategoryType.Expend, 0));
            result.Add(Get(9, 0, "美容美发", CategoryType.Expend, 0));
            result.Add(Get(9, 0, "报刊书籍", CategoryType.Expend, 0));
            result.Add(Get(9, 0, "电器", CategoryType.Expend, 0));
            result.Add(Get(9, 0, "玩具", CategoryType.Expend, 0));
            result.Add(Get(9, 0, "纸尿裤", CategoryType.Expend, 0));
            result.Add(Get(9, 0, "珠宝首饰", CategoryType.Expend, 0));
            result.Add(Get(9, 0, "电子数码", CategoryType.Expend, 0));
            result.Add(Get(9, 0, "家具家纺", CategoryType.Expend, 0));
            result.Add(Get(9, 0, "购物其他", CategoryType.Expend, 0));


            result.Add(Get(0, 10, "娱乐", CategoryType.Expend, 1, 790));
            result.Add(Get(10, 0, "旅游", CategoryType.Expend, 0));
            result.Add(Get(10, 0, "网游", CategoryType.Expend, 0));
            result.Add(Get(10, 0, "电影", CategoryType.Expend, 0));
            result.Add(Get(10, 0, "洗浴足疗", CategoryType.Expend, 0));
            result.Add(Get(10, 0, "运动健身", CategoryType.Expend, 0));
            result.Add(Get(10, 0, "卡拉OK", CategoryType.Expend, 0));
            result.Add(Get(10, 0, "麻将棋牌", CategoryType.Expend, 0));
            result.Add(Get(10, 0, "聚会玩乐", CategoryType.Expend, 0));
            result.Add(Get(10, 0, "娱乐其他", CategoryType.Expend, 0));


            result.Add(Get(0, 11, "医教", CategoryType.Expend, 1, 790));
            result.Add(Get(11, 0, "药品", CategoryType.Expend, 0));
            result.Add(Get(11, 0, "住院费", CategoryType.Expend, 0));
            result.Add(Get(11, 0, "预防针", CategoryType.Expend, 0));
            result.Add(Get(11, 0, "学杂教材", CategoryType.Expend, 0));
            result.Add(Get(11, 0, "培训考试", CategoryType.Expend, 0));
            result.Add(Get(11, 0, "家教补习", CategoryType.Expend, 0));
            result.Add(Get(11, 0, "学费", CategoryType.Expend, 0));
            result.Add(Get(11, 0, "出国留学", CategoryType.Expend, 0));
            result.Add(Get(11, 0, "医教其他", CategoryType.Expend, 0));


            result.Add(Get(0, 12, "居家", CategoryType.Expend, 1, 790));
            result.Add(Get(12, 0, "手机电话", CategoryType.Expend, 0));
            result.Add(Get(12, 0, "房款房贷", CategoryType.Expend, 0));
            result.Add(Get(12, 0, "水电燃气", CategoryType.Expend, 0));
            result.Add(Get(12, 0, "住宿房租", CategoryType.Expend, 0));
            result.Add(Get(12, 0, "宽带费", CategoryType.Expend, 0));
            result.Add(Get(12, 0, "快递费", CategoryType.Expend, 0));
            result.Add(Get(12, 0, "物业费", CategoryType.Expend, 0));
            result.Add(Get(12, 0, "家政服务", CategoryType.Expend, 0));
            result.Add(Get(12, 0, "婚庆摄影", CategoryType.Expend, 0));
            result.Add(Get(12, 0, "保险费", CategoryType.Expend, 0));
            result.Add(Get(12, 0, "税手续费", CategoryType.Expend, 0));
            result.Add(Get(12, 0, "生活其他", CategoryType.Expend, 0));

            result.Add(Get(0, 13, "人情", CategoryType.Expend, 1, 790));
            result.Add(Get(13, 0, "礼金红包", CategoryType.Expend, 0));
            result.Add(Get(13, 0, "请客", CategoryType.Expend, 0));
            result.Add(Get(13, 0, "孝敬老人", CategoryType.Expend, 0));
            result.Add(Get(13, 0, "慈善捐款", CategoryType.Expend, 0));
            result.Add(Get(13, 0, "给予", CategoryType.Expend, 0));
            result.Add(Get(13, 0, "代付款", CategoryType.Expend, 0));
            result.Add(Get(13, 0, "人情其他", CategoryType.Expend, 0));


            result.Add(Get(0, 14, "投资", CategoryType.Expend, 1, 790));

            return result;
        }

        private static CategoryDTO Get(int parentId, int initKey, string name, CategoryType inoutType,int commonUse=0,int orderBy=0)
        {
            return new CategoryDTO
            {
                Name = name,
                InitKey = initKey,
                ParentId = parentId,
                InOutType = inoutType,
                IsCommonUse = commonUse,
            };
        }
    }
}