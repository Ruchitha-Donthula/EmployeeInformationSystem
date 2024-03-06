using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using EmployeeLibrary;
using EmployeeServiceClient;
using ClientWebServices.Models;

namespace ClientWebServices.Controllers
{
    /// <summary>
    /// Controller for managing employee-related actions.
    /// </summary>
    public class EmployeeController : Controller
    {
        /// <summary>
        /// Displays the default view for employee management.
        /// </summary>
        /// <returns>The default view.</returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Displays the form for adding a new employee.
        /// </summary>
        /// <returns>The view containing the employee addition form.</returns>
        public ActionResult AddEmployeeForm()
        {
            var employee = new Models.Employee();
            return View(employee);
        }

        /// <summary>
        /// Displays the form for updating an existing employee.
        /// </summary>
        /// <returns>The view containing the employee update form.</returns>
        public ActionResult UpdateEmployeeForm()
        {
            var employee = new Models.Employee();
            return View(employee);
        }

        /// <summary>
        /// Displays the form for taking input id to display EmployeeById.
        /// </summary>
        /// <returns>The view containing the employee Details.</returns>
        public ActionResult InputGetEmployeeId()
        {
            var employee = new Models.Employee();
            return View(employee);
        }

        /// <summary>
        /// Displays the form for taking input id to delete the employee.
        /// </summary>
        /// <returns>The view containing input taking Form.</returns>
        public ActionResult InputDeleteEmployeeId()
        {
            var employee = new Models.Employee();
            return View(employee);
        }

        /// <summary>
        /// Adds a new employee.
        /// </summary>
        /// <param name="employee">The employee data to add.</param>
        /// <returns>A view indicating the success or failure of the operation.</returns>
        [HttpPost]
        public async Task<ActionResult> AddEmployee(Models.Employee employee)
        {
            if (ModelState.IsValid)
            {
                EmployeeLibrary.Employee employeeModel = EmployeeHelper.GetEmployeeModelFromViewModel(employee);
                bool isSuccess = await new ServiceClient().AddEmployee(employeeModel);

                if (isSuccess)
                {
                    return Content("Employee added successfully");
                }
                else
                {
                    return View("AddEmployeeForm", employee);
                }
            }

            return View("AddEmployeeForm", employee);
        }

        /// <summary>
        /// Retrieves all employees.
        /// </summary>
        /// <returns>A view containing the list of all employees.</returns>
        [HttpGet]
        public async Task<ActionResult> GetAllEmployees()
        {
            try
            {
                List<EmployeeLibrary.Employee> employees = await new ServiceClient().GetAllEmployees();
                List<Models.Employee> ViewEmployeesList = new List<Models.Employee>();

                foreach (var employee in employees)
                {
                    var modelEmployee = EmployeeHelper.GetEmployeeViewModelFromModel(employee);
                    ViewEmployeesList.Add(modelEmployee);
                }
                return View("GetAllEmployees", ViewEmployeesList);
            }
            catch (Exception ex)
            {
                return Content($"An unexpected error occurred: {ex.Message}");
            }
        }

        /// <summary>
        /// Saves employee data.
        /// </summary>
        /// <returns>A view containing a message indicating the success of the operation.</returns>
        [HttpGet]
        public async Task<ActionResult> SaveEmployees()
        {
            try
            {
                ServiceClient client = new ServiceClient();
                bool isSuccess = await client.SaveEmployees();

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
                return Content($"<div style=\"text-align:center;\">An error occurred while saving employee data: {ex.Message}</div>");
            }
        }

        /// <summary>
        /// Reads employee data from the server.
        /// </summary>
        /// <returns>A view containing a message indicating the success of the operation.</returns>
        [HttpGet]
        public async Task<ActionResult> ReadEmployees()
        {
            try
            {
                ServiceClient client = new ServiceClient();
                bool isSuccess = await client.ReadEmployees();

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
                return Content($"<div style=\"text-align:center;\">An error occurred while reading employee data: {ex.Message}</div>");
            }
        }

        /// <summary>
        /// Updates an existing employee.
        /// </summary>
        /// <param name="employee">The updated employee data.</param>
        /// <returns>A view indicating the success or failure of the operation.</returns>
        [HttpPost]
        public async Task<ActionResult> UpdateEmployee(Models.Employee employee)
        {
            if (ModelState.IsValid)
            {
                EmployeeLibrary.Employee employeeModel = EmployeeHelper.GetEmployeeModelFromViewModel(employee);

                try
                {
                    bool isSuccess = await new ServiceClient().UpdateEmployee(employeeModel);

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
                    return Content($"An error occurred while updating employee: {ex.Message}");
                }
            }

            return View("UpdateEmployeeForm", employee);
        }

        /// <summary>
        /// Retrieves an employee by their ID.
        /// </summary>
        /// <param name="id">The ID of the employee to retrieve.</param>
        /// <returns>A view containing the details of the specified employee.</returns>
        [HttpGet]
        public async Task<ActionResult> GetEmployeeById(int id)
        {
            try
            {
                EmployeeLibrary.Employee libraryEmployee = await new ServiceClient().GetEmployeeById(id);

                if (libraryEmployee != null)
                {
                    var modelEmployee = EmployeeHelper.GetEmployeeViewModelFromModel(libraryEmployee);
                    return View("GetEmployeeById", modelEmployee);
                }
                else
                {
                    return Content("Employee not found");
                }
            }
            catch (Exception ex)
            {
                return Content($"An error occurred while getting employee data: {ex.Message}");
            }
        }

        /// <summary>
        /// Deletes an employee by their ID.
        /// </summary>
        /// <param name="id">The ID of the employee to delete.</param>
        /// <returns>A view indicating the success or failure of the operation.</returns>
        [HttpGet]
        public async Task<ActionResult> DeleteEmployee(int id)
        {
            try
            {
                bool isSuccess = await new ServiceClient().DeleteEmployee(id);

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
                return Content($"An error occurred while deleting employee: {ex.Message}");
            }
        }
    }
}
