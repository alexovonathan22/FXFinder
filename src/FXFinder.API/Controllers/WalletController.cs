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
    //[Authorize(Policy = "")]
    public class WalletController : ControllerBase
    {
        private readonly IWalletManager _walletManager;
        private readonly ILogger<WalletController> _log;


        public WalletController(IWalletManager wallet, ILogger<WalletController> log)
        {
            _walletManager = wallet;
            _log = log;
        }


        /// <summary>
        /// This endpoint creates a wallet for users
        /// Payload to pass is shown below
        /// </summary>
        /// <remarks>
        /// {
        ///    "currencySymbolToCreateWallet": "eur" 
        /// }
        /// </remarks>
        /// <param name="model"></param>
        /// <returns></returns>
        // POST api/<WalletController>
        [Authorize(Policy = "AuthorizedUsers")]
        [HttpPost("create")]
        public async Task<IActionResult> Post(WalletModel model)
        {
            var response = new APIResponse();
            var (entity, message) = await _walletManager.CreateWallet(model);
            if(entity != null)
            {
                response.Result = entity;
                response.ApiMessage = message;
                response.StatusCode = "00";
                return Ok(response);
            }
            response.ApiMessage = message;
            response.Result = entity;

            return BadRequest(response);

        }

        /// <summary>
        /// This endpoint is called to fund a wallet. 
        /// Payload to pass is shown below
        /// </summary>
        /// <remarks>
        /// {
        ///    "Symbol": "eur",
        ///     "Amount":25,
        ///     "UserId":2,
        ///     "AcctDigits":"0093457"   
        /// }
        /// </remarks>
        /// <param name="model"></param>
        /// <returns></returns>
        // POST api/<WalletController>
        [HttpPost("fund")]
        public async Task<IActionResult> FundWallet(FundWalletModel model)
        {
            var response = new APIResponse();
            var (entity, message) = await _walletManager.FundWallet(model);
            if (entity != null)
            {
                response.Result = entity;
                response.ApiMessage = message;
                response.StatusCode = "00";
                return Ok(response);
            }
            response.ApiMessage = message;
            response.Result = entity;

            return BadRequest(response);

        }

        /// <summary>
        /// This endpoint is called to Withdraw from a wallet. 
        /// Payload to pass is shown below
        /// </summary>
        /// <remarks>
        /// {
        ///    "Symbol": "eur",
        ///     "Amount":25,
        ///     "UserId":2,
        ///     "AcctDigits":"0093457"   
        /// }
        /// </remarks>
        /// <param name="model"></param>
        /// <returns></returns>
        // POST api/<WalletController>
        [Authorize(Policy = "AuthorizedUsers")]
        [HttpPost("withdraw")]
        public async Task<IActionResult> WithdrawFund(FundWalletModel model)
        {
            var response = new APIResponse();
            var (entity, message) = await _walletManager.WithdrawFromWallet(model);
            if (entity != null)
            {
                response.Result = entity;
                response.ApiMessage = message;
                response.StatusCode = "00";
                return Ok(response);
            }
            response.ApiMessage = message;
            response.Result = entity;

            return BadRequest(response);

        }
        /// <summary>
        /// The need for this endpoint is for noob users to be able to check if 
        /// funds have been approved.
        /// Payload to pass is shown below
        /// And its passed as a query string
        /// </summary>
        /// <remarks>
        /// checkfunds/0045674
        /// </remarks>
        /// <param name="walletdigits"></param>
        /// <returns></returns>
        [HttpGet("checkfunds/{walletdigits}")]
        [Authorize(Policy="AuthorizedUsers")]
        public async Task<IActionResult> CheckFunds(string walletdigits)
        {
            var response = new APIResponse();
            var (entity, message) = await _walletManager.CheckFunds(walletdigits);
            if (entity != null)
            {
                response.Result = entity;
                response.ApiMessage = message;
                response.StatusCode = "00";
                return Ok(response);
            }
            response.ApiMessage = message;
            response.Result = entity;

            return BadRequest(response);

        }

        /// <summary>
        /// This endpoint is used by Only admin to Approve funding.
        /// </summary>
        /// <returns></returns>
        [Authorize(Policy = "AuthorizedAdmin")]
        [HttpPost("approvefunds")]
        public async Task<IActionResult> ApproveFunds()
        {
            var response = new APIResponse();
            var (entity, message) = await _walletManager.ApproveFunds();
            if (entity != null)
            {
                response.Result = entity;
                response.ApiMessage = message;
                response.StatusCode = "00";
                return Ok(response);
            }
            response.ApiMessage = message;
            response.Result = entity;

            return BadRequest(response);

        }

    }
}
