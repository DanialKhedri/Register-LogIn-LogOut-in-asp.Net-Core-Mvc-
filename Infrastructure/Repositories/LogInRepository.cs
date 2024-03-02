#region Using
using Domain.Entities;
using Domain.IRepositories;
using Infrastructure.DB;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
#endregion


namespace Infrastructure.Repositories
{
    public class LogInRepository : ILogInRepository
    {

        #region Ctor

        private readonly DataContext _dataContext;
        private readonly IHttpContextAccessor _IhttpContextAccessor;


        public LogInRepository(DataContext dataContext, IHttpContextAccessor httpContextAccessor)
        {
            _dataContext = dataContext;
            _IhttpContextAccessor = httpContextAccessor;
        }


        #endregion



        #region LogIn

        public async Task<Boolean> LogIn(User tempuser)
        {

            #region FindUser
            var user = _dataContext.Users.FirstOrDefault(p => p.UserName == tempuser.UserName && p.PassWord == tempuser.PassWord);
                #endregion


            if(user != null)
            {
                #region SetCoockie
                var claims = new List<Claim>

                {

                new (ClaimTypes.NameIdentifier, user.Id.ToString()),
                new (ClaimTypes.Name, user.UserName),

                  };

                var claimIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(claimIdentity);

                var authProps = new AuthenticationProperties();
                //authProps.IsPersistent = model.RememberMe;

                await _IhttpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, authProps);

                #endregion

                return true;
            }

            else
            {
                return false;
            }
        }

        #endregion
    }
}












