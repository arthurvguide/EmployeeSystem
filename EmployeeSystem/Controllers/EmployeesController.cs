using EmployeeSystem.Models;
using EmployeeSystem.Data;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeSystem.Controllers
{
    public class EmployeesController : Controller
    {
        public IActionResult Index()
        {
            List<EmployeeEntity> Employee = new List<EmployeeEntity>();

            EmployeeRepository EmployeeRepository = new EmployeeRepository();

            Employee = EmployeeRepository.RetrieveAllEmployee();

            return View(Employee);
        }

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
    }
}
