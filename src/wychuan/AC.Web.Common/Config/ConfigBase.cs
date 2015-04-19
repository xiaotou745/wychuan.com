using System.IO;
using AC.Helper;

namespace AC.Web.Common.Config
{
    /// <summary>
    /// 配置文件基类
    /// </summary>
    public abstract class ConfigBase
    {
        protected string FilePath;

        protected T Get<T>() where T : class
        {
            if (IsFilePathNullOrEmpty())
            {
                return null;
            }
            if (!File.Exists(FilePath))
            {
                return null;
            }
            return XmlHelper.Deserialize<T>(FilePath);
        }

        protected bool Save(object obj)
        {
            if (IsFilePathNullOrEmpty())
            {
                return false;
            }
            XmlHelper.ToXml(FilePath, obj);
            return true;
        }

        private bool IsFilePathNullOrEmpty()
        {
            return string.IsNullOrEmpty(FilePath);
        }
    }
}