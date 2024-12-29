using IKIA.PL.ViewModels.Roles;
using IKIA.PL.ViewModels.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IKIA.PL.Controllers
{
    public class RoleController : Controller
    {
        #region Services

        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<RoleController> _logger;
        private readonly IWebHostEnvironment _environment;

        public RoleController(RoleManager<IdentityRole> roleManager, ILogger<RoleController> logger, IWebHostEnvironment environment)
        {
            _roleManager = roleManager;
            _logger = logger;
            _environment = environment;
        } 

        #endregion

        #region Index

        [HttpGet]
        public async Task<IActionResult> Index(string search)
        {
            ViewBag.ActionName = "ConfirmDelete";

            if (search == null)
            {
                var roles = await _roleManager.Roles.Select(x => new RoleViewModel()
                {
                    Id = x.Id,
                    Name = x.Name
                }).ToListAsync();

                return View(roles);
            }
            else
            {
                var roles = await _roleManager.Roles
                    .Where(x => x.NormalizedName.Contains(search.ToUpper()))
                    .Select(x => new RoleViewModel()
                    {
                        Id = x.Id,
                        Name = x.Name
                    }).ToListAsync();

                return View(roles);
            }

        } 

        #endregion

        #region Create

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        public async Task<IActionResult> Create(RoleCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var role = new IdentityRole()
                    {
                        Name = model.Name
                    };
                    var result = await _roleManager.CreateAsync(role);
                    if (result.Succeeded)
                        return RedirectToAction(nameof(Index));
                    ModelState.AddModelError("", "role not added !");
                }
                catch (Exception ex)
                {
                    if (_environment.IsDevelopment())
                        ModelState.AddModelError("", ex.Message);
                    else
                        ModelState.AddModelError("", "role not added !");
                }
            }
            return View(model);
        }

        #endregion

        #region Details

        [HttpGet]
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
                return BadRequest();
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
                return NotFound();

            return View(new RoleViewModel()
            {
                Id = id,
                Name = role.Name,
            });
        }
        #endregion

        #region Edit

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
                return BadRequest();
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
                return NotFound();

            return View(new RoleViewModel()
            {
                Id = id,
                Name = role.Name,
            });
        }

        [HttpPost]
        public async Task<IActionResult> Edit(RoleViewModel model, [FromRoute] string id)
        {
            if (model.Id != id)
                return BadRequest();

            if (!ModelState.IsValid)
                return View(model);

            var message = string.Empty;
            try
            {
                // When updating must be like this ... cannot update as we did in other models (Employee , department , ... )
                var role = await _roleManager.FindByIdAsync(id);
                
                role.Name = model.Name;
               
                var result = await _roleManager.UpdateAsync(role);

                if (result.Succeeded)
                    return RedirectToAction(nameof(Index));
                else
                {
                    message = "Role is Not Updated !!";
                    ModelState.AddModelError(string.Empty, message);
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                if (_environment.IsDevelopment())
                    message = ex.Message;
                else message = "Role is Not Updated !!";
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
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
                return NotFound();

            return View(new RoleViewModel()
            {
                Id = id,
                Name = role.Name,
            });
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmDelete(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
                return NotFound();

            string message;
            try
            {
                var result = await _roleManager.DeleteAsync(role);
                if (result.Succeeded)
                    return RedirectToAction(nameof(Index));
                else
                {
                    message = "Role is Not Deleted !!";
                    ModelState.AddModelError(string.Empty, message);
                    return View(new RoleViewModel()
                    {
                        Id = id,
                        Name = role.Name,
                    });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                if (_environment.IsDevelopment())
                    message = ex.Message;
                else message = "Role is Not Deleted !!";
            }
            ModelState.AddModelError(string.Empty, message);
            return View("Error", message);
        }

        #endregion
    }
}
