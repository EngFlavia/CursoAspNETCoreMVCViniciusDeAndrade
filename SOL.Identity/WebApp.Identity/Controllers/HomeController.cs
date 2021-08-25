

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApp.Identity.Models;

namespace WebApp.Identity.Controllers
{
    //[ApiController] //Substitui o ModelState.IsValid
    public class HomeController : Controller
    {
        //private readonly UserManager<MyUser> _userManager;
        private readonly UserManager<IdentityUser> _userManager;

        //public HomeController(UserManager<MyUser> userManager)
        //{
        //    _userManager = userManager;
        //}

        public HomeController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }
        //private readonly ILogger<HomeController> _logger;

        //public HomeController(ILogger<HomeController> logger)
        //{

        //    //_logger = logger;
        //}

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.UserName);

                if (user == null)
                {
                    user = new IdentityUser()
                    {
                        Id = Guid.NewGuid().ToString(),
                        UserName = model.UserName
                    };

                    var result = await _userManager.CreateAsync(
                        user, model.Password);

                    //if(result.Errors != null) 
                    //{
                    //    return View();
                    //}

                }
                              
                return View("Sucess");
            }

            return View();

        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.UserName);

                if(user != null && await _userManager.CheckPasswordAsync(
                    user, model.Password))
                {
                    var identity = new ClaimsIdentity("cookies");
                    identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id));
                    identity.AddClaim(new Claim(ClaimTypes.Name, user.UserName));

                    await HttpContext.SignInAsync("cookies", new ClaimsPrincipal(identity));

                    return RedirectToAction("about");
                }
                ModelState.AddModelError("", "Usuário ou Senha Invalida");
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Login()
        {
            return View();
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Register()
        {
            return View(); 
        }

        [HttpGet]
        public IActionResult About()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Sucess()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
