using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FXFinder.Core.Util;

namespace FXFinder.API.ExtensionClasses
{
    public static class Extensions
    {
        public static void AddAppAuthentication(this IServiceCollection services, string jwtsecret)
        {
            // Add Authentication

            services.AddAuthentication(opt =>
            {

                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(tok =>
                {
                    tok.SaveToken = true;
                    tok.TokenValidationParameters = new TokenValidationParameters();
                    tok.TokenValidationParameters.ValidateIssuerSigningKey = true;
                    tok.TokenValidationParameters.IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtsecret));
                    tok.TokenValidationParameters.ValidateAudience = false;
                    tok.TokenValidationParameters.ValidateIssuer = false;
                    tok.TokenValidationParameters.RequireExpirationTime = false;
                    tok.TokenValidationParameters.ValidateLifetime = true;



                });
        }
    }
}