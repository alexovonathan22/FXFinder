using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FXFinder.Core.DBModels;
using FXFinder.Core.Models;

namespace FXFinder.Core.Managers.Interfaces
{
    public interface IWalletManager
    {
        Task<(FundedWalletRespnse entity, string Message)> FundWallet(FundWalletModel model);
        Task<(object entity, string Message)> WithdrawFromWallet(FundWalletModel model);
        Task<(WalletView entity, string Message)> CreateWallet(WalletModel model);
        Task<(FundedWalletRespnse entity, string Message)> CheckFunds(string model);
        Task<(ApprovedFundsModel entity, string Message)> ApproveFunds();
        Task<(WalletView entity, string Message)> GenerateWallet(User userInDb, string symbolName, string toCapsSymbol, string userCtx);
    }
}
