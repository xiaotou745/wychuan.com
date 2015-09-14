using System.Collections.Generic;
using System.Web.Http;
using AC.Json;
using AC.Extension;
using AC.Service.Config;
using Common.Logging;
using ICategoryService = AC.Web.Common.Config.ICategoryService;

namespace AC.Web.Controllers.API
{
    public class CategoryController : ApiController
    {
        private readonly ILog logger = LogManager.GetCurrentClassLogger();

        //private readonly ICategoryService categoryService = new CategoryConfigHelper();

        // GET api/<controller>
        //public IList<CategoryConfigInfo> Get()
        //{
            //return categoryService.GetAll() ?? new List<CategoryConfigInfo>();
        //}

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public AjaxResult Create([FromBody] CategoryConfigInfo value)
        {
            //var categoryId = categoryService.Create(value);
            //if (value.Id <= 0)
            //{
            //    value.Id = categoryId;
            //}
//            var temp = @"
//<tr>
//    <td>{0}<input type='hidden' value='{1}' /></td>
//    <td {2}>{1}</td>
//    <td>{3}</td>
//    <td>{4}</td>
//    <td><a href='javascript:;' name='btnModify'>修改</a> <a>删除</a></td>
//</tr>";
//            var html = temp.format(value.ParentId == 0 ? value.Id.ToString() : "", 
//                value.Id, 
//                value.ParentId == 0 ? "data-id={0}".format(value.Id) : "", 
//                value.Name, value.ParentId);
            return AjaxResult.Success();
        }


        // PUT api/<controller>/5
        public AjaxResult Modify([FromBody] CategoryConfigInfo value)
        {
            //categoryService.Modify(value);
            return AjaxResult.Success();
        }


        public AjaxResult Remove([FromBody] int id, [FromBody] int parentId)
        {
            //categoryService.Remove(id, parentId);
            return AjaxResult.Success();
        }
    }
}