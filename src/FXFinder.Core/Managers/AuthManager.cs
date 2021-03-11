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
using FXFinder.Core.Util.Models;

namespace FXFinder.Core.Managers
{
    public class AuthManager : IAuthManager
    {
        #region fields
        private readonly IRepository<FXUser> _userrepo;
        private readonly IEmailUtil _mail;

        private readonly ILogger<AuthManager> log;
        private readonly IConfiguration configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IWalletManager _wallet;

        #endregion
        public AuthManager(IRepository<FXUser> repository, ILogger<AuthManager> log, IConfiguration configuration, IHttpContextAccessor httpContextAccessor, IWalletManager wallet, IEmailUtil mail)
        {
            _userrepo = repository ?? throw new ArgumentNullException(nameof(repository));
            this.log = log ?? throw new ArgumentNullException(nameof(log));
            this.configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            _wallet = wallet;
            _mail = mail;
        }

        public async Task<List<FXUser>> GetAppUsers()
        {
            var users = await _userrepo.LoadWhere(i => i.Role != UserRoles.Admin);

            return users;
        }

        /// <summary>
        /// Service method to log a user in.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<TokenModel> LogUserIn(LoginModel model)
        {
            if (model == null) return null;
            FXUser user = new FXUser();
            var userContext = _httpContextAccessor.HttpContext.User.Identity.Name;
            //_httpContextAccessor.HttpContext.Request.
            //get bsae url wt above
            log.LogInformation($"Attempting to retrieve user {userContext} info.");
            try
            {

                user = await _userrepo.FirstOrDefault(u => u.Username == model.Username);
                if (user == null) return null;
            }
            catch (Exception e)
            {
                log.LogError($"{e.Message}");
                return null;
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

            await _userrepo.Update(user);
            return new TokenModel 
            { 
                Token = token, 
                RefreshToken = refreshToken,
                Email=user.Email, 
                UserID=user.Id,
                Role=user.Role, 
                Username=user.Username
            };
            //throw new NotImplementedException("h");
        }

        /// <summary>
        /// Service method to register a new user
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<(UserView user, string message)> RegisterUser(SignUp model)
        {
            var userExists = await _userrepo.FirstOrDefault(r => r.Username == model.Username);
            if (userExists != null)
            {
                return (user: null, message: $"User {userExists.Username} exists.");
            }
            // Validate Email-add in-app
            var (IsValid, Email) = AuthUtil.ValidateEmail(model.Email);

            if (IsValid == false)
            {
                return (user: null, message: $"Email format incorrect.");
            }

            if (userExists == null)
            {

                AuthUtil.CreatePasswordHash(model.Password, out byte[] passwordHash, out byte[] passwordSalt);
                var userDetails = new FXUser
                {
                    CreatedAt = DateTime.Now,
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt,
                    Username = model.Username,
                    Email = model.Email,
                    Role = UserRoles.User
                };

                //Send verification notification 
                Random rand = new Random();
                string digits = rand.Next(0, 999999).ToString("D6");
                var msg = new EmailMessage();
                msg.ToEmail = model.Email;
                msg.Subject = $"Security Code - OTP";
                msg.Body = $"Your OTP is {digits}";

                userDetails.OTP = digits;
                var newuser = await _userrepo.Insert(userDetails);
               
                var sendmail = await _mail.SendEmailAsync(msg);

                if (sendmail)
                {
                    var returnView = new UserView
                    {
                        Username = newuser.Username,
                        Email = newuser.Email,
                        LastName = model.LastName,
                        FirstName = model.FirstName,
                        Message = $"An Email has been sent to {model.Email} for verification.",
                        IsVerified = false
                    };

                    return (user: returnView, message: $"User created successfully. Check email {model.Email} for an OTP.");
                }
            }
            return (user: null, message: $"Something went wrong.");
        }

        /// <summary>
        /// Service method to Verify user for now via email.
        /// Note: If a user is not verified such a one cannot create a wallet.
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public async Task<(UserView user, string message)> VerifyUserEmail(OneTimePassword otp)
        {
            var userExists = await _userrepo.FirstOrDefault(r => r.Username == otp.Username);
            if (userExists == null)
            {
                return (user: null, message: @$"PLease Create an account with us. {otp.Username} doesn't exist");
            }

            if(userExists.OTP == otp.Otp)
            {
                userExists.IsEmailConfirm = true;
                userExists.OTP = string.Empty;
                await _userrepo.Update(userExists);
                var returnView = new UserView
                {
                    Username = userExists.Username,
                    Email = userExists.Email,
                    IsVerified = true,
                    Message = $"{userExists.Username}, your account verified."
                };


                return (user: returnView, message: "User verified successfully.");
            }

            return (user: null, message: "User verification unsuccessful.");
        }
    }
   
}
