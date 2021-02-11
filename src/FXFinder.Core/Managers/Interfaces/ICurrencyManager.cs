using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FXFinder.Core.Models;

namespace FXFinder.Core.Managers.Interfaces
{
    public interface ICurrencyManager
    {
        Task<(CurrencyChange entity, string message)> ChangeMainCurrency(string symbol, int userid);
        Task<(ConversionModel entity, string message)> CurrencyConversion(string from, string to, decimal amount);

    }
}
