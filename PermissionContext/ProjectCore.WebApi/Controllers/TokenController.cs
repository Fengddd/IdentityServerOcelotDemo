using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Dapper;
using DotNetCore.CAP;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ProjectCore.Application.AppUser;
using ProjectCore.Application.AppUser.Dto;
using ProjectCore.Common;
using ProjectCore.Common.Dapper;
using ProjectCore.Common.Event;
using ProjectCore.Common.RabbitMqEvent;
using ProjectCore.Domain.Model.Entity;
using ProjectCore.WebApi.Filter;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProjectCore.WebApi.Controllers
{
    [Route("api/[controller]/[Action]")]
    [EnableCors("any")]
    public class TokenController : ApiControllerBase
    {
        private readonly JwtSettings _jwtSettings;
        private readonly ILoginService _loginService;
        private readonly IApplicationEventBus _publishEventBus;
        private readonly ICapPublisher _capBus;
        public TokenController(IApplicationEventBus eventBus, ICapPublisher capBus, IOptions<JwtSettings> jwtSettingsAccesser,ILoginService loginService)
        {
            _jwtSettings = jwtSettingsAccesser.Value;
            _loginService = loginService;
            _publishEventBus = eventBus;
            _capBus = capBus;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<HeaderResult<string>> GetSignatureToken([FromBody]UserModelDto input)
        {           
            if (ModelState.IsValid)
            {
                return await _loginService.VerificationUserLogin(input);
            }
            return new HeaderResult<string>
            {
                Message = "错误",
                IsSucceed = false
            };

        }
        /// <summary>
        /// 发布
        /// </summary>
        [HttpPost]
        //[AllowAnonymous]      
        public void PublishTestQueue()
        {
            //_capBus.Publish("services.show.time", new CeshiEvent("CeshiEvent"));
            //_capBus.Publish("services.show.time.one", new CeshiEvent("CeshiEventOne"));
            //_capBus.Publish("dddddddd", new CeshiEvent("CeshiEventOne"));
        }

        /// <summary>
        /// 订阅
        /// </summary>
        [HttpPost]
        //[CapSubscribe("services.show.time", Group= "Group")]
        public void GetTestQueue(CeshiEvent ce)
        {
            
        }

        [HttpPost]
        //[CapSubscribe("services.show.time.one")]
        public void GetTest()
        {
            
        }
        
        /// <summary>
        /// 测试
        /// </summary>
        /// <param name="msg"></param>
        [HttpPost]
        [RequestAuthorize]
        public void CheckReceivedMessage(string msg)
        {
            var c = ClaimsUserId;                    
       
        }


    }

   

}
