using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FXFinder.Core.DataAccess;
using FXFinder.Core.DBModels;
using FXFinder.Core.Managers.Interfaces;
using FXFinder.Core.Models;
using System.Linq;

namespace FXFinder.Core.Managers
{
    public class WalletManager : IWalletManager
    {
        #region Fields 
        private readonly IRepository<User> _userrepo;
        private readonly IRepository<FXUser> _fxuser;
        protected WalletDbContext Context;
        private readonly IRepository<Wallet> _walletrepo;
        private readonly ICurrencyManager _currency;
        private readonly ILogger<WalletManager> log;
        private readonly IConfiguration configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        #endregion 

        #region Constructors
        public WalletManager(IRepository<User> userrepo, ILogger<WalletManager> log, IConfiguration configuration, IHttpContextAccessor httpContextAccessor, IRepository<Wallet> walletrepo, ICurrencyManager currency, WalletDbContext context, IRepository<FXUser> fxuser)
        {
            _userrepo = userrepo ?? throw new ArgumentNullException(nameof(userrepo));
            _walletrepo = walletrepo ?? throw new ArgumentNullException(nameof(walletrepo));
            this.log = log ?? throw new ArgumentNullException(nameof(log));
            this.configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            _currency = currency;
            Context = context;
            _fxuser = fxuser;
        }
        #endregion
        /// <summary>
        /// This method creates a wallet for users. but if a noob user has a wallet already it 
        /// returns an error response.
        /// </summary>
        /// <remarks>
        /// {
        ///    "currencySymbolToCreateWallet": "eur" 
        /// }
        /// </remarks>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<(WalletView entity, string Message)> CreateWallet(WalletModel model)
        {
            var userCtx = _httpContextAccessor.HttpContext.User.Identity.Name;
            var toCapsSymbol = model.CurrencySymbolToCreateWallet.ToUpperInvariant();

            var symbolName = configuration[$"SupportedSymbols:{toCapsSymbol}"];
            if (string.IsNullOrEmpty(symbolName)) return (entity: null, Message: $"Enter a correct currency symbol, {toCapsSymbol} incorrect.");

            var userInDb = await _fxuser.FirstOrDefault(u => u.Username == userCtx && u.IsEmailConfirm == true);

            if (userInDb.Role == UserRoles.Admin)
            {
               return (entity: null, Message: $"Admin Can't create a wallet");
            }

            if (userInDb == null) return (entity: null, Message: $"{userCtx}, you need to verify your email or phone number.");

            //If User is verified go ahead and create the wallet in specified currency
            var create = await GenerateWallet(userInDb, toCapsSymbol,symbolName,userCtx);
           
            return create;
        }

        private async Task<(WalletView entity, string Message)> GenerateEliteWallet(User userInDb, string toCapsSymbol, string symbolName, string userCtx, decimal amt)
        {
            Random random = new Random();
            // Any random integer   
            int num = random.Next(10000, 9999999);
            var walletAcctDigit = $"00{num}";
            var newWallet = new Wallet
            {
                CurrnencyTitle = symbolName,
                CurrencySymbol = toCapsSymbol,
                UserId = userInDb.Id,
                AcctDigits = walletAcctDigit,
                CreatedAt = DateTime.Now,
                ModifiedAt = DateTime.Now,
                ActionTaken = "Created new Wallet",
                CreatedBy = userCtx,
                User = userInDb
            };

           
            var returnToView = new WalletView
            {
                CurrencyTitle = symbolName,
                CurrencySymbol = toCapsSymbol,
                UserId = userInDb.Id,
                CreatedAt = DateTime.Now,
                ModifiedAt = DateTime.Now,
                ActionTaken = "Created new Wallet",
                CreatedBy = userCtx,
                WalletAcct = walletAcctDigit,
                UserCreated=userInDb
            };
            if (amt > 0)
            {
                newWallet.GrandAmount = amt;
                newWallet.Amount = amt;
                newWallet.ActionTaken = $"You tried to fund a wallet that didnt exist so a new one was created. with the amount {newWallet.CurrencySymbol} {amt}";
                returnToView.Amount = amt;
            }
            _ = await _walletrepo.Insert(newWallet);
            return (entity: returnToView, Message: $"Successfully created your wallet.");
        }

