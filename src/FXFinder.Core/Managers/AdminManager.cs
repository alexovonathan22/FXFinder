using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FXFinder.Core.DataAccess;
using FXFinder.Core.DBModels;
using FXFinder.Core.Managers.Interfaces;
using FXFinder.Core.Models;

namespace FXFinder.Core.Managers
{
    public class AdminManager : IAdminManager
    {
        #region fields
        private readonly IRepository<User> repository;
        private readonly ILogger<AdminManager> log;
        private readonly IConfiguration configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;


        #endregion
        public AdminManager(IRepository<User> repository, ILogger<AdminManager> log, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
            this.log = log ?? throw new ArgumentNullException(nameof(log));
            this.configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// 
        /// </summary>
        ///
        /// <param name="model"></param>
        /// <returns>
        ///  <param name="details"></param>
        ///  <param name="message"></param>
        /// </returns>
        public async Task<(PromotionDetail details, string message)> UpgradeDownGrade(AdminPowers model)
        {
            //User user = new User();
            var userContext = _httpContextAccessor.HttpContext.User.Identity.Name;

            if (model == null) return (null, $"{userContext}, Pass the right parameter");
            var getTheUsers = await repository.LoadWhere(u => (u.Id == model.UserId || u.Username == model.Username) || u.Username == userContext);
            if (getTheUsers == null) return (null, $"No user found.");
            var user = getTheUsers.FirstOrDefault(t => t.Username == model.Username);
            var admin = getTheUsers.FirstOrDefault(t => t.Username == userContext);
            if (admin.Role == UserRoles.Admin)
            {
                if (model.PromoteOrDemote)
                {
                    if (user.Role == UserRoles.Noob)
                    {
                        user.Role = UserRoles.Elite;

                        var msg = $"Role changed to {user.Role}";

                        user.ActionTaken = msg;
                        user.ModifiedAt = DateTime.Now;
                        user.ModifiedBy = admin.Username;
                        await repository.Update(user);

                        var detail = new PromotionDetail();
                        detail.IsRoleChanged = true;
                        detail.Message = user.ActionTaken;
                        detail.UpdatedRole = user.Role;


                        return (details: detail, message: msg);
                    }
                    if (user.Role == UserRoles.Elite)
                    {
                        var msge = $"Role unchanged because the user is already an {user.Role} user";

                        var detail = new PromotionDetail
                        {
                            IsRoleChanged = false,
                            Message = msge,
                            UpdatedRole = user.Role
                        };


                        return (details: detail, message: msge);
                    }

                }

                // Demote user.
                if (user.Role == UserRoles.Noob)
                {
                    var msg = $"Role unchanged because the user is already a {user.Role} user";

                    var detail = new PromotionDetail();
                    detail.IsRoleChanged = false;
                    detail.Message = msg;
                    detail.UpdatedRole = user.Role;


                    return (details: detail, message: msg);
                }
                if (user.Role == UserRoles.Elite)
                {

                    user.Role = UserRoles.Noob;
                    
                    var msge = $"Role changed to {user.Role}";

                    user.ActionTaken = msge;
                    user.ModifiedAt = DateTime.Now;
                    user.ModifiedBy = admin.Username;
                    await repository.Update(user);

                    var detail = new PromotionDetail();
                    detail.IsRoleChanged = true;
                    detail.Message = user.ActionTaken;
                    detail.UpdatedRole = user.Role;

                    return (details: detail, message: msge);
                }
            }
            return (details: null, message: $"An Error Occured.");

        }

    }
}
