using ClientWebServices.Models;
using System.Web.Mvc;

namespace ClientWebServices.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        public ActionResult Index()
        {
            return View();
        }

        // GET: Employee/AddEmployee
        public ActionResult AddEmployee()
        {
            var employee = new Employee();
            return View(employee);
        }

        // POST: Employee/SubmitEmployee
        [HttpPost]
        public ActionResult SubmitEmployee(Employee employee)
        {
            if (ModelState.IsValid)
            {
                //return Content("Employee added successfully!");
                return View("AddEmployee", employee);
            }
            else
            {
                return View("AddEmployee", employee);
            }
        }
    }
}
