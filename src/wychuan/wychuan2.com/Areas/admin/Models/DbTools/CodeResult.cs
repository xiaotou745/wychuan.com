using System.Collections.Generic;
using AC.Code.Config;

namespace wychuan2.com.Areas.admin.Models.DbTools
{
    /// <summary>
    /// 代码生成结果类
    /// </summary>
    public class CodeResult
    {
        private static readonly CodeResult empty = new CodeResult();
        public static CodeResult Empty
        {
            get { return empty; }
        }

        public CodeResult()
        {
            Codes = new List<CodeFileItem>();
        }

        public CodeLanguage Language { get; set; }

        public IList<CodeFileItem> Codes { get; set; } 
    }

    public class CodeFileItem
    {
        public string Id { get; set; }

        public string TabText { get; set; }

        public string Code { get; set; }
    }
}