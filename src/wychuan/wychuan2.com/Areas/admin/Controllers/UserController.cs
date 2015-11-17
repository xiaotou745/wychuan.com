using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AC.Service.DTO.User;
using AC.Service.Impl.User;
using AC.Service.User;
using wychuan2.com.Areas.admin.Models.User;

namespace wychuan2.com.Areas.admin.Controllers
{
    public class UserController : BaseController
    {
        private readonly IUserService userService = new UserService();
        private readonly IMenuService menuService = new MenuService();
        private readonly IRoleService roleService = new RoleService();

        #region 用户列表

        public ActionResult Index()
        {
            var model = new UserModel
            {
                Users = userService.GetAll(),
                Roles = roleService.GetAll()
            };
            foreach (var user in model.Users)
            {
                user.Roles = new List<string>();
                var roleIds = user.RoleIds.Split(new[]{","}, StringSplitOptions.RemoveEmptyEntries);
                foreach (var roleId in roleIds)
                {
                    var role = model.Roles.FirstOrDefault(r => r.Id.ToString().Equals(roleId));
                    if (role != null)
                    {
                        user.Roles.Add(role.Name);
                    }
                }
            }
            return View(model);
        }

        public ActionResult RefreshUsers()
        {
            var model = new UserModel
            {
                Users = userService.GetAll(),
                Roles = roleService.GetAll()
            };
            foreach (var user in model.Users)
            {
                user.Roles = new List<string>();
                var roleIds = user.RoleIds.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var roleId in roleIds)
                {
                    var role = model.Roles.FirstOrDefault(r=>r.Id.ToString().Equals(roleId));
                    if (role != null)
                    {
                        user.Roles.Add(role.Name);
                    }
                }
            }
            return View("_UserList", model);
        }

        #endregion

        #region Menus

        public ActionResult Menu()
        {
            var model = new MenuModel {Menus = menuService.GetAll()};
            return View(model);
        }

        public ActionResult RefreshMenu()
        {
            var model = new MenuModel {Menus = menuService.GetAll()};
            return View("_MenuList", model);
        }

        #endregion

        #region Roles

        public ActionResult Role()
        {
            var model = new RoleModel();
            model.Roles = roleService.GetAll();
            model.CurrentRole = model.Roles.FirstOrDefault();
            model.Menus = menuService.GetAll();
            return View(model);
        }

        public ActionResult RefreshRole(int roleId)
        {
            var model = new RoleModel();
            model.Roles = roleService.GetAll();
            model.CurrentRole = model.Roles.FirstOrDefault(r=>r.Id==roleId);
            return View("_RolesList", model);
        }
        #endregion
    }
}