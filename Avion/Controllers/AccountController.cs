using Avion.Areas.Admin.ViewModels.Account;
using Avion.Data;
using Avion.Helpers.Enums;
using Avion.Models;
using Avion.Services.Interfaces;
using Avion.ViewModels.Basket;
using Avion.ViewModels.Wishlist;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Avion.Controllers
{
    //mvlr lqzh qrnp wcmt
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IBasketService _basketService;
        private readonly IWishlistService _wishlistService;
        private readonly AppDbContext _context;
        private readonly IEmailService _emailService;

        public AccountController(UserManager<AppUser> userManager,
                                 SignInManager<AppUser> signInManager,
                                 RoleManager<IdentityRole> roleManager,
                                 IBasketService basketService,
                                 IWishlistService wishlistService,
                                 AppDbContext context,
                                 IEmailService emailService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _basketService = basketService;
            _wishlistService = wishlistService;
            _context = context;
            _emailService = emailService;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterVM request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }

            //var existUser = await _userManager.FindByEmailAsync(request.Email);

            //if(existUser is not null)
            //{
            //    ModelState.AddModelError(string.Empty, "This email address already use");
            //    return View(request);
            //}

            AppUser user = new()
            {
                FullName = request.FullName,
                Email = request.Email,
                UserName = request.UserName,
            };


            IdentityResult result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, item.Description);
                }

                return View(request);
            }

            var createdUser = await _userManager.FindByNameAsync(user.UserName);

            await _userManager.AddToRoleAsync(createdUser, Roles.Member.ToString());

            //emailconfirm


            string token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            var url = Url.Action(nameof(ConfirmEmail), "Account", new { userId = user.Id, token }, Request.Scheme, Request.Host.ToString());

            string subject = "Welcome to Avion";
            string emailHtml = string.Empty;

            using (StreamReader reader = new("wwwroot/templates/register-confirm.html"))
            {
                emailHtml = reader.ReadToEnd();
            }

            emailHtml = emailHtml.Replace("{{link}}", url);
            emailHtml = emailHtml.Replace("{{fullName}}", user.FullName);

            _emailService.Send(user.Email, subject, emailHtml);

            return RedirectToAction(nameof(VerifyEmail));
        }
        public IActionResult VerifyEmail()
        {
            return View();  
        }
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (userId is null || token is null) return RedirectToAction("Index", "Error"); ;

            AppUser user = await _userManager.FindByIdAsync(userId);

            if (user is null) return RedirectToAction("Index", "Error"); ;

            await _userManager.ConfirmEmailAsync(user, token);

            await _signInManager.SignInAsync(user, false);

            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVM request)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            AppUser dbUser = await _userManager.FindByEmailAsync(request.EmailOrUsername);

            if (dbUser is null)
            {
                dbUser = await _userManager.FindByNameAsync(request.EmailOrUsername);
            }

            if (dbUser is null)
            {
                ModelState.AddModelError(string.Empty, "Login informations is wrong");
                return View();
            }

            var result = await _signInManager.PasswordSignInAsync(dbUser, request.Password, false, false);

            if (!result.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "Login informations is wrong");
                return View();
            }

            List<WishlistVM> wishlist = new();
            Wishlist dbWishlist = await _wishlistService.GetByUserIdAsync(dbUser.Id);

            List<BasketVM> basket = new();
            Basket dbBasket = await _basketService.GetByUserIdAsync(dbUser.Id);

            if (dbBasket is not null)
            {
                List<BasketProduct> basketProducts = await _basketService.GetAllByBasketIdAsync(dbBasket.Id);

                foreach (var item in basketProducts)
                {
                    basket.Add(new BasketVM
                    {
                        Id = item.ProductId,
                        Count = item.Count
                    });
                }

                Response.Cookies.Append("basket", JsonConvert.SerializeObject(basket));

            }

            if (dbWishlist is not null)
            {
                List<WishlistProduct> wishlistProducts = await _wishlistService.GetAllByWishlistIdAsync(dbWishlist.Id);

                foreach (var item in wishlistProducts)
                {
                    wishlist.Add(new WishlistVM
                    {
                        Id = item.ProductId
                    });
                }

                Response.Cookies.Append("wishlist", JsonConvert.SerializeObject(wishlist));

            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout(string userId)
        {
            await _signInManager.SignOutAsync();
            List<WishlistVM> wishlist = _wishlistService.GetDatasFromCoockies();
            Wishlist dbWishlist = await _wishlistService.GetByUserIdAsync(userId);

            List<BasketVM> basket = _basketService.GetDatasFromCoockies();
            Basket dbBasket = await _basketService.GetByUserIdAsync(userId);

            if (basket.Count != 0)
            {
                if (dbBasket == null)
                {
                    dbBasket = new()
                    {
                        AppUserId = userId,
                        BasketProducts = new List<BasketProduct>()
                    };

                    foreach (var item in basket)
                    {
                        dbBasket.BasketProducts.Add(new BasketProduct()
                        {
                            ProductId = item.Id,
                            BasketId = dbBasket.Id,
                            Count = item.Count
                        });
                    }
                    await _context.Baskets.AddAsync(dbBasket);
                    await _context.SaveChangesAsync();

                }
                else
                {
                    List<BasketProduct> basketProducts = new();

                    foreach (var item in basket)
                    {
                        basketProducts.Add(new BasketProduct()
                        {
                            ProductId = item.Id,
                            BasketId = dbBasket.Id,
                            Count = item.Count
                        });
                    }

                    dbBasket.BasketProducts = basketProducts;
                    _context.SaveChanges();
                }

                Response.Cookies.Delete("basket");
            }
            else
            {
                if (dbBasket is not null)
                {
                    _context.Baskets.Remove(dbBasket);
                    _context.SaveChanges();
                }

            }



            if (wishlist.Count != 0)
            {
                if (dbWishlist == null)
                {
                    dbWishlist = new()
                    {
                        AppUserId = userId,
                        WishlistProducts = new List<WishlistProduct>()

                    };

                    foreach (var item in wishlist)
                    {
                        dbWishlist.WishlistProducts.Add(new WishlistProduct()
                        {
                            ProductId = item.Id,
                            WishlistId = dbWishlist.Id
                        });
                    }

                    await _context.Wishlists.AddAsync(dbWishlist);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    List<WishlistProduct> wishlistProducts = new();

                    foreach (var item in wishlist)
                    {
                        wishlistProducts.Add(new WishlistProduct()
                        {
                            ProductId = item.Id,
                            WishlistId = dbWishlist.Id
                        });
                    }

                    dbWishlist.WishlistProducts = wishlistProducts;

                    _context.SaveChanges();

                }

                Response.Cookies.Delete("wishlist");
            }
            else
            {
                if (dbWishlist is not null)
                {
                    _context.Wishlists.Remove(dbWishlist);
                    _context.SaveChanges();
                }

            }

            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordVM model)
        {

            if (!ModelState.IsValid)
            {
                return View();
            }

            AppUser existUser = await _userManager.FindByEmailAsync(model.Email);

            if (existUser is null || !existUser.EmailConfirmed)
            {
                ModelState.AddModelError("Email", "User is not found.");

                return View();
            }

            TempData["Email"] = existUser.Email;

            string token = await _userManager.GeneratePasswordResetTokenAsync(existUser);
            string link = Url.Action(nameof(ResetPassword), "Account", new { userId = existUser.Id, token }, Request.Scheme, Request.Host.ToString());
            string subject = "Reset Password";
            string html;

            using (StreamReader reader = new StreamReader("wwwroot/templates/reset-password.html"))
            {
                html = reader.ReadToEnd();
            }

            string fullName = existUser.FullName;

            html = html.Replace("{{fullName}}", fullName);
            html = html.Replace("{{link}}", link);

            _emailService.Send(existUser.Email, subject, html);

            return RedirectToAction(nameof(VerifyResetPassword));
        }
        [HttpGet]
        public IActionResult VerifyResetPassword()
        {
            return View();
        }
        [HttpGet]
        public IActionResult ResetPassword(string userId, string token)
        {
            return View(new ResetPasswordVM { Token = token, UserId = userId });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordVM resetPassword)
        {

            if (!ModelState.IsValid) return View(resetPassword);
            AppUser existUser = await _userManager.FindByIdAsync(resetPassword.UserId);
            if (existUser == null) return RedirectToAction("Index", "Error");

            if (await _userManager.CheckPasswordAsync(existUser, resetPassword.Password))
            {
                ModelState.AddModelError("", "New password cant be same with old password");
                return View(resetPassword);
            }
            await _userManager.ResetPasswordAsync(existUser, resetPassword.Token, resetPassword.Password);
            return RedirectToAction(nameof(Login));

        }
        //[HttpGet]
        //public async Task<IActionResult> CreateRoles()
        //{
        //    foreach (var role in Enum.GetValues(typeof(Roles)))
        //    {
        //        if (!await _roleManager.RoleExistsAsync(role.ToString()))
        //        {
        //            await _roleManager.CreateAsync(new IdentityRole { Name = role.ToString() });
        //        }
        //    }
        //    return Ok();
        //}
    }

}
