using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using FXFinder.Core.DBModels;
using FXFinder.Core.Util;

namespace FXFinder.Core.DataAccess
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            //create admin password
            var pwd = "01234Admin";
            AuthUtil.CreatePasswordHash(pwd, out byte[] passwordHash, out byte[] passwordSalt);

            //User
            modelBuilder.Entity<User>()
                .HasData(
                   new User { Id = 10, Username = "adminovo", CreatedAt=DateTime.Now,
                    Email="avo.nathan@gmail.com",Role=UserRoles.Admin,PasswordHash=passwordHash, PasswordSalt=passwordSalt
                   }
            );
        }
    }
}
