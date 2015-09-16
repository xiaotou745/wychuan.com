namespace AC.Service.DTO.LiCai
{
    public class BusinessDTO
    {
        /// <summary>
        /// ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// 商家名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 是否常用商家
        /// </summary>
        public int Level { get; set; }

    }
}