using EmployeeSystem.Models;
using EmployeeSystem.Data;
using Microsoft.AspNetCore.Mvc;

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
                //FIX FIX 
                return View();
            }
            catch
            {
                //FIX FIX
                return View();
            }
        }

        [HttpGet]
        public IActionResult EditEmployee(int Id)
        {
            EmployeeEntity Employee = new EmployeeEntity();

            EmployeeRepository EmployeeRepository = new EmployeeRepository();

            Employee = EmployeeRepository.RetrieveEmployeeById(Id);

            return View("EditEmployee", Employee);
        }

        //NEEDS IMPROVEMENT FOR ERROR HANDLING ( MESSAGES FOR END USER )
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
                return View("EditEmployee", EmployeeDetails);
            }
            catch
            {
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
