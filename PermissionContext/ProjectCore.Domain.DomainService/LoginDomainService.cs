using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ProjectCore.Common;
using ProjectCore.Common.DomainInterfaces;
using ProjectCore.Common.RedisHelper;
using ProjectCore.Domain.Model.Entity;
using ProjectCore.Domain.Repository.Interfaces;

namespace ProjectCore.Domain.DomainService
{
    /// <summary>
    /// 登录领域服务
    /// </summary>
    public class LoginDomainService: IDomainService
    {
        private readonly IBaseRepository<UserInfo> _userRepository;
        private readonly IUserRepository _userBaseRepository;

        private readonly JwtSettings _jwtSettings;
        public LoginDomainService(IBaseRepository<UserInfo> userRepository, IUserRepository userBaseRepository, IOptions<JwtSettings> jwtSettingsAccesser)
        {
            _userRepository = userRepository;
            _userBaseRepository = userBaseRepository;
            _jwtSettings = jwtSettingsAccesser.Value;
        }

        /// <summary>
        /// 登录验证
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<HeaderResult<string>> VerificationUserLogin(string username, string password)
        {
            var user = await _userRepository.WhereLoadEntityAsNoTrackingAsync(e => e.UserName == username);
            if (user != null)
            {
                if (user.IsDisable == true)
                {
                    return new HeaderResult<string> { IsSucceed = false, Message = "用户处于禁用状态！" };
                }
                var userPwd = user.UserPassword;
                if (userPwd == password)
                {
                    var claim = new Claim[]
                    {
                        new Claim("UserId", user.Id.ToString()),
                        new Claim("userName", user.UserName)
                    };

                    //对称秘钥
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
                    //签名证书(秘钥，加密算法)
                    // ReSharper disable once IdentifierTypo
                    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                    //生成token  [注意]需要nuget添加Microsoft.AspNetCore.Authentication.JwtBearer包，并引用System.IdentityModel.Tokens.Jwt命名空间
                    var token = new JwtSecurityToken(_jwtSettings.Issuer, _jwtSettings.Audience, claim, DateTime.Now,
                        DateTime.Now.AddMinutes(30), creds);
                    var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

                    //用redis缓存每一个用户的Token
                    var jwtHeaderToken = "Bearer " + jwtToken;
                    RedisHelper.SetStringKey(user.Id.ToString() + "Token", jwtToken, TimeSpan.FromDays(1));

                    return new HeaderResult<string> { IsSucceed = true, Message = "登录成功！", Result = jwtHeaderToken };
                }
                else
                {
                    return new HeaderResult<string> { IsSucceed = false, Message = "密码错误！" };
                }
            }
            else
            {
                return new HeaderResult<string> { IsSucceed = false, Message = "用户名错误！" };
            }

        }
              
    }
}
