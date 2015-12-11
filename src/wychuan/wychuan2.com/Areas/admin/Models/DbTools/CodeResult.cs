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

    /// <summary>
    /// 代码类
    /// </summary>
    public class CodeFileItem
    {
        /// <summary>
        /// Id主要用来作为tab的ID
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// tab标签显示的文本
        /// </summary>
        public string TabText { get; set; }

        /// <summary>
        /// 代码
        /// </summary>
        public string Code { get; set; }
    }
}