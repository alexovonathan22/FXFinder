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
        private readonly ILogger<CurrencyController> _log;


        public CurrencyController(ILogger<CurrencyController> log, ICurrencyManager currencyManager)
        {
            _log = log;
            _currencyManager = currencyManager;
        }
        
        
        // TODO: GET ALL CURRENCY / CUR SYMBOLS
        /// <summary>
        /// This End point is only accessed by Admin. to change main currency of users.
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
        [Authorize(Policy = "AuthorizedAdmin")]
        public async Task<IActionResult> Post(CurrencyChangeModel model)
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

      
    }
}
