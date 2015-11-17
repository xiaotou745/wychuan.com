using System.Web.Http;
using AC.Service.DTO.User;
using AC.Service.Impl.User;
using AC.Service.User;
using AC.Web;
using wychuan2.com.Areas.admin.Models.User;

namespace wychuan2.com.Areas.admin.Controllers.api
{
    public class MenuController : ApiController
    {
        private readonly IMenuService menuService = new MenuService();
        private readonly IRoleService roleService = new RoleService();

        #region Save

        public AjaxResult Save(MenuDTO menu)
        {
            if (menu == null)
            {
                return AjaxResult.Error("menu is null");
            }
            if (menu.Id == 0)
            {
                menu.Id = menuService.Create(menu);
            }
            else
            {
                menuService.Modify(menu);
            }
            return AjaxResult.Success(menu.Id);
        }

        #endregion

        #region Remove

        [HttpGet]
        public AjaxResult Remove(int id)
        {
            if (id <= 0)
            {
                return AjaxResult.Error();
            }
            menuService.Remove(id);
            return AjaxResult.Success();
        }
        #endregion

        #region SavePrivilege

        public AjaxResult SavePrivilege(RolePrivilegeParams privilegeParams)
        {
            roleService.SavePrivilege(privilegeParams.RoleId, privilegeParams.MenuIds);
            return AjaxResult.Success();
        }
        #endregion

        #region GetPrivilege
        public AjaxResult GetPrivilege(int roleId)
        {
            var privilege = roleService.GetPrivilege(roleId);
            return AjaxResult.Success(privilege);
        }
        #endregion
    }
}