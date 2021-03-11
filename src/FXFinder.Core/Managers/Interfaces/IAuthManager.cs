using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FXFinder.Core.DBModels;
using FXFinder.Core.Models;

namespace FXFinder.Core.Managers.Interfaces
{
    public interface IAuthManager
    {
        Task<TokenModel> LogUserIn(LoginModel model);
        Task<(UserView user, string message)> RegisterUser(SignUp model);
        Task<(UserView user, string message)> VerifyUserEmail(OneTimePassword otp);
        Task<List<FXUser>> GetAppUsers();
    }
}
