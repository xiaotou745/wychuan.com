namespace AC.Service.DTO.Blog
{
    /// <summary>
    /// sectionId已存在
    /// </summary>
    public class SectionIdExistsException : ServiceException
    {
        public override string Message
        {
            get
            {
                return "SectionId已存在。";
            }
        }
    }

    public class BlogIdExistsException : ServiceException
    {
        public override string Message
        {
            get
            {
                return "BlogId已存在，不允许重复。";
            }
        }
    }
}