using Microsoft.AspNetCore.Mvc;

namespace Avion.Areas.Admin.Controllers
{
    public class AccountController : MainController
    {
        rivate readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(UserManager<AppUser> userManager,
                                 SignInManager<AppUser> signInManager,
                                 RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }
        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.ToListAsync();


            List<UserVM> userVM = new();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);

                userVM.Add(new UserVM
                {
                    Id = user.Id,
                    FullName = user.FullName,
                    UserName = user.UserName,
                    Email = user.Email,
                    RoleName = roles,
                    EmailConfirm = user.EmailConfirmed
                });
            }

            return View(userVM);
        }

        [HttpGet]
        public async Task<IActionResult> AddRoleToUser()
        {
            ViewBag.roles = await GetRolesAsync();
            ViewBag.users = await GetUsersAsync();
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddRoleToUser(UserRoleVM request)
        {
            AppUser user = await _userManager.FindByIdAsync(request.UserId);
            IdentityRole role = await _roleManager.FindByIdAsync(request.RoleId);

            await _userManager.AddToRoleAsync(user, role.Name);

            return RedirectToAction(nameof(Index));
        }

        private async Task<SelectList> GetRolesAsync()
        {
            List<IdentityRole> roles = await _roleManager.Roles.ToListAsync();

            return new SelectList(roles, "Id", "Name");
        }

        private async Task<SelectList> GetUsersAsync()
        {
            var users = await _userManager.Users.ToListAsync();

            return new SelectList(users, "Id", "UserName");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {id} cannot be found";
                return View("NotFound");
            }
            else
            {
                var result = await _userManager.DeleteAsync(user);

                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return RedirectToAction(nameof(Index));
            }
        }
    }
}
