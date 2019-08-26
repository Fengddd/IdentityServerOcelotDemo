using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectCore.Domain.Model.Entity;

namespace ProjectCore.EntityFrameworkCore.Mapping
{
    public class UserMap: IEntityTypeConfiguration<UserInfo>
    {        
        public void Configure(EntityTypeBuilder<UserInfo> builder)
        {
            builder.HasIndex(p => new { p.IsDisable, p.IsDeleted, p.UserName });
        }
    }
}
