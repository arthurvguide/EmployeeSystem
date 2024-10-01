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
    }
}
