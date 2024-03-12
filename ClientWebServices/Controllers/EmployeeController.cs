using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using EmployeeObjects;
using EmployeeServiceClient;
using ClientWebServices.Models;

namespace ClientWebServices.Controllers
{
    public class EmployeeController : Controller
    {
        private static readonly ServiceClient serviceClient = new ServiceClient();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddEmployeeForm()
        {
            var employee = new ViewModelEmployee();
            return View(employee);
        }

        public async Task<ActionResult> UpdateEmployeeForm(int id)
        {
            try
            {
                EmployeeObjects.Employee Employee = await serviceClient.GetEmployeeById(id);
                if (Employee != null)
                {
                    var modelEmployee = EmployeeHelper.GetEmployeeViewModelFromModel(Employee);
                    return View(modelEmployee);
                }
                else
                {
                    return Content("Employee not found");
                }
               
            }
            catch (Exception ex)
            {
                return Content($"An error occurred while retrieving the employee: {ex.Message}");
            }
        }

        public ActionResult GetEmployeeByJquery()
        {
            return View();
        }
        public ActionResult InputGetEmployeeId()
        {
            var employee = new ViewModelEmployee();
            return View(employee);
        }

        public ActionResult InputDeleteEmployeeId()
        {
            var employee = new ViewModelEmployee();
            return View(employee);
        }

        [HttpPost]
        public async Task<ActionResult> AddEmployee(ViewModelEmployee employee)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var employeeModel = EmployeeHelper.GetEmployeeModelFromViewModel(employee);
                    bool isSuccess = await serviceClient.AddEmployee(employeeModel);

                    if (isSuccess)
                    {
                        return Content("Employee added successfully");
                    }
                    else
                    {
                        return View("AddEmployeeForm", employee);
                    }
                }
                catch (Exception ex)
                {
                    return Content($"An error occurred while adding the employee: {ex.Message}");
                }
            }

            return View("AddEmployeeForm", employee);
        }

        [HttpGet]
        public async Task<ActionResult> GetAllEmployees()
        {
            try
            {
                List<EmployeeObjects.Employee> employees = await serviceClient.GetAllEmployees();
                List<ViewModelEmployee> viewEmployeesList = new List<ViewModelEmployee>();

                foreach (var employee in employees)
                {
                    var modelEmployee = EmployeeHelper.GetEmployeeViewModelFromModel(employee);
                    viewEmployeesList.Add(modelEmployee);
                }
                return View("GetAllEmployees", viewEmployeesList);
            }
            catch (Exception ex)
            {
                return Content($"An unexpected error occurred while retrieving all employees: {ex.Message}");
            }
        }

        [HttpGet]
        public async Task<ActionResult> SaveEmployees()
        {
            try
            {
                bool isSuccess = await serviceClient.SaveEmployees();

                if (isSuccess)
                {
                    return Content("<div style=\"text-align:center;\">Employee data saved successfully</div>");
                }
                else
                {
                    return Content("<div style=\"text-align:center;\">Failed to save employee data</div>");
                }
            }
            catch (Exception ex)
            {
                return Content($"An error occurred while saving employee data: {ex.Message}");
            }
        }

        [HttpGet]
        public async Task<ActionResult> ReadEmployees()
        {
            try
            {
                bool isSuccess = await serviceClient.ReadEmployees();

                if (isSuccess)
                {
                    return Content("<div style=\"text-align:center;\">Employee data read successfully</div>");
                }
                else
                {
                    return Content("<div style=\"text-align:center;\">Failed to read employee data</div>");
                }
            }
            catch (Exception ex)
            {
                return Content($"An error occurred while reading employee data: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult> UpdateEmployee(ViewModelEmployee employee)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var employeeModel = EmployeeHelper.GetEmployeeModelFromViewModel(employee);
                    bool isSuccess = await serviceClient.UpdateEmployee(employeeModel);

                    if (isSuccess)
                    {
                        return Content("Employee updated successfully");
                    }
                    else
                    {
                        return Content("Failed to update employee");
                    }
                }
                catch (Exception ex)
                {
                    return Content($"An error occurred while updating the employee: {ex.Message}");
                }
            }

            return View("UpdateEmployeeForm", employee);
        }

        [HttpGet]
        public async Task<ActionResult> GetEmployeeById(int id)
        {
            try
            {
                EmployeeObjects.Employee Employee = await serviceClient.GetEmployeeById(id);

                if (Employee != null)
                {
                    var modelEmployee = EmployeeHelper.GetEmployeeViewModelFromModel(Employee);
                    return View("GetEmployeeById", modelEmployee);
                }
                else
                {
                    return Content("Employee not found");
                }
            }
            catch (Exception ex)
            {
                return Content($"An error occurred while retrieving the employee: {ex.Message}");
            }
        }

        [HttpGet]
        public async Task<JsonResult> GetEmployeeByIdUsingJQuery(int id)
        {
            try
            {
                EmployeeObjects.Employee Employee = await serviceClient.GetEmployeeById(id);

                if (Employee != null)
                {
                    var modelEmployee = EmployeeHelper.GetEmployeeViewModelFromModel(Employee);
                    return Json(modelEmployee, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { error = "Employee not found" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { error = $"An error occurred while retrieving the employee: {ex.Message}" }, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpGet]
        public async Task<ActionResult> DeleteEmployee(int id)
        {
            try
            {
                bool isSuccess = await serviceClient.DeleteEmployee(id);

                if (isSuccess)
                {
                    return Content("Employee deleted successfully");
                }
                else
                {
                    return Content("Failed to delete employee");
                }
            }
            catch (Exception ex)
            {
                return Content($"An error occurred while deleting the employee: {ex.Message}");
            }
        }
    }
}
