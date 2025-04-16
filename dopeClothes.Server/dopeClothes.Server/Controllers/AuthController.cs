using dopeClothes.Server.IRepositories;
using dopeClothes.Server.Models;
using dopeClothes.Server.Models.VMs;
using dopeClothes.Server.Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
                if ((await _userManager.FindByEmailAsync("admin@gmail.com") == null))
                {
                    ApplicationUser admin = new ApplicationUser()
                    {
                        UserName = "Admin",
                        Email = "admin@gmail.com",


                    };
                    await _userManager.CreateAsync(admin, "Heslo_123");
                    await _roleManager.CreateAsync(new IdentityRole(SD.ROLE_ADMIN));
                    await _userManager.AddToRoleAsync(admin, SD.ROLE_ADMIN);
                }
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
        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword(ResetPasswordVM resetPasswordVM)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = "Insert valid passwords!" });
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return NotFound(new { message = "User not found." });
            }

           
            var isOldPasswordValid = await _userManager.CheckPasswordAsync(user, resetPasswordVM.OldPassword);
            if (!isOldPasswordValid)
            {
                return BadRequest(new { message = "Old password is incorrect." });
            }

            
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, token, resetPasswordVM.NewPassword);

            if (result.Succeeded)
            {
                return Ok(new { message = "Password reset successfully!" });
            }
            else
            {
                return BadRequest(new { message = "Unable to reset password.", errors = result.Errors });
            }
        }
    }
}
