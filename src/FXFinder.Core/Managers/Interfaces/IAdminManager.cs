using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FXFinder.Core.DBModels;
using FXFinder.Core.Models;

namespace FXFinder.Core.Managers.Interfaces
{
    public interface IAdminManager
    {
        Task<(PromotionDetail details, string message)> UpgradeDownGrade(AdminPowers model);
    }
}
