using System.IO;
using AC.Helper;

namespace AC.Web.Common.Config
{
    /// <summary>
    /// 配置文件基类
    /// </summary>
    public class ConfigsManager<T> where T:class
    {
        public ConfigsManager(string filePath)
        {
            this.filePath = filePath;
        }

        private readonly string filePath;

        public T Get()
        {
            if (IsFilePathNullOrEmpty())
            {
                return null;
            }
            if (!File.Exists(filePath))
            {
                return null;
            }
            return XmlHelper.Deserialize<T>(filePath);
        }

        public bool Save(object obj)
        {
            if (IsFilePathNullOrEmpty())
            {
                return false;
            }
            XmlHelper.ToXml(filePath, obj);
            return true;
        }

        private bool IsFilePathNullOrEmpty()
        {
            return string.IsNullOrEmpty(filePath);
        }
    }
}