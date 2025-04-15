using dopeClothes.Server.IRepositories;
using dopeClothes.Server.Models;
using dopeClothes.Server.Models.VMs;
using dopeClothes.Server.Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace dopeClothes.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public AuthController(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            if (ModelState.IsValid)
            {
                if (await _userManager.FindByEmailAsync(registerVM.Email) != null)
                {
                    ModelState.AddModelError("Email", "User with this email already exists");
                    return BadRequest(ModelState);
                }
                ApplicationUser applicationUser = new ApplicationUser()
                {
                    UserName = registerVM.UserName,
                    Email = registerVM.Email,


                };
                var result = await _userManager.CreateAsync(applicationUser, registerVM.Password);
                if (result.Succeeded)
                {
                    if (!await _roleManager.RoleExistsAsync(SD.ROLE_USER))
                    {
                        await _roleManager.CreateAsync(new IdentityRole(SD.ROLE_USER));

                    }
                    await _userManager.AddToRoleAsync(applicationUser, SD.ROLE_USER);


                    await _signInManager.SignInAsync(applicationUser, isPersistent: false);
                    var role = _userManager.GetRolesAsync(applicationUser).Result.FirstOrDefault();
                    return Ok(new
                    {
                        user = new { applicationUser.UserName, applicationUser.Email, role },
                        message = "User registered successfully"
                    });
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    return BadRequest(ModelState);
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
        [HttpPost("Logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok(new { Message = "User logged out successfully" });
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            if (ModelState.IsValid)
            {
                var userFromDb = await _userManager.FindByEmailAsync(loginVM.Email);
                if (userFromDb == null)
                {
                    return BadRequest(ModelState);
                }
                var result = await _signInManager.PasswordSignInAsync(userFromDb.UserName, loginVM.Password, isPersistent: false, lockoutOnFailure: false);
                    if (result.Succeeded)
                {
                    var role =  _userManager.GetRolesAsync(userFromDb).Result.FirstOrDefault();
                    return Ok(new
                    {
                        user = new { userFromDb.UserName, userFromDb.Email,role },
                        message = "User logged in successfully"
                    });
                }
                else
                {
                    ModelState.AddModelError("", "Invalid login attempt");
                    return BadRequest(ModelState);
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
    }
}
