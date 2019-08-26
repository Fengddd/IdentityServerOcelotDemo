//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net.Http;
//using System.Threading.Tasks;
//using System.Xml;
//using Microsoft.ApplicationInsights.AspNetCore.Extensions;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Http.Extensions;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Filters;
//using ProjectCore.Application.Authority;
//using ProjectCore.Domain.Model;
//using ProjectCore.Web.Session;


//namespace ProjectCore.Web.Filter
//{
//    public class ProjectActionFilter: IAuthorizationFilter
//    {
//        private readonly IHttpContextAccessor _accessor;
//        private readonly IAuthorityService _authorityService;
//        public ProjectActionFilter(IHttpContextAccessor accessor, IAuthorityService authorityService)
//        {
//            _accessor = accessor;
//            _authorityService = authorityService;
//        }

//        public void OnAuthorization(AuthorizationFilterContext context)
//        {
//            var url1 = context.HttpContext.Request.Path.Value;
//            //获取controller的名称
//            var controller = context.RouteData.Values["controller"].ToString().ToLower();
//            //获取Action的名称
//            var action = context.RouteData.Values["Action"].ToString().ToLower();
//            //var user = _accessor.HttpContext.Session.Get<UserInfo>("user");
//            var url = "/"+controller +"/"+ action;
//            //if (controller != "home" && controller != "login")
//            //{
//            //    if (user != null)
//            //    {
//            //        var fileRoleMenuUrl = GetRoleList().Select(e=>e.MenuUrl.ToLower()).ToList();
//            //        if (!fileRoleMenuUrl.Contains(url))
//            //        {
//            //            context.Result = new JsonResult(new { code = 0, msg = "你没有权限" });
//            //        }
//            //    }
//            //}          
//        }

//        public List<MenuInfo> GetRoleList()
//        {
//            var roleMeenuUrl = _accessor.HttpContext.Session.Get<List<MenuInfo>>("Role");

//            if (roleMeenuUrl==null)
//            {
//                var user = _accessor.HttpContext.Session.Get<UserInfo>("user");
//                if (user != null)
//                {
//                    var fileRoleMenuList = _authorityService.FileRoleMenuList(user.UserId);
//                    _accessor.HttpContext.Session.Set<List<MenuInfo>>("Role", fileRoleMenuList);
//                    return fileRoleMenuList;
//                }
//            }        
//            return roleMeenuUrl;
//        }

//        public void OnActionExecuting(ActionExecutingContext context)
//        {
//            Console.WriteLine("执行后");
//        }

       
//    }
//}
