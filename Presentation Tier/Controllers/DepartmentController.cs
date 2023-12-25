using AutoMapper;
using Business_Logic_Tier.Interfaces;
using Data_Access_Tier.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation_Tier.Models;

namespace Presentation_Tier.Controllers
{
    [Authorize(Roles = "Admin,Departments Manager")]
    public class DepartmentController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DepartmentController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            IEnumerable<Department> departments = _unitOfWork.DepartmentRepository.GetAll();
            IEnumerable<DepartmentViewModel> mappedDepartments = _mapper.Map<IEnumerable<DepartmentViewModel>>(departments);
            return View(mappedDepartments);
        }

        [HttpGet]
        public IActionResult Create() 
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(DepartmentViewModel departmentVM)
        {
            if (ModelState.IsValid)
            {
                Department department = _mapper.Map<Department>(departmentVM);
                _unitOfWork.DepartmentRepository.Add(department);
                return RedirectToAction("Index");
            }
            return View(departmentVM);
        }

        public IActionResult Details (int? Id)
        {
            if (Id is null)
                return NotFound();

            var department = _unitOfWork.DepartmentRepository.GetById(Id);
            if (department is null)
                return NotFound();
            DepartmentViewModel departmentVM = _mapper.Map<DepartmentViewModel>(department);
            return View(departmentVM);
        }

        [HttpGet]
        public IActionResult Update(int? Id)
        {
            if (Id is null)
                return NotFound();

            var department = _unitOfWork.DepartmentRepository.GetById(Id);
            if (department is null)
                return NotFound();
            DepartmentViewModel departmentVM = _mapper.Map<DepartmentViewModel>(department);
            return View(departmentVM);
        }

        [HttpPost]
        public IActionResult Update(DepartmentViewModel departmentVM, int? Id)
        {
            if (Id != departmentVM.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                Department department = _mapper.Map<Department>(departmentVM);
                _unitOfWork.DepartmentRepository.Update(department);
                return RedirectToAction("Index");
            }
            return View(departmentVM);
        }

        public IActionResult Delete (int? Id)
        {
            if (Id is null)
                return NotFound();

            var department = _unitOfWork.DepartmentRepository.GetById(Id);
            if (department is null)
                return NotFound();

            _unitOfWork.DepartmentRepository.Delete(department);

            return RedirectToAction("Index");
        }
    }
}