        public async Task<(WalletView entity, string Message)> GenerateWallet(FXUser userInDb, string symbolName, string toCapsSymbol, string userCtx)
        {
            Random random = new Random();
            // Any random integer   
            int num = random.Next(10000, 9999999);
            var walletAcctDigit = $"10{num}";
            var newWallet = new Wallet
            {
                CurrnencyTitle = symbolName,
                CurrencySymbol = toCapsSymbol,
                UserId = userInDb.Id,
                AcctDigits = walletAcctDigit,
                CreatedAt = DateTime.Now,
                ModifiedAt = DateTime.Now,
                ActionTaken = "Created new Wallet",
                CreatedBy = userCtx,
            };
            
             newWallet.IsMainCurrency = true;
            
            _ = await _walletrepo.Insert(newWallet);
            var returnToView = new WalletView
            {
                CurrencyTitle = symbolName,
                CurrencySymbol = toCapsSymbol,
                UserId = userInDb.Id,
                CreatedAt = DateTime.Now,
                ModifiedAt = DateTime.Now,
                ActionTaken = "Created new Wallet",
                CreatedBy = userCtx,
                WalletAcct = walletAcctDigit

            };
            return (entity: returnToView, Message: $"Successfully created your wallet.");
        }

        public async Task<(FundedWalletRespnse entity, string Message)> FundWallet(FundWalletModel model)
        {
            if (model.Amount < 1) return (entity: null, Message: $"Enter a correct Amount, {model.Amount} incorrect.");
            var userCtx = _httpContextAccessor.HttpContext.User.Identity.Name;
            var toCapsSymbol = model.Symbol.ToUpperInvariant();
            var fundedResponse = new FundedWalletRespnse();

            var symbolName = configuration[$"SupportedSymbols:{toCapsSymbol}"];
            if (string.IsNullOrEmpty(symbolName)) return (entity: null, Message: $"Enter a correct currency symbol, {toCapsSymbol} incorrect.");
            var userInDb = await _userrepo.FirstOrDefault(u => u.Username == userCtx);

            // funding by admin for elite or noob

            if (userInDb.Role == UserRoles.Admin)
            {
                if (model.UserId < 1) return (entity: null, Message: $"You need to provide a User Id.");
                var getUserToFund = await _userrepo.FirstOrDefault(d => d.Id == model.UserId);

                var walletquery = from userWallet in Context.WalletAccts
                            where getUserToFund.Id == userWallet.UserId
                            select userWallet;
                var userWallets = walletquery.ToList();

                log.LogInformation($"Admin attempting to fund wallet for user {getUserToFund.Role}");
                if (getUserToFund.Role == UserRoles.Noob)
                {

                    return await FundNoob(fundedResponse, toCapsSymbol, userInDb, model, userWallets);
                }

                if (getUserToFund.Role == UserRoles.Elite)
                {
                    return await FundElite(fundedResponse, symbolName, toCapsSymbol, userCtx, getUserToFund, model, userWallets, userInDb);
                }
            }
            if (userInDb == null) return (entity: null, Message: $"Log in to fund wallet");

            var query = from userWallet in Context.WalletAccts
                            where userInDb.Id == userWallet.UserId
                        select userWallet;
            var userWalletInContext = query.ToList();
            //var userWalletInContext = query.ToList().FirstOrDefault();
            // funding for noob
            if (userInDb.Role == UserRoles.Noob)
            {
                return await FundNoob(fundedResponse, toCapsSymbol, userInDb, model, userWalletInContext);
            }

            // funding for elite
            if (userInDb.Role == UserRoles.Elite)
            {


                return await FundElite(fundedResponse,symbolName,toCapsSymbol, userCtx, userInDb, model, userWalletInContext);
                
            }
           

            return (entity: null, Message: $"An error occurred. out side if block");

        }

