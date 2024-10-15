using EmployeeSystem.Models;
using EmployeeSystem.Data;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeSystem.Controllers
{
    public class DepartmentController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            List<DepartmentEntity> Department = new List<DepartmentEntity>();

            DepartmentRepository DepartmentRepository = new DepartmentRepository();

            Department = DepartmentRepository.RetrieveAllDepartments();

            return View(Department);
        }


    }
}
