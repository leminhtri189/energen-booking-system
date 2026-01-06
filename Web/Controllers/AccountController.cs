using BusinessLogicLayer.Services.Interface;
using BusinessObject.Enums;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Web.Models.Account;

namespace Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _service;

        public AccountController(IUserService service)
        {
            _service = service;
        }

        public IActionResult Register()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> Profile()
        {
            string user_id = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "Id")!.Value;
            var user = await _service.GetUserWithId(Guid.Parse(user_id));

            AccountUpdateViewModel model = new AccountUpdateViewModel
            {
                Id = user.Id,
                Username = user.UseName,
                Email = user.Email,
                Fullname = user.FullName,
                PhoneNumber = user.Phone,
                Gender = user.Gender,
                Role = user.Role,
            };

            return View(model);
        }

        [Authorize]
        public async Task<IActionResult> Update(AccountUpdateViewModel info)
        {
            string user_id = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "Id")!.Value;
            var result = await _service.UpdateUserProfile(Guid.Parse(user_id), 
                email: info.Email, 
                username: info.Username, 
                fullname: info.Fullname,
                phone: info.PhoneNumber,
                gender: info.Gender);

            if (result.IsFailed)
            {
                ViewData["Error"] = result;
                return RedirectToAction(nameof(Profile));
            }

            return RedirectToAction(nameof(Profile));
        }

        [HttpPost]
        public async Task<IActionResult> Register(AccountRegistrationViewModel registration)
        {
            if (!ModelState.IsValid)
            {
                return View(registration);
            }

            var result = await _service.CreateUserAccount(registration.Email, registration.Password, registration.Fullname, registration.Phone,registration.Gender, Role.Custommer);
            
            if (result.IsFailed)
            {
                TempData["ErrorMessage"] = $"Account registration failed: {result.Error.Description}";
                return View(registration);
            }

            return Redirect("/");
        }

        [HttpPost]
        public async Task<IActionResult> Login(AccountLoginViewModel loginInfo,  string? returnUrl )
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Invalid input. Please check your email and password.";
                return View(loginInfo);
            }

            var user = await _service.GetUserWithEmail(loginInfo.Email, loginInfo.Password);
            if (user == null)
            {
                TempData["ErrorMessage"] = "Incorrect email or password.";
                return View(loginInfo);
            }

            IList<Claim> claims = new List<Claim>
    {
        new Claim("Id", user.Id.ToString()),
        new Claim(ClaimTypes.Email, user.Email),
       new Claim("Avatar", user.Avatar),
        new Claim(ClaimTypes.Role, ((int) user.Role).ToString()),
        new Claim(ClaimTypes.Name, user.FullName)
    };

            ClaimsIdentity identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            ClaimsPrincipal principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
            if (user.Role == Role.Admin)
            {
                return RedirectToAction("Index", "Dashboard");
            }
            if (user.Role == Role.Staff || user.Role == Role.Therapist )
            {
                return RedirectToAction("ManageBooking", "Bookings");
            }
            if(user.Role == Role.Manager)
            {
                return RedirectToAction("IndexDaboard", "Services");
            }
            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return Redirect("/");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }


    }
}
