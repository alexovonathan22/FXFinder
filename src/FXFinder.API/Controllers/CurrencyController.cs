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
    public class CurrencyController : ControllerBase
    {
        private readonly ICurrencyManager _currencyManager;


        public CurrencyController(ICurrencyManager currencyManager)
        {
            _currencyManager = currencyManager;
        }
        
        
        // TODO: GET ALL CURRENCY / CUR SYMBOLS
        /// <summary>
        /// This End point is accessed by Users to change wallet currency.
        /// </summary>
        /// <remarks>
        /// {
        ///    "symbol":"eur",
        ///     "userId": 2
        /// }
        /// </remarks>
        /// <param name="model">
        /// you have to pass the currency symbol that will replace the main currency
        /// then the userId also
        /// </param>
        /// <returns></returns>
        // POST api/<CurrencyController>
        [HttpPost("changecurrency")]
        [Authorize(Policy = "AuthorizedUsers")]
        public async Task<IActionResult> ChangeUserCurrency(CurrencyChangeModel model)
        {
            var response = new APIResponse();
            var (entity, message) = await _currencyManager.ChangeMainCurrency(model.Symbol, model.UserId);
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
        /// This endpoint is responsible for fetching all 
        /// trade currencies.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("fetchcurrencies")]
        [Authorize(Policy = "AuthorizedUsers")]
        public async Task<IActionResult> FetchCurrencies()
        {
            var response = new APIResponse();
            var (entity, message) = await _currencyManager.FetchCurrencies();
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
