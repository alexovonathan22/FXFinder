using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FXFinder.Core.DataAccess;
using FXFinder.Core.DBModels;
using FXFinder.Core.Managers.Interfaces;
using FXFinder.Core.Models;
using FXFinder.Core.Util;
using System.Linq;


namespace FXFinder.Core.Managers
{
    public class CurrencyManager : ICurrencyManager
    {
        #region fields
        private readonly IRepository<User> _userrepository;
        private readonly IRepository<Wallet> _wallet;
        private readonly WalletDbContext _ctx;
        private readonly ILogger<CurrencyManager> log;
        private readonly IConfiguration configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public IHttpClientFactory _httpClientFactory { get; set; }

        #endregion
        public CurrencyManager(IRepository<User> userrepository, ILogger<CurrencyManager> log, IConfiguration configuration, IHttpContextAccessor httpContextAccessor, IHttpClientFactory httpClientFactory, IRepository<Wallet> wallet = null, WalletDbContext ctx = null)
        {
            _userrepository = userrepository ?? throw new ArgumentNullException(nameof(userrepository));
            this.log = log ?? throw new ArgumentNullException(nameof(log));
            this.configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            _httpClientFactory = httpClientFactory;
            _wallet = wallet;
            _ctx = ctx;
        }

        public async Task<(CurrencyChange entity, string message)> ChangeMainCurrency(string symbol, int userid)
        {
            var userCtx = _httpContextAccessor.HttpContext.User.Identity.Name;
            var toCapsSymbol = symbol.ToUpperInvariant();

            var symbolName = configuration[$"SupportedSymbols:{toCapsSymbol}"];
            if (string.IsNullOrEmpty(symbolName)) return (entity: null, message: $"Enter a correct currency symbol, {toCapsSymbol} incorrect.");

            var userInDb = await _userrepository.FirstOrDefault(u => u.Username == userCtx);
            if (userInDb == null) return (entity: null, message: $"User doesnt exist");
            if (userInDb.Role != UserRoles.Admin) return (entity: null, message: $"{userCtx}, you cannot change main currency.");

            //var userToChangeCurrency = await _userrepository.FirstOrDefault(u => u.Id == userid);


            var userToChangeCurrency = await _userrepository.LoadById(userid);
            var userWalletToChangeCurr = await _wallet.FirstOrDefault(r => r.UserId == userToChangeCurrency.Id && r.IsMainCurrency==true);
            if (userToChangeCurrency == null) return (entity: null, message: $"User doesnt exist.");
            if (toCapsSymbol == userToChangeCurrency.CurrencySymbol) return (entity: null, message: $"Provide a new currency. {toCapsSymbol} is what you {userToChangeCurrency.Username} use.");


            var userModel = userToChangeCurrency;
            var OldSymbol = userToChangeCurrency.CurrencySymbol;
            var OldSymbolTitle = userToChangeCurrency.CurrencyTitle;

            userModel.CurrencySymbol = toCapsSymbol;
            userModel.CurrencyTitle = symbolName;
            try
            {
                var (entity, message) = await CurrencyConversion(OldSymbol, userModel.CurrencySymbol, userWalletToChangeCurr.GrandAmount);

                userWalletToChangeCurr.GrandAmount = entity.Result;
                userWalletToChangeCurr.Amount = entity.Result;
                userWalletToChangeCurr.IsCurrencyConverted = entity.Success;
                userWalletToChangeCurr.User = userModel;
                
                await _userrepository.Update(userModel);
                await _wallet.Update(userWalletToChangeCurr);

                var curr = new CurrencyChange
                {
                    FormerMainCurrencySymbol = OldSymbol,
                    FormerMainCurrencyTitle = OldSymbolTitle,
                    NewMainCurrencySymbol = toCapsSymbol,
                    NewMainCurrencyTitle = symbolName,
                    NewAmountInWalletWithChangedCurr = $"{toCapsSymbol} {userWalletToChangeCurr.GrandAmount}"
                };
                return (entity: curr, message: $"Successfully changed currency.");
            }
            catch
            {
                return (entity: null, message: $"An error occured. While updating currency.");
                
            }

           
        }

        public async Task<(ConversionModel entity, string message)> CurrencyConversion(string from, string to, decimal amount)
        {
            /* http://data.fixer.io/api/convert?access_key=API_KEY&from=GBP&to=JPY&amount=25*/
            if(from == to && amount == 0) return (entity: null, message: $"Pass different currency symbols,.");
            var toCapsSymbolFrom = from.ToUpperInvariant();
            var toCapsSymbolTo = to.ToUpperInvariant();


            var symbolNameFrom = configuration[$"SupportedSymbols:{toCapsSymbolFrom}"];
            var symbolNameTo = configuration[$"SupportedSymbols:{toCapsSymbolFrom}"];
            if (string.IsNullOrEmpty(symbolNameFrom) || string.IsNullOrEmpty(symbolNameTo)) return (entity: null, message: $"Pass correct currency symbol.");

            var baseurl = configuration["BaseFixerUrl"];
            var accesskey = configuration["AccessKey"];
            var querybuilder = $"convert?access_key={accesskey}&from={toCapsSymbolFrom}&to={toCapsSymbolTo}&amount={amount}";
            try
            {
                var httpClient = _httpClientFactory.CreateClient("FixerApi");
                var result = await MakeHttpCall.GetJsonResult(httpClient, querybuilder);
                return (entity: result, message: $"Successful conversion.");
            }
            catch(Exception e)
            {
                log.LogError($"{e}");
                return (entity: null, message: $"Couldnt complete Conversion.");
            }
            
        }
    }
}