        /// <summary>
        /// This Method has a job of Funding an elite Wallet.
        /// if a wallet exist with a passed amount it updates it
        /// else it creates it with that currency and amount passed
        /// </summary>
        /// <param name="fundedResponse"></param>
        /// <param name="symbolCaps"></param>
        /// <param name="symbolname"></param>
        /// <param name="userctx"></param>
        /// <param name="userInDb"></param>
        /// <param name="model"></param>
        /// <param name="uwallet"></param>
        /// <returns></returns>
        private async Task<(FundedWalletRespnse entity, string Message)> FundElite(FundedWalletRespnse response, string symbolname, string symbolCaps ,string userctx,User userInDb, FundWalletModel model, List<Wallet> uwallet, User adminInCtx =null)
        {
            if (model == null)
            {
                return (entity: null, Message: $"An error occured  with details passed.");
            }
            var eliteWallets = uwallet;
            Wallet selectedWallet = null;
            try
            {

                selectedWallet = eliteWallets.FirstOrDefault(r => r.AcctDigits == model.AcctDigits && r.CurrencySymbol== symbolCaps && r.UserId == userInDb.Id);
                if(selectedWallet == null && adminInCtx.Username == userctx)
                {
                    return (entity: null, Message: $"Wallet Acct {model.AcctDigits} doesnt exist for {userInDb.Username}.");
                }

            }
            catch(Exception e)
            {
                log.LogError($"An error occured while retrieving wallet. {e.Message}");
                return (entity: null, Message: $"An error occured  while retrieving wallet for {userInDb.Username}");
            }

            // Create a new wallet if Elite user doenst have a wallet in the passed symbol currency
            // Then fund it with passed amount
            var newWallet = new Wallet();
            if (selectedWallet == null && userctx == userInDb.Username)
            {
                // Created wallet with passed currency
                var (newwallet, Message) = await GenerateEliteWallet(userInDb, symbolCaps, symbolname, userInDb.Username, model.Amount);

                // Funding new wallet it.
                newWallet.GrandAmount = newwallet.Amount;
                newWallet.Amount = newwallet.Amount;
                newWallet.AcctDigits = newwallet.WalletAcct;
                newWallet.ActionTaken = $"Created wallet and funded it.";
                newWallet.CreatedAt = DateTime.Now;
                newWallet.ModifiedAt = DateTime.Now;
                newWallet.CreatedBy = userctx;
                newWallet.CurrencySymbol = newwallet.CurrencySymbol;
                newWallet.CurrnencyTitle = newwallet.CurrencyTitle;
                newWallet.UserId = newwallet.UserId;
                await _walletrepo.Update(newWallet);

                // Populate response
                response.AcctDigits = newwallet.WalletAcct;
                response.Amount = newwallet.Amount;
                response.IsApproved = true;
                response.NewAmountAfterApproval = newwallet.Amount;
                response.UserIdToFund = newwallet.UserId;

                return (entity: response, Message:$"Created wallet and funded it for {userctx}. by {userctx}");
            }
            else
            {
                // Fund wallet with passed currency
                selectedWallet.GrandAmount +=model.Amount;
                selectedWallet.Amount += model.Amount;
                selectedWallet.ActionTaken = $"Created wallet and funded it.";
                selectedWallet.CreatedAt = DateTime.Now;
                selectedWallet.ModifiedAt = DateTime.Now;
                selectedWallet.CreatedBy = userctx;
                
                await _walletrepo.Update(selectedWallet);

                // Populate response
                response.AcctDigits = selectedWallet.AcctDigits;
                response.Amount = selectedWallet.GrandAmount;
                response.IsApproved = true;
                response.NewAmountAfterApproval = selectedWallet.Amount;
                response.UserIdToFund = selectedWallet.UserId;

                return (entity: response, Message: $"Wallet Funded for {userctx}");
            }

        }

