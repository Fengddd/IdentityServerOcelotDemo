using System;
using System.Collections.Generic;
using System.Security.Claims;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;

namespace IdentityServerWeb
{
    public class IdentityConfig
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResource("delimitClaim","delimitClaim",new List<string>(){ "role", "name"}), 
            };
        }

        /// <summary>
        /// 这里指定了name和display name, 以后api使用authorization server的时候, 这个name一定要一致
        /// 资源
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<ApiResource> GetApiResource()
        {
            return new List<ApiResource>
            {              
                new ApiResource("identityServerApi", "identityServerApi"),
                //new ApiResource("delimitClaim","delimitClaim",new List<string>(){ "demo1", "demo2"})

            };
        }

        /// <summary>
        /// 客户端
        /// 其中ClientSecrets是Client用来获取token用的. AllowedGrantType: 这里使用的是通过用户名密码和ClientCredentials来换取token的方式. ClientCredentials允许Client只使用ClientSecrets来获取token. 这比较适合那种没有用户参与的api动作. AllowedScopes: 这里只用socialnetwork
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Client> GetClients()
        {

            return new List<Client>
            {
                new Client
                {
                    ClientId = "client",

                    // 没有交互性用户，使用 clientid/secret 实现认证。
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                   
                    // 用于认证的密码
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())

                    },
                    // 客户端有权访问的范围（Scopes）
                    AllowedScopes = { "identityServerApi" }
                },
                new Client
                {
                  ClientId = "mvc",
                  ClientName = "MVC Client",
                  AllowedGrantTypes = GrantTypes.Hybrid,
                  ClientSecrets =
                  {
                      new Secret("secret".Sha256())
                  },
                  AlwaysIncludeUserClaimsInIdToken = true,
                  // 登录成功回调处理地址，处理回调返回的数据
                  RedirectUris = { "http://localhost:12878/signin-oidc" },
                  
                    // where to redirect to after logout
                    PostLogoutRedirectUris = { "http://localhost:12878/signout-callback-oidc" },
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "identityServerApi",
                        "delimitClaim"
                    },
                    AllowOfflineAccess = true
                 },
                new Client
                {
                    ClientId = "mvcTwo",
                    ClientName = "MVC Client",
                    AllowedGrantTypes = GrantTypes.Hybrid,
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    // 登录成功回调处理地址，处理回调返回的数据
                    RedirectUris = { "http://localhost:60121/signin-oidc" },
                    AlwaysIncludeUserClaimsInIdToken = true,
                    // where to redirect to after logout
                    PostLogoutRedirectUris = { "http://localhost:60121/signout-callback-oidc" },
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "identityServerApi",
                        "delimitClaim"
                    },
                    AllowOfflineAccess = true
                },
                new Client
                {
                    ClientId = "ResourceOwnerPassword",
                    ClientName = "ResourceOwnerPassword Client",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "identityServerApi"
                    },
                    AllowOfflineAccess = true

                },
                new Client
                {                
                    ClientId = "js", //客户端Id
                    ClientName = "JavaScript Client", //客户端名称
                    AllowedGrantTypes = GrantTypes.Code, //授权模式
                    //AllowedGrantTypes = GrantTypes.Implicit,
                    RequirePkce = true,
                    RequireClientSecret = false,
                    RequireConsent = false, //禁用 consent 页面确认                    
                    AllowAccessTokensViaBrowser = true,
                    AlwaysIncludeUserClaimsInIdToken = true,
                    RedirectUris =
                    {
                        "http://localhost:8080/#/IdentityServerCallBack",  //登陆后回调页面 
                        "http://localhost:8080/#/IdentityServerRefreshToken" //刷新Token的页面
                    },
                    PostLogoutRedirectUris = { "http://localhost:8080/#/IdentityServerClient" },//注销退出后跳转的页面（登录页）                   
                    AllowedCorsOrigins = { "http://localhost:8080" }, //跨域
                    AccessTokenLifetime = 60, //AccessToken 的有效事件
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "identityServerApi", //授权的Scopes
                        "delimitClaim" //Claims 信息
                    },
                    AllowOfflineAccess = true, 
                    
                }
            };
        }

        /// <summary>
        /// 用户
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<TestUser> GetUsers()
        {
            return new[]
            {
                new TestUser
                {
                    SubjectId = "1",
                    Username = "李锋",
                    Password = "123456",
                    Claims = new []
                    {
                        new Claim("userId", Guid.NewGuid().ToString()),
                        new Claim("userPhone", "8888888888"),
                        new Claim("userRole", "Admin")
                    }
                }

            };
        }
    }
}
