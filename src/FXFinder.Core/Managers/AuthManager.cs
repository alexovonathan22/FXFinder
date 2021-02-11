using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using FXFinder.Core.DataAccess;
using FXFinder.Core.DBModels;
using FXFinder.Core.Managers.Interfaces;
using FXFinder.Core.Models;
using FXFinder.Core.Util;
using System.Net.Mail;

namespace FXFinder.Core.Managers
{
    public class AuthManager : IAuthManager
    {
        #region fields
        private readonly IRepository<User> repository;
        private readonly ILogger<AuthManager> log;
        private readonly IConfiguration configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IWalletManager _wallet;

        #endregion
        public AuthManager(IRepository<User> repository, ILogger<AuthManager> log, IConfiguration configuration, IHttpContextAccessor httpContextAccessor, IWalletManager wallet)
        {
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
            this.log = log ?? throw new ArgumentNullException(nameof(log));
            this.configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            _wallet = wallet;
        }


        public async Task<TokenModel> LogUserIn(LoginModel model)
        {
            if (model == null) return null;
            User user = new User();
            var userContext = _httpContextAccessor.HttpContext.User.Identity.Name;
            //var userIdentity = (ClaimsIdentity)userContext.Identity;
            //var claim = userIdentity.Claims.ToList();
            //var roleClaimType = userIdentity.RoleClaimType;
            //var roles = claim.Where(c => c.Type == ClaimTypes.Role).Select(d => d.Value).ToList();
            log.LogInformation($"=>> {userContext}");
            try
            {

                user = await repository.FirstOrDefault(u => u.Username == model.Username);
                if (user == null) return null;
            }
            catch (DbUpdateException e)
            {
                log.LogError($"{e.Message}");
            }


            var verifyPwd = AuthUtil.VerifyPasswordHash(model.Password, user.PasswordHash, user.PasswordSalt);
            if (!verifyPwd) return null;

            var claims = new ClaimsIdentity(new[] { new Claim("id", $"{user.Id}"), new Claim(ClaimTypes.Role, user.Role), new Claim(ClaimTypes.Name, user.Username) });
            var jwtSecret = configuration["JwtSettings:Secret"];
            var token = AuthUtil.GenerateJwtToken(jwtSecret, claims);
            claims.AddClaim(new Claim("token", token));

            var refreshToken = AuthUtil.GenerateRefreshToken();

            // Save tokens to DB
            user.AuthToken = token;
            user.RefreshToken = refreshToken;

            await repository.Update(user);
            return new TokenModel 
                { Token = token, RefreshToken = refreshToken, Email=user.Email, UserID=user.Id,
                    Role=user.Role, Username=user.Username
                };
            //throw new NotImplementedException("h");
        }


        public async Task<(UserView user, string message)> RegisterUser(SignUp model)
        {
            User userExists = null;
            var checkrole = model.Role == UserRoles.Noob || model.Role == UserRoles.Elite;
            if (!checkrole)
            {
                return (user: null, message: $"User role incorrect. Do this pass; 'Noob' or 'Elite'.");
            }
            // Validate Email-add in-app
            var (IsValid, Email) = AuthUtil.ValidateEmail(model.Email);

            if (IsValid == false)
            {
                return (user: null, message: $"Email format incorrect.");
            }
            userExists = await repository.FirstOrDefault(r => r.Username == model.Username);

            if (userExists == null)
            {
                var toCapsSymbol = model.CurrencySymbol.ToUpperInvariant();

                var symbolName = configuration[$"SupportedSymbols:{toCapsSymbol}"];
                if (string.IsNullOrEmpty(symbolName)) return (user: null, message: $"Enter a correct currency symbol, {model.CurrencySymbol} incorrect.");
                if (userExists != null) return (user: null, message: $"User {model.Username} exists.");
                //if (userExists.Role === null) return (user: null, message: $"User {model.Username} not allowed to have a user acct!");

               

                AuthUtil.CreatePasswordHash(model.Password, out byte[] passwordHash, out byte[] passwordSalt);
                var userDetails = new User
                {
                    CreatedAt = DateTime.Now,
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt,
                    Username = model.Username,
                    Email = model.Email,
                    CurrencySymbol=toCapsSymbol,
                    CurrencyTitle =symbolName,
                    Role = model.Role
                };

                var newuser = await repository.Insert(userDetails);
                var returnView = new UserView
                {
                    Username=newuser.Username,
                    Email=newuser.Email,
                    MainCurrency=symbolName,
                    MainSymbol = toCapsSymbol,
                    Role = model.Role

                };
      
                // Create Wallet for an Elite only with main currency at signup. 
                if (newuser.Role == UserRoles.Elite || newuser.Role==UserRoles.Noob)
                {
                    var (wallet, message) = await _wallet.GenerateWallet(newuser, symbolName, toCapsSymbol, newuser.Username);

                    returnView.WalletAccounts = wallet;
                    returnView.Message = $"Successful sign up. A {newuser.CurrencySymbol} wallet which is your main currency created for you wallet acct digits is {wallet.WalletAcct}";
                }
                return (user: returnView, message: "User created successfully.");
            }
            return (user: null, message: $"User {model.Username} exists already!");
        }

    }
   
}