        private async Task<(FundedWalletRespnse entity, string Message)> FundNoob(FundedWalletRespnse fundedResponse, string symbolCaps, User userInDb, FundWalletModel model, List<Wallet > uwallet)
        {
            //var getUserWallet = await _walletrepo.FirstOrDefault(t => t.UserId == userInDb.Id);
            var getUserWallet = uwallet.FirstOrDefault(r => r.UserId == userInDb.Id);
            string message = "";

            if (getUserWallet == null) return (entity: null, Message: $"WALLET NOT FOUND.");
            if (getUserWallet.CurrencySymbol == symbolCaps)
            {
                var walletdto = getUserWallet;
                walletdto.GrandAmount += model.Amount;
                walletdto.IsCurrencyConverted = true;
                walletdto.ActionTaken = $"Funded Wallet by {userInDb.Username}.";
               
                fundedResponse.AcctDigits = getUserWallet.AcctDigits;
                // This New Amount is the proposed amount user will have after approval.
                fundedResponse.NewAmountAfterApproval = walletdto.GrandAmount;

                // Noob User still sees previous amount
                fundedResponse.Amount = getUserWallet.Amount;
                fundedResponse.UserIdToFund = userInDb.Id;
                
                if (userInDb.Role == UserRoles.Admin)
                {
                    message = $"Successful. wallet is funded.";
                    fundedResponse.IsApproved = true;
                    walletdto.Amount = walletdto.GrandAmount;
                }

                // Update wallet in db when admin has approved
                await _walletrepo.Update(walletdto);
                message = $"Success. Account funded in {getUserWallet.CurrencySymbol}. You just need wait for admin to approve funding.";
                return (entity: fundedResponse, Message: message);

            }
            try
            {
                var (conversionresult, info) = await _currency.CurrencyConversion(symbolCaps, getUserWallet.CurrencySymbol, model.Amount);
                if (conversionresult == null) return (entity: null, Message: $"An error occurred.");

                var walletdto = getUserWallet;
                walletdto.GrandAmount += conversionresult.Result;
                walletdto.IsCurrencyConverted = true;
                walletdto.ActionTaken = $"Funded Wallet by {userInDb.Username}.";
                log.LogInformation($"{conversionresult.Result}");
                await _walletrepo.Update(walletdto);
                fundedResponse.AcctDigits = getUserWallet.AcctDigits;
                // This New Amount is the proposed amount user will have after approval.
                fundedResponse.NewAmountAfterApproval = walletdto.GrandAmount;

                // Noob User still sees previous amount before approval
                fundedResponse.Amount = getUserWallet.Amount;
                fundedResponse.UserIdToFund = userInDb.Id;

                if (userInDb.Role == UserRoles.Admin)
                {
                    message = $"Successful. wallet is funded. And approved.";
                    fundedResponse.IsApproved = true;
                }

                message = $"Success. Account funded in {getUserWallet.CurrencySymbol}. You just need wait for admin to approve funding.";
                return (entity: fundedResponse, Message: message);

            }
            catch (Exception e)
            {
                log.LogError($"An error occurred. {e.Message}");
            }
            return (entity: null, Message: $"An error occurred. out side if block");
        }

