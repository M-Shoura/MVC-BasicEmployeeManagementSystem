using IKIA.BLL.DTOs.Employees;
using IKIA.DAL.Models.Identity;
using IKIA.PL.ViewModels.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Data;

namespace IKIA.PL.Controllers
{
    public class UserController : Controller
    {
        #region Services
        
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<UserController> _logger;
        private readonly IWebHostEnvironment _environment;

        public UserController(UserManager<ApplicationUser> userManager, ILogger<UserController> logger, IWebHostEnvironment environment)
        {
            _userManager = userManager;
            _logger = logger;
            _environment = environment;
        }

        #endregion

        #region Index

        [HttpGet]
        public async Task<IActionResult> Index(string search)
        {
            ViewBag.ActionName = "ConfirmDelete";

            if (search.IsNullOrEmpty())
            {
                List<UserViewModel> users = new List<UserViewModel>();
                var userList = await _userManager.Users.ToListAsync();

                foreach (var user in userList)
                {
                    var roles = await _userManager.GetRolesAsync(user);
                    users.Add(new UserViewModel
                    {
                        Id = user.Id,
                        Email = user.Email,
                        FName = user.FName,
                        LName = user.LName,
                        PhoneNumber = user.PhoneNumber,
                        Roles = string.Join(", ", roles)
                    });

                }
                return View(users);
            }
            else
            {
                List<UserViewModel> users = new List<UserViewModel>();
                var userList = await _userManager.Users.Where(x => x.NormalizedUserName.Contains(search.ToUpper())).ToListAsync();

                foreach (var user in userList)
                {
                    var roles = await _userManager.GetRolesAsync(user);
                    users.Add(new UserViewModel
                    {
                        Id = user.Id,
                        Email = user.Email,
                        FName = user.FName,
                        LName = user.LName,
                        PhoneNumber = user.PhoneNumber,
                        Roles = string.Join(", ", roles)
                    });

                }
                return View(users);

            }
        }

        #endregion

        #region Details

        [HttpGet]
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
                return BadRequest();
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return NotFound();

            var roles = await _userManager.GetRolesAsync(user);

            return View(new UserViewModel()
            {
                Id = id,
                Email = user.Email,
                FName = user.FName,
                LName = user.LName,
                PhoneNumber = user.PhoneNumber,
                Roles = string.Join(", ", roles)
            });
        }
        #endregion

        #region Edit

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
                return BadRequest();
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return NotFound();

            var roles = await _userManager.GetRolesAsync(user);

            return View(new UserViewModel()
            {
                Id = id,
                Email = user.Email,
                FName = user.FName,
                LName = user.LName,
                PhoneNumber = user.PhoneNumber,
                Roles = string.Join(", ", roles)
            });
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UserViewModel model, [FromRoute] string id)
        {
            if (model.Id != id)
                return BadRequest();

            if (!ModelState.IsValid)
                return View(model);

            var message = string.Empty;
            try
            {
                // When updating must be like this ... cannot update as we did in other models (Employee , department , ... )
                var user = await _userManager.FindByIdAsync(id);
                user.PhoneNumber = model.PhoneNumber;
                user.FName = model.FName;
                user.LName = model.LName;

                var result = await _userManager.UpdateAsync(user);

                if (result.Succeeded)
                    return RedirectToAction(nameof(Index));
                else
                {
                    message = "User is Not Updated !!";
                    ModelState.AddModelError(string.Empty, message);
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                if (_environment.IsDevelopment())
                    message = ex.Message;
                else message = "User is Not Updated !!";
            }
            ModelState.AddModelError(string.Empty, message);
            return View("Error", message);
        }

        #endregion

        #region Delete

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
                return BadRequest();
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return NotFound();

            var roles = await _userManager.GetRolesAsync(user);

            return View(new UserViewModel()
            {
                Id = id,
                Email = user.Email,
                FName = user.FName,
                LName = user.LName,
                PhoneNumber = user.PhoneNumber,
                Roles = string.Join(", ", roles)
            });
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmDelete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return NotFound();

            string message;
            try
            {
                var result = await _userManager.DeleteAsync(user);
                if (result.Succeeded)
                    return RedirectToAction(nameof(Index));
                else
                {
                    var roles = await _userManager.GetRolesAsync(user);
                    message = "User is Not Deleted !!";
                    ModelState.AddModelError(string.Empty, message);
                    return View(new UserViewModel()
                    {
                        Id = id,
                        Email = user.Email,
                        FName = user.FName,
                        LName = user.LName,
                        PhoneNumber = user.PhoneNumber,
                        Roles = string.Join(", ", roles)
                    });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                if (_environment.IsDevelopment())
                    message = ex.Message;
                else message = "User is Not Deleted !!";
            }
            ModelState.AddModelError(string.Empty, message);
            return View("Error", message);
        } 

        #endregion
    }
}
