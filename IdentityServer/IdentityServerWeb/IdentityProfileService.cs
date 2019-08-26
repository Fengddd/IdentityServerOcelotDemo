using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using IdentityServer4.Test;
using Microsoft.Extensions.Logging;

namespace IdentityServerWeb
{
    public class IdentityProfileService: IProfileService
    {
        /// <summary>
        /// The logger
        /// </summary>
        protected readonly ILogger Logger;


        /// <summary>
        /// Initializes a new instance of the <see cref="TestUserProfileService"/> class.
        /// </summary>
        /// <param name="users">The users.</param>
        /// <param name="logger">The logger.</param>
        public IdentityProfileService(ILogger<TestUserProfileService> logger)
        {
            Logger = logger;
        }

        /// <summary>
        /// 只要有关用户的身份信息单元被请求（例如在令牌创建期间或通过用户信息终点），就会调用此方法
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public virtual Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            context.LogProfileRequest(Logger);

            //判断是否有请求Claim信息
            if (context.RequestedClaimTypes.Any())
            {
                var userClaims = new List<Claim>
                {
                    new Claim("role", "测试1"),
                    new Claim("name", "测试2"),
                };
                List<TestUser> userList = new List<TestUser>()
                {
                    new TestUser(){SubjectId = "cfac01a9-ba15-4678-bccb-cc22d7896362",Password = "123456",Username="李锋",Claims = userClaims},
                    new TestUser(){SubjectId = "cfac01a9-ba15-4678-bccb-cc22d7855555",Password = "123456",Username="张三"},
                };
                TestUserStore userStore = new TestUserStore(userList);
                //根据用户唯一标识查找用户信息
                var user = userStore.FindBySubjectId(context.Subject.GetSubjectId());
                if (user != null)
                {
                    //调用此方法以后内部会进行过滤，只将用户请求的Claim加入到 context.IssuedClaims 集合中 这样我们的请求方便能正常获取到所需Claim
                    context.AddRequestedClaims(user.Claims);

                }
                //context.IssuedClaims=userClaims;
            }
            context.LogIssuedClaims(Logger);

            return Task.CompletedTask;
        }

        /// <summary>
        /// 验证用户是否有效 例如：token创建或者验证
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public virtual Task IsActiveAsync(IsActiveContext context)
        {
            Logger.LogDebug("IsActive called from: {caller}", context.Caller);
            var userClaims = new List<Claim>
            {
                new Claim("demo1", "测试1"),
                new Claim("demo2", "测试2"),
            };
            List<TestUser> userList = new List<TestUser>()
            {
                new TestUser(){SubjectId = "cfac01a9-ba15-4678-bccb-cc22d7896362",Password = "123456",Username="李锋",Claims = userClaims},
                new TestUser(){SubjectId = "cfac01a9-ba15-4678-bccb-cc22d7855555",Password = "123456",Username="张三"},
            };
            TestUserStore userStore = new TestUserStore(userList);
            var user = userStore.FindBySubjectId(context.Subject.GetSubjectId());
            context.IsActive = user?.IsActive == true;

            return Task.CompletedTask;
        }
    }
}