        /// <summary>
        /// /// This Method has a job of carrying out a withdrawal for noob and elite Wallet.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<(object entity, string Message)> WithdrawFromWallet(FundWalletModel model)
        {
            var userCtx = _httpContextAccessor.HttpContext.User.Identity.Name;
            if (model == null || model.Amount < 1) return (entity: null, Message: $"Enter a correct details to withdraw. or your amount is less than expected.");

            var toCapsSymbol = model.Symbol.ToUpperInvariant();
            var fundedResponse = new FundedWalletRespnse();

            var symbolName = configuration[$"SupportedSymbols:{toCapsSymbol}"];
            if (string.IsNullOrEmpty(symbolName)) return (entity: null, Message: $"Enter a correct currency symbol, {toCapsSymbol} incorrect.");
            var userInDb = await _userrepo.FirstOrDefault(u => u.Username == userCtx);
            if (userInDb == null) return (entity: null, Message: $"User not found.");
            if (userInDb.Role == UserRoles.Noob)
            {
                var getUserWallet = await _walletrepo.FirstOrDefault(t => t.UserId == userInDb.Id);
                if (getUserWallet == null) return (entity: null, Message: $"WALLET NOT FOUND.");
                if (getUserWallet.CurrencySymbol == toCapsSymbol)
                {
                    if (getUserWallet.Amount < model.Amount) return (entity: null, Message: $"Wallet amount insufficient {getUserWallet.Amount}.");
                    var walletdto = getUserWallet;
                    var withdraw = getUserWallet.GrandAmount - model.Amount;
                    walletdto.IsCurrencyConverted = true;
                    walletdto.GrandAmount = withdraw;

                    // Update wallet in db when admin has approved
                    await _walletrepo.Update(walletdto);
                    fundedResponse.AcctDigits = getUserWallet.AcctDigits;
                    fundedResponse.NewAmountAfterApproval = withdraw;
                    fundedResponse.Amount = walletdto.Amount;
                    return (entity: fundedResponse, Message: $"Successful. Wait for admin approval of withdrawal. \n After Approval you will have {withdraw}, Till then You have {getUserWallet.Amount}");

                }
                try
                {
                    var (conversionresult, message) = await _currency.CurrencyConversion(model.Symbol, getUserWallet.CurrencySymbol, model.Amount);
                    if (conversionresult == null) return (entity: null, Message: $"An error occurred.");

                    if (getUserWallet.Amount < conversionresult.Result) return (entity: null, Message: $"Wallet amount insufficient {getUserWallet.Amount}.");


                    var walletdto = getUserWallet;
                    walletdto.IsCurrencyConverted = conversionresult.Success;
                    var withdraw = getUserWallet.GrandAmount - conversionresult.Result;
                    walletdto.GrandAmount = withdraw;

                    await _walletrepo.Update(walletdto);
                    fundedResponse.AcctDigits = getUserWallet.AcctDigits;
                    fundedResponse.NewAmountAfterApproval = withdraw;
                    fundedResponse.Amount = walletdto.Amount;
                    return (entity: fundedResponse, Message: $"Successful. Wait for admin approval of withdrawal. \n After Approval you will have {withdraw}, Till then You have {getUserWallet.Amount}");

                }
                catch (Exception e)
                {
                    log.LogError($"An error occurred. {e.Message}");
                }

            }
            if(userInDb.Role == UserRoles.Elite)
            {
                // Withdrawals from a wallet acct with a sub-currency 
                // Getting all elite wallets
                var getEliteWallets = await _walletrepo.LoadWhere(t => t.UserId == userInDb.Id);

                // Find main currency wallet with the passed currency and wallet acct digits
                var eliteMainCurrWallet = getEliteWallets.FirstOrDefault(r => r.CurrencySymbol == userInDb.CurrencySymbol && r.UserId == userInDb.Id && r.IsMainCurrency==true);
                if (eliteMainCurrWallet != null && eliteMainCurrWallet.GrandAmount > model.Amount)
                {
                    var withdraw = eliteMainCurrWallet.GrandAmount - model.Amount;
                    eliteMainCurrWallet.GrandAmount = withdraw;
                    eliteMainCurrWallet.ModifiedAt = DateTime.Now;
                    eliteMainCurrWallet.ModifiedBy = userCtx;
                    eliteMainCurrWallet.Amount = withdraw;
                    eliteMainCurrWallet.IsCurrencyConverted = true;
                    eliteMainCurrWallet.ActionTaken = $"Withdrew {eliteMainCurrWallet.CurrencySymbol} {model.Amount}";
                    // Update wallet in db when admin has approved
                    await _walletrepo.Update(eliteMainCurrWallet);
                    fundedResponse.AcctDigits = eliteMainCurrWallet.AcctDigits;
                    fundedResponse.NewAmountAfterApproval = withdraw;
                    fundedResponse.Amount = model.Amount;
                    return (entity: fundedResponse, Message: $"Successful Withdrawal from main currency wallet.");
                }
                 if(eliteMainCurrWallet != null && eliteMainCurrWallet.GrandAmount < model.Amount)
                {

                    fundedResponse.AcctDigits = eliteMainCurrWallet.AcctDigits;
                    fundedResponse.NewAmountAfterApproval = eliteMainCurrWallet.Amount;
                    fundedResponse.Amount = model.Amount;
                    return (entity: fundedResponse, Message: $"Failed. couldnt withdraw from main currency wallet.Low balance {eliteMainCurrWallet.CurrencySymbol} {eliteMainCurrWallet.Amount}, compared with amount to withdraw {model.Amount}.");
                }
                
                if (eliteMainCurrWallet == null) return (entity: null, Message: $"Main wallet not found.");

                var elitewallet = getEliteWallets.FirstOrDefault(r => r.CurrencySymbol == toCapsSymbol && r.AcctDigits == model.AcctDigits && r.IsMainCurrency==false);
                if (elitewallet == null) return (entity: null, Message: $"Wallet not found.");
                
                // If there is enough wallet balance
                if (elitewallet.Amount > model.Amount)
                {
                    var withdraw = elitewallet.GrandAmount - model.Amount;
                    elitewallet.GrandAmount = withdraw;
                    elitewallet.ModifiedAt = DateTime.Now;
                    elitewallet.ModifiedBy = userCtx;
                    elitewallet.Amount = withdraw;
                    elitewallet.IsCurrencyConverted = true;
                    elitewallet.ActionTaken = $"Withdrew {elitewallet.CurrencySymbol} {model.Amount}";
                    
                    // Update wallet in db when admin has approved
                    await _walletrepo.Update(elitewallet);

                    fundedResponse.AcctDigits = elitewallet.AcctDigits;
                    fundedResponse.NewAmountAfterApproval = withdraw;
                    fundedResponse.Amount = model.Amount;
                    return (entity: fundedResponse, Message: $"Successful Withdrawal {userCtx}");
                }
                

                // Withdrawals from a wallet acct with a sub-currency with no balance
                try
                {
                    // in the case Wallet balance is less,
                    // Convert Amount to be withdrawn to main currency.
                    var (conversionresult, message) = await _currency.CurrencyConversion(toCapsSymbol, userInDb.CurrencySymbol, model.Amount);
                    if (conversionresult == null) return (entity: null, Message: $"An error occurred.");

                    // If main currency acct is > amount passed do the withdrawal else return appropriate message.
                    if (eliteMainCurrWallet.GrandAmount > conversionresult.Result)
                    {
                        eliteMainCurrWallet.IsCurrencyConverted = conversionresult.Success;
                        var withdraw = eliteMainCurrWallet.GrandAmount - conversionresult.Result;
                        eliteMainCurrWallet.GrandAmount = withdraw;
                        eliteMainCurrWallet.ModifiedAt = DateTime.Now;
                        eliteMainCurrWallet.ModifiedBy = userCtx;
                        eliteMainCurrWallet.Amount = withdraw;
                        eliteMainCurrWallet.ActionTaken = $"Withdrew {eliteMainCurrWallet.CurrencySymbol} {model.Amount}";

                        await _walletrepo.Update(eliteMainCurrWallet);
                         
                        // Populate response
                        fundedResponse.AcctDigits = eliteMainCurrWallet.AcctDigits;
                        fundedResponse.NewAmountAfterApproval = withdraw;
                        fundedResponse.Amount = model.Amount;
                        return (entity: fundedResponse, Message: $"Successful. Withdraw made on {eliteMainCurrWallet.CurrencySymbol} wallet instead of " +
                            $"{elitewallet.CurrencySymbol} wallet. it has a low balance, please fund it.");
                    }
                    else
                    {
                        return (entity: null, Message: $"Failed. Tried to make withdraw on {eliteMainCurrWallet.CurrencySymbol} wallet instead of " +
                            $"{elitewallet.CurrencySymbol} wallet. but both have low balances, please fund them.");
                    }

                }
                catch (Exception e)
                {
                    log.LogError($"An error occurred. {e.Message}");
                    return (entity: null, Message: $"Error. Something went wrong.");
                }
                 

            }
            return (entity: null, Message: $"An error occurred. out side if block");

        }

