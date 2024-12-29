using AutoMapper;
using IKIA.BLL.DTOs.Departments;
using IKIA.BLL.Services.Departments;
using IKIA.PL.ViewModels.Departments;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IKIA.PL.Controllers
{
    [Authorize(AuthenticationSchemes = "Identity.Application")]
    public class DepartmentController : Controller
    {
        #region Services

        private readonly IDepartmentService _departmentService;
        private readonly ILogger<DepartmentController> _logger;
        private readonly IWebHostEnvironment _environment;
        private readonly IMapper _mapper;

        public DepartmentController(IDepartmentService departmentService,
                                    ILogger<DepartmentController> logger,
                                    IWebHostEnvironment environment,
                                    IMapper mapper)
        {
            _departmentService = departmentService;
            _logger = logger;
            _environment = environment;
            _mapper = mapper;
        }

        #endregion

        #region Index

        [HttpGet] 
        public async Task<IActionResult> Index()
        {

            ViewData["Message"] = "Hello from ViewData";
            ViewBag.Message = "Hello from ViewBag";

            ViewBag.ActionName = "Delete";


            var departments = await _departmentService.GetAllDepartmentsAsync();
            return View(departments);
        }

        #endregion

        #region Details

        [HttpGet]    
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var department = await _departmentService.GetDepartmentByIdAsync(id.Value);
            if (department == null)
            {
                return NotFound();
            }
            return View(department);
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
        public async Task<IActionResult> Create(DepartmentViewModel DeptVM)
        {
           
            if (!ModelState.IsValid)           
            {                                  
                return View(DeptVM);
            }

            var message = string.Empty;

            try
            {
                var dto = _mapper.Map<CreatedDepartmentDTO>(DeptVM);

                var result = await _departmentService.CreateDepartmentAsync(dto);

                if (result > 0)
                {
                    TempData["Toast"] = "Department Created Successfully";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["Toast"] = "Department Creation Failed";
                    message = "Department is Not Created !!";
                    ModelState.AddModelError(string.Empty, message);
                    return View(DeptVM);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                if (_environment.IsDevelopment())
                {
                    message = ex.Message;
                    return View(DeptVM);
                }
                else
                {
                    message = "Department is Not Created";
                    return View("Error", message);
                }
            }
        }

        #endregion

        #region Edit

        [HttpGet]   
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return BadRequest();
            var department = await _departmentService.GetDepartmentByIdAsync(id.Value);
            if (department == null)
                return NotFound();

            var result = _mapper.Map<DepartmentViewModel>(department);

            return View(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] int id, DepartmentViewModel deptVM)
        {
           
            if (!ModelState.IsValid)
                return View(deptVM);

            var message = string.Empty;

            try
            {
                var dto = _mapper.Map<UpdatedDepartmentDTO>(deptVM);

                var result = await _departmentService.UpdateDepartmentAsync(dto);

                if (result > 0)
                    return RedirectToAction(nameof(Index));
                else
                    message = "An error has occured during updating the Department !!";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
  
                message = _environment.IsDevelopment() ? ex.Message : "An error has occured during updating the Department !!";
            }
            ModelState.AddModelError(string.Empty, message);
            return View(deptVM);
        }

        #endregion

        #region Delete

        [HttpGet]  
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return BadRequest();
            var department = await _departmentService.GetDepartmentByIdAsync(id.Value);
            if (department == null)
                return NotFound();

            return View(department);
        }

        [HttpPost]    
        public async Task<IActionResult> Delete(int id)
        {
            var message = string.Empty;
            try
            {
                var result = await _departmentService.DeleteDepartmentAsync(id);
                if (result)
                    return RedirectToAction(nameof(Index));

                message = "An error has occured during updating the Department !!";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                message = _environment.IsDevelopment() ? ex.Message : "Error when deleting the department";
            }
            ModelState.AddModelError(string.Empty, message);
            return View("Error", message);
        }

        #endregion
    }
}
