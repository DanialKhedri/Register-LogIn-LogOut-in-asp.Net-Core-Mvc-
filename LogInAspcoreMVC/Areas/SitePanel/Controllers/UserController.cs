using Application.DTOs;
using Application.IServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LogInAspcoreMVC.Areas.SitePanel.Controllers
{
    public class UserController : Controller
    {

        //Ctor
        #region Ctor

        private readonly IAddUserService _IAddUserService;
        private readonly ILogInService _ILogInService;

        public UserController(IAddUserService addUserService, ILogInService logInService)
        {
            _ILogInService = logInService;
            _IAddUserService = addUserService;
        }

        #endregion


        //Index
        #region Index
        [Area("SitePanel")]
        public IActionResult Index()
        {
            return View();
        }

        #endregion

        //Register
        #region Register
        [Area("SitePanel"), HttpGet]
        public async Task<IActionResult> Register()
        {
            return View();
        }

        [Area("SitePanel"), HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(UserRegisterDTO userRegisterDTO)
        {
            if (ModelState.IsValid)
            {
                bool IsSuccses = await _IAddUserService.AddUser(userRegisterDTO);

                if (IsSuccses)
                {
                    return RedirectToAction(nameof(Index));
                }

                else if (!IsSuccses)
                {
                    TempData["Message"] = "UserName Or PhoneNumber is Used Before";


                }
            }

            return View();


        }

        #endregion


        //LogIn
        #region LogIn
        [Area("SitePanel"), HttpGet]
        public async Task<IActionResult> LogIn()
        {
            return View();
        }

        [Area("SitePanel"), HttpPost]
        public async Task<IActionResult> LogIn(UserLogInDTO userLogInDTO)
        {

            var IsSuccess = await _ILogInService.Login(userLogInDTO);

            if (IsSuccess)
            {
                return RedirectToAction(nameof(Index));
            }

            else if (!IsSuccess)
            {
                TempData["Message"] = "Username is not Exist or password is Wrong";

            }
            return View();

        }

        #endregion



        //LogOut

        #region LogOut
        [Area("SitePanel")]
        public async Task<IActionResult> LogOut() 
        {
        
                await HttpContext.SignOutAsync();

            return RedirectToAction(nameof(Index));
        }

        #endregion
    }
}
