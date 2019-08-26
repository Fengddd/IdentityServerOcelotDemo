using System;
using System.Collections;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ProjectCore.Application.AppUser.Dto;
using ProjectCore.Common;
using ProjectCore.Common.RedisHelper;
using StackExchange.Redis.Extensions.Core.Extensions;

namespace ProjectCore.WebApi.Filter
{
    /// <summary>
    /// 验证Jwt的信息
    /// </summary>
    public class RequestAuthorizeAttribute : AuthorizeAttribute, IAuthorizationFilter
    {    
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            //从http请求的头里面获取身份验证信息，验证Jwt 
            var jwtKey = context.HttpContext.Request.Headers["Authorization"].ToString();      
            if (!string.IsNullOrEmpty(jwtKey))
            {
                //截取Bearer
                var jwtStr = jwtKey.Substring(7);
                //解析Token
                var jwtSecurityToken = new JwtSecurityTokenHandler().ReadJwtToken(jwtStr);
                var userId= jwtSecurityToken.Claims.FirstOrDefault(e => e.Type == "userId")?.Value;
                var token= RedisHelper.GetStringKey(userId + "Token");
                if (jwtStr != token) {
                    context.Result = new JsonResult(new HeaderResult<string>
                    {
                        IsSucceed = false,
                        Message = "请重新登录，Token已失效!",
                        StatusCode = HttpStatusCode.Unauthorized.GetHashCode().ToString()
                    });
                }
            }
            //如果取不到身份验证信息，并且不允许匿名访问，则返回未验证401
            else
            {
                context.Result = new JsonResult(new HeaderResult<string>
                {
                    IsSucceed = false,
                    Message = "请求未授权",
                    StatusCode = HttpStatusCode.Unauthorized.GetHashCode().ToString()
                });
            }
        }
    }
}
