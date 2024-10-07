using EmployeeSystem.Models;
using EmployeeSystem.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EmployeeSystem.Controllers
{
    public class EmployeesController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            List<EmployeeEntity> Employee = new List<EmployeeEntity>();

            EmployeeRepository EmployeeRepository = new EmployeeRepository();

            Employee = EmployeeRepository.RetrieveAllEmployee();

            return View(Employee);
        }
        //NEEDS IMPROVEMENT FOR ERROR HANDLING ( MESSAGES FOR END USER )
        [HttpGet]
        public ActionResult AddEmployee()
        {
            // Retrieve all departments
            DepartmentRepository departmentRepository = new DepartmentRepository();
            List<DepartmentEntity> departments = departmentRepository.RetrieveAllDepartments();

            // Pass the department list to the view as a SelectList
            ViewBag.Departments = new SelectList(departments, "Id", "Name");

            return View();
        }


        [HttpPost]
        public ActionResult AddEmployee(EmployeeEntity EmployeeDetails)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    EmployeeRepository _DbEmployee = new EmployeeRepository();
                    if (_DbEmployee.AddEmployee(EmployeeDetails))
                    {
                        return RedirectToAction("Index");
                    }

                }
                // Retrieve department list again if ModelState is not valid
                DepartmentRepository departmentRepository = new DepartmentRepository();
                List<DepartmentEntity> departments = departmentRepository.RetrieveAllDepartments();
                ViewBag.Departments = new SelectList(departments, "Id", "Name");

                return View(EmployeeDetails);
            }
            catch
            {
                //FIX FIX
                return View(EmployeeDetails);
            }
        }

        [HttpGet]
        public IActionResult EditEmployee(int Id)
        {
            EmployeeEntity Employee = new EmployeeEntity();

            EmployeeRepository EmployeeRepository = new EmployeeRepository();
            DepartmentRepository DepartmentRepository = new DepartmentRepository(); // Retrieve departments

            Employee = EmployeeRepository.RetrieveEmployeeById(Id);

            // Retrieve the list of departments and pass them to the ViewBag
            var departments = DepartmentRepository.RetrieveAllDepartments();
            ViewBag.Departments = new SelectList(departments, "Id", "Name", Employee.DepartmentId);

            return View("EditEmployee", Employee);
        }

        //NEEDS IMPROVEMENT FOR ERROR HANDLING ( MESSAGES FOR END USER )
        //NEEDS REFACTORING BUT IT IS WORKING
        [HttpPost]
        public IActionResult EditEmployeeSave(int Id, EmployeeEntity EmployeeDetails)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    EmployeeRepository _DbEmployee = new EmployeeRepository();
                    if (_DbEmployee.EditEmployeeSave(Id, EmployeeDetails))
                    {
                        return RedirectToAction("Index");
                    }
                }
                // Retrieve department list if ModelState is not valid
                DepartmentRepository departmentRepository = new DepartmentRepository();
                List<DepartmentEntity> departments = departmentRepository.RetrieveAllDepartments();
                ViewBag.Departments = new SelectList(departments, "Id", "Name", EmployeeDetails.DepartmentId);
                return View("EditEmployee", EmployeeDetails);
            }
            catch
            {
                // Retrieve department list if ModelState is not valid
                DepartmentRepository departmentRepository = new DepartmentRepository();
                List<DepartmentEntity> departments = departmentRepository.RetrieveAllDepartments();
                ViewBag.Departments = new SelectList(departments, "Id", "Name", EmployeeDetails.DepartmentId);
                return View("EditEmployee", EmployeeDetails);
            }
        }

        [HttpGet]
        public ActionResult GetDeleteEmployee(int Id)
        {
            EmployeeEntity Employee = new EmployeeEntity();

            EmployeeRepository EmployeeRepository = new EmployeeRepository();

            Employee = EmployeeRepository.RetrieveEmployeeById(Id);

            return View("DeleteEmployee", Employee);

        }

        [HttpPost]
        public IActionResult DeleteEmployee(int Id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    EmployeeRepository _DbEmployee = new EmployeeRepository();
                    if (_DbEmployee.DeleteEmployee(Id))
                    {
                        return RedirectToAction("Index");
                    }
                }

                return View(GetDeleteEmployee);
            }
            catch
            {
                return View(GetDeleteEmployee);
            }
        }
    }
}
