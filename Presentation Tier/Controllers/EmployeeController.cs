using AutoMapper;
using Business_Logic_Tier.Interfaces;
using Data_Access_Tier.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation_Tier.Helper;
using Presentation_Tier.Models;

namespace Presentation_Tier.Controllers
{
	[Authorize(Roles = "Admin,Employees Manager")]
	public class EmployeeController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;

		public EmployeeController(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public IActionResult Index(string SearchValue = "", int? departmentId = null)
		{
			IEnumerable<Employee> employees;

			if (!string.IsNullOrEmpty(SearchValue))
				employees = _unitOfWork.EmployeeRepository.Search(SearchValue);

			else if (departmentId is not null)
				employees = _unitOfWork.EmployeeRepository.GetEmployeesByDepartmentID(departmentId);
			
			else
				employees = _unitOfWork.EmployeeRepository.GetAll();

			var mappedEmployees = _mapper.Map<IEnumerable<EmployeeViewModel>>(employees);

			return View(mappedEmployees);
		}

		[HttpGet]
		public IActionResult Create()
		{
			ViewBag.Department = _unitOfWork.DepartmentRepository.GetAll();
			return View();
		}

		[HttpPost]
		public IActionResult Create(EmployeeViewModel employeeVM)
		{
			if (ModelState.IsValid)
			{
				if (employeeVM.Image is not null)
                    employeeVM.ImageURL = DocumentSettings.UploadFile(employeeVM.Image, "Images");
				else
				{
                    employeeVM.Image = ImageReader.ReadImage("Files\\Images\\EmployeesDefaultImage.jpg");
                    employeeVM.ImageURL = DocumentSettings.UploadFile(employeeVM.Image, "Images");
                }

                Employee employee = _mapper.Map<Employee>(employeeVM);
				_unitOfWork.EmployeeRepository.Add(employee);
				return RedirectToAction("Index");
			}
			ViewBag.Department = _unitOfWork.DepartmentRepository.GetAll();

			return View(employeeVM);
		}

		[HttpGet]
		public IActionResult Update(int? Id)
		{
			if (Id is null)
				return NotFound();

			var employee = _unitOfWork.EmployeeRepository.GetById(Id);

			if (employee is null)
				return NotFound();

			var employeeVM = _mapper.Map<EmployeeViewModel>(employee);

			ViewBag.Department = _unitOfWork.DepartmentRepository.GetAll();

			return View(employeeVM);
		}

		[HttpPost]
		public IActionResult Update(EmployeeViewModel employeeVM, int? Id)
		{
			if (Id != employeeVM.Id)
				return NotFound();

			if (ModelState.IsValid)
			{
                if (employeeVM.Image != null)
                {
                    if (!string.IsNullOrEmpty(employeeVM.ImageURL))
                        DocumentSettings.DeleteFile(Path.Combine("Images", employeeVM.ImageURL));

                    employeeVM.ImageURL = DocumentSettings.UploadFile(employeeVM.Image, "Images");
                }

                Employee mappedEmployee = _mapper.Map<Employee>(employeeVM);

				_unitOfWork.EmployeeRepository.Update(mappedEmployee);

				return RedirectToAction("Index");
			}
			ViewBag.Department = _unitOfWork.DepartmentRepository.GetAll();

			return View(employeeVM);
		}

		public IActionResult Delete(int? Id)
		{
			if (Id is null)
				return NotFound();

			var employee = _unitOfWork.EmployeeRepository.GetById(Id);

			if (employee is null)
				return NotFound();

			if (!string.IsNullOrEmpty(employee.ImageURL))
                DocumentSettings.DeleteFile(Path.Combine("Images", employee.ImageURL));

            _unitOfWork.EmployeeRepository.Delete(employee);

			return RedirectToAction("Index");
		}

		public IActionResult Details(int? Id)
		{
			if (Id is null)
				return NotFound();

			var employee = _unitOfWork.EmployeeRepository.GetById(Id);
			if (employee is null)
				return NotFound();
			EmployeeViewModel employeeVM = _mapper.Map<EmployeeViewModel>(employee);
			return View(employeeVM);
		}
	}
}