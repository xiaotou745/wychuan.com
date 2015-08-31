using System.IO;
using System.Web.UI.WebControls;
using AC.Helper;
using AC.Web.Common.Config;

namespace AC.Tests.Common
{
    public class ConfigBase<T> where T : class
    {
        protected string fileName;

        protected T Get()
        {
            if (IsFilePathNullOrEmpty())
            {
                return null;
            }
            if (!File.Exists(fileName))
            {
                return null;
            }
            return XmlHelper.Deserialize<T>(fileName);
        }

        protected bool Save(object obj)
        {
            if (IsFilePathNullOrEmpty())
            {
                return false;
            }
            XmlHelper.ToXml(fileName, obj);
            return true;
        }

        protected bool IsFilePathNullOrEmpty()
        {
            return string.IsNullOrEmpty(fileName);
        }
    }

    public class MenuConfig
    {
        private ConfigBase<MenuInfo[]> manager;


    }
}
