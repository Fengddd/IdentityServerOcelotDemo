using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectCore.Application.AppUser.Dto
{
   public class UserRoleCeShiDto
    {
        public UserDto UserDto { get; set; }
        public string RoleName { get; set; }
        public Guid UserRoleId { get; set; }
    }
}
