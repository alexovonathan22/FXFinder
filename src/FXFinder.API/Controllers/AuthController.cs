using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FXFinder.Core.Managers.Interfaces;
using FXFinder.Core.Models;
using FXFinder.Core.Util;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FXFinder.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthManager _auth;
        private readonly IAdminManager _authAdmin;
        private readonly ILogger<AuthController> _log;


        public AuthController(IAuthManager auth, ILogger<AuthController> log, IAdminManager authAdmin)
        {
            _auth = auth;
            _log = log;
            _authAdmin = authAdmin;
        }

        /// <summary>
        /// This End creates new users where they can choose their user role - Noob or Elite
        /// </summary>
        /// <remarks>
        /// Use as sample Elite/Noob creation
        /// {
        ///    "username": "OvoDGreat",
        ///    "password": "123456"
        /// }
        /// </remarks>
        /// <param name="user"></param>
        /// <returns></returns>
        // api/<AuthController>
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginModel user)
        {
            _log.LogInformation($"Attempting to log user {user.Username}");
            var response = new APIResponse();
            response.ApiMessage = $"Error: Login credentials incorrect!";
            response.StatusCode = "01";
            response.Error = null;
            var jwt = await _auth.LogUserIn(user);
            if (jwt != null)
            {
                response.Result = jwt;
                response.StatusCode = "00";
                response.ApiMessage = "Successful Login!";
                return Ok(response);
            }

            return BadRequest(response);

        }

    /// <summary>
    /// This End creates new users where they can choose their user role - Noob or Elite
    /// </summary>
    /// <remarks>
    /// Use as sample Elite/Noob creation
    ///   {
    ///     "currencySymbol": "eur",
    ///       "username": "OvoDGreat",
    ///       "email": "OvoDGreat@gmail.com",
    ///        "password": "123456",
    ///     "role": "Elite"
    /// }
    /// Note When you run the application for first time, admin is created
    /// with username=adminovo, password=01234Admin
    /// </remarks>
    /// <param name="userdto"></param>
    /// <returns></returns>
        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(SignUp userdto)
        {
            var response = new APIResponse();
            response.StatusCode = "01";
            response.Error = null;
            var (user, message) = await _auth.RegisterUser(userdto); ;
            if (user != null)
            {
                response.Result = user;
                response.StatusCode = "00";
                response.ApiMessage = message;
                return Ok(response);
            }
            response.ApiMessage = message;
            
            return BadRequest(response);
        }

        /// <summary>
        /// This End point is only accessed by Admin. to demote or promote user - noob to elite, elite to noob.
        /// Note:  "PromoteOrDemote" is set to true if user should be demoted.
        /// </summary>
        /// 
        ///<remarks>
        /// {
        ///     "Username": "cobol",
        ///       "userId": 1,
        ///       "PromoteOrDemote":true
        /// }
        /// </remarks>
        /// <param name="userdto"></param>
        /// <returns></returns>
        [HttpPost("promoteuser")]
        [Authorize(Policy = "AuthorizedAdmin")]
        public async Task<IActionResult> AdminPower(AdminPowers userdto)
        {
            var response = new APIResponse();
            response.StatusCode = "01";
            response.Result = null;
            var (user, message) = await _authAdmin.UpgradeDownGrade(userdto); ;
            if (user != null)
            {
                response.Result = user;
                response.StatusCode = "00";
                response.ApiMessage = message;
                return Ok(response);
            }
            response.ApiMessage = message;

            return BadRequest(response);
        }


        // [HttpPost("refresh")]
        // public async Task<IActionResult> RefreshToken(SignUp userdto)
        // {
        //     var response = new APIResponse();
        //     response.StatusCode = "01";
        //     response.Result = null;
        //     var (user, message) = await _auth.RegisterUser(userdto); ;
        //     if (user != null)
        //     {
        //         response.Result = user;
        //         response.StatusCode = "00";
        //         response.ApiMessage = message;
        //         return Ok(response);
        //     }
        //     response.ApiMessage = message;

        //     return BadRequest(response);
        // }

        [HttpPost("verify/{username}")]
        [Authorize(Policy = "AuthorizedAdmin")]
        public async Task<IActionResult> VerifyUser(string username)
        {
            var response = new APIResponse();
            response.StatusCode = "01";
            response.Result = null;
            var (user, message) = await _auth.VerifyUserEmail(username); ;
            if (user != null)
            {
                response.Result = user;
                response.StatusCode = "00";
                response.ApiMessage = message;
                return Ok(response);
            }
            response.ApiMessage = message;

            return BadRequest(response);
        }


    }
}