        public async Task<(FundedWalletRespnse entity, string Message)> CheckFunds(string model)
        {
            
            if(string.IsNullOrEmpty(model)) return (entity: null, Message: $"Check your params");
            var userCtx = _httpContextAccessor.HttpContext.User.Identity.Name;
            var userInDb = await _userrepo.FirstOrDefault(u => u.Username == userCtx);

           
            var checkWallet = await _walletrepo.FirstOrDefault(e => e.UserId == userInDb.Id && e.AcctDigits==model);
            if(checkWallet ==null) return (entity: null, Message: $"Wallet acct doesnt exist.");
            var response = new FundedWalletRespnse
            {
                AcctDigits = checkWallet.AcctDigits,
                Amount = checkWallet.Amount,
                NewAmountAfterApproval = checkWallet.GrandAmount
            };
            if (checkWallet.GrandAmount != checkWallet.Amount)
            {
                return (entity: response, Message: $"Your funds are not approved yet. Contact admin.");
            }
            response.IsApproved = true;
            return (entity: response, Message: $"Funds are approved.");

        }

        public async Task<(ApprovedFundsModel entity, string Message)> ApproveFunds()
        {
            var userCtx = _httpContextAccessor.HttpContext.User.Identity.Name;
            var userInDb = await _userrepo.FirstOrDefault(u => u.Username == userCtx);

            if (userInDb.Role == UserRoles.Admin) 
            {
                var getUnApproved = await _walletrepo.LoadWhere(t => t.IsCurrencyConverted == true);
                if (getUnApproved.Count == 0) return (entity: null, Message: $"No records for approval.");

                var response = new ApprovedFundsModel();
                var ListOfWalletAcctApproved = new List<string>();
                var lists = getUnApproved;
                foreach(var wallet in lists)
                {
                    wallet.Amount = wallet.GrandAmount;
                    ListOfWalletAcctApproved.Add(wallet.AcctDigits);
                }
                response.ApprovedWalletAccts = ListOfWalletAcctApproved;
                response.ApprovedWallets = lists.Count;
                await _walletrepo.UpdateLists(lists);

                return (entity: response, Message: $"Success. {response.ApprovedWallets} wallets approved.");
            }

            return (entity: null, Message: $"Error occurred with approvals.");

        }
    }
}
