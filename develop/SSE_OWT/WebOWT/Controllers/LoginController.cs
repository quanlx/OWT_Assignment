using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WebOWT.Models;
using WebOWT.Models.EntityDataModels;
using WebOWT.Services;

namespace WebOWT.Controllers
{
    public class LoginController : Controller
    {
        private readonly IConfiguration _config;

        private readonly IUserService _userRepository;

        private readonly ITokenService _tokenService;

        private string generatedToken = null;

        public LoginController(IConfiguration config, ITokenService tokenService, IUserService userRepository)
        {
            _config = config;
            _tokenService = tokenService;
            _userRepository = userRepository;
        }

        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("Token") != null)
            {
                return RedirectToAction("MainWindow", "Home");
            }

            return View();
        }

        [AllowAnonymous]
        [Route("login")]
        [HttpPost]
        public IActionResult Login(UserModel userModel)
        {
            if (string.IsNullOrEmpty(userModel.UserName) || string.IsNullOrEmpty(userModel.Password))
            {
                return (RedirectToAction("Error"));
            }

            IActionResult response = Unauthorized();

            var validUser = GetUser(userModel);

            if (validUser != null)
            {
                HttpContext.Session.SetString("CurrentUser", validUser.UserName);

                switch (validUser.Role)
                {
                    case "normal":
                        HttpContext.Session.SetString("CheckRole", "normal");
                        break;

                    case "contributor":
                        HttpContext.Session.SetString("CheckRole", "contributor");
                        break;

                    case "admin":
                        HttpContext.Session.SetString("CheckRole", "admin");
                        break;

                    default:
                        break;
                }

                generatedToken = _tokenService.BuildToken(_config["Jwt:Key"].ToString(), _config["Jwt:Issuer"].ToString(), validUser);

                if (generatedToken != null)
                {
                    HttpContext.Session.SetString("Token", generatedToken);
                    return RedirectToAction("MainWindow", "Home");
                }
                else
                {
                    return (RedirectToAction("Error"));
                }
            }
            else
            {
                return (RedirectToAction("Home", "Error"));
            }
        }

        private User GetUser(UserModel userModel)
        {
            //Write your code here to authenticate the user
            return _userRepository.Login(userModel.UserName, userModel.Password);
        }
    }
}
