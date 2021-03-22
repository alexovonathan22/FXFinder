using System;
using System.Collections.Generic;
using System.Text;
using FXFinder.Core.DBModels;

namespace FXFinder.Core.Models
{
    public class UserWalletModel
    {
        public List<Wallet> Wallet { get; set; }
        public FXUser User { get; set; }

    }
}
