using IKIA.BLL.DTOs.Employees;
using IKIA.BLL.Services.Employees;
using IKIA.DAL.Common.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IKIA.PL.Controllers
{
    [Authorize(AuthenticationSchemes = "Identity.Application")]
    public class EmployeeController : Controller
    {
        #region Services

        private readonly IEmployeeService _employeeService;
        private readonly ILogger<EmployeeController> _logger;
        private readonly IWebHostEnvironment _environment;

        public EmployeeController(IEmployeeService employeeService, 
            ILogger<EmployeeController> logger, 
            IWebHostEnvironment environment)
        {
            _employeeService = employeeService;
            _logger = logger;
            _environment = environment;
        }

        #endregion
        
        #region Index

        public async Task<IActionResult> Index(string search)
        {
            ViewBag.ActionName = "Delete";

            var Employees = await _employeeService.GetEmployeesAsync(search);
            return View(Employees);
        }

        #endregion

        #region Create

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateEmployeeDTO empDTO)
        {
            if (!ModelState.IsValid)
                return View(empDTO);

            var message = string.Empty;
            try
            {
                int result = await _employeeService.CreateEmployeeAsync(empDTO);
                if (result > 0)
                    return RedirectToAction(nameof(Index));
                else
                {
                    message = "Employee is Not Created !!";
                    ModelState.AddModelError(string.Empty, message);
                    return View(empDTO);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                if (_environment.IsDevelopment())
                    message = ex.Message;
                else message = "Employee is Not Created !!";
            }
            ModelState.AddModelError(string.Empty, message);
            return View("Error", message);
        }

        #endregion

        #region Details

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return BadRequest();
            var employee = await _employeeService.GetEmployeeByIdAsync(id.Value);
            if (employee == null)
                return NotFound();

            return View(employee);
        }

        #endregion

        #region Edit

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return BadRequest();
            var employee = await _employeeService.GetEmployeeByIdAsync(id.Value);
            if (employee == null)
                return NotFound();

            return View(new CreateEmployeeDTO()
            {
                Address = employee.Address,
                Age = employee.Age,
                Email = employee.Email, 
                EmployeeType = EmployeeType.FullTime,
                Gender = Gender.Male,
                HiringDate = employee.HiringDate,
                IsActive = employee.IsActive,
                Name = employee.Name,
                PhoneNumber = employee.PhoneNumber,
                Salary = employee.Salary,
                DepartmentId = employee.DepartmentId
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([FromRoute] int id , CreateEmployeeDTO empVM)
        {
            if (!ModelState.IsValid)
                return View(empVM);

            var message = string.Empty;
            try
            {
                int result = await _employeeService.UpdateEmployeeAsync(new UpdateEmployeeDTO()
                {
                    Id = id,
                    Salary = empVM.Salary,
                    Address = empVM.Address,
                    Email = empVM.Email,
                    PhoneNumber = empVM.PhoneNumber,
                    Name = empVM.Name,
                    IsActive = empVM.IsActive,
                    HiringDate = empVM.HiringDate,
                    Age = empVM.Age,
                    Gender = empVM.Gender,
                    EmployeeType = empVM.EmployeeType,
                    DepartmentId = empVM.DepartmentId,
                    Image = empVM.Image,
                });

                if (result > 0)
                    return RedirectToAction(nameof(Index));
                else
                {
                    message = "Employee is Not Updated !!";
                    ModelState.AddModelError(string.Empty, message);
                    return View(empVM);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                if (_environment.IsDevelopment())
                    message = ex.Message;
                else message = "Employee is Not Updated !!";
            }
            ModelState.AddModelError(string.Empty, message);
            return View("Error", message);
        }

        #endregion

        #region Delete

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return BadRequest();
            var employee =  await _employeeService.GetEmployeeByIdAsync(id.Value);
            if (employee == null)
                return NotFound();

            return View(employee);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var message = string.Empty;
            try
            {
                var result = await _employeeService.DeleteEmployeeAsync(id);

                if (result)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    message = "Employee is Not Deleted !!";
                    ModelState.AddModelError(string.Empty, message);
                    return View(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                if (_environment.IsDevelopment())
                    message = ex.Message;
                else message = "Employee is Not Deleted !!";
            }
            ModelState.AddModelError(string.Empty, message);
            return View("Error", message);
        }

        #endregion
    }
}
