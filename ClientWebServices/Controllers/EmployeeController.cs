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
                ServiceClient client = new ServiceClient("http://localhost:44393/");
                bool isSuccess = await client.AddEmployee(employeeModel);

                if (isSuccess)
                {
                    return Content("Employee added successfully");
                }
                else
                {
                    return View("AddEmployee", employee);
                }
            }

            return View("AddEmployee", employee);
        }

        /// <summary>
        /// Retrieves all employees.
        /// </summary>
        /// <returns>A view containing the list of all employees.</returns>
        [HttpGet]
        public async Task<ActionResult> GetAllEmployees()
        {
            ServiceClient client = new ServiceClient("http://localhost:44393/");

            try
            {
                List<EmployeeLibrary.Employee> libraryEmployees = await client.GetAllEmployees();
                List<Models.Employee> employees = new List<Models.Employee>();
                foreach (var libraryEmployee in libraryEmployees)
                {
                    var modelEmployee = EmployeeHelper.GetEmployeeViewModelFromModel(libraryEmployee);
                    employees.Add(modelEmployee);
                }

                return View("GetAllEmployees", employees);
            }
            catch (Exception ex)
            {
                return Content($"An error occurred while reading employee data: {ex.Message}");
            }
        }

        /// <summary>
        /// Retrieves an employee by their ID.
        /// </summary>
        /// <param name="id">The ID of the employee to retrieve.</param>
        /// <returns>A view containing the details of the specified employee.</returns>
        [HttpGet]
        public async Task<ActionResult> GetEmployeeById(int id)
        {
            ServiceClient client = new ServiceClient("http://localhost:44393/");

            try
            {
                EmployeeLibrary.Employee libraryEmployee = await client.GetEmployeeById(id);

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
        /// Saves employee data.
        /// </summary>
        /// <returns>A view containing a message indicating the success of the operation.</returns>
        [HttpGet]
        public async Task<ActionResult> SaveEmployeeData()
        {
            ServiceClient client = new ServiceClient("http://localhost:44393/");

            try
            {
                bool isSuccess = await client.SaveEmployeeData();

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
                ServiceClient client = new ServiceClient("http://localhost:44393/");
                bool isSuccess = await client.UpdateEmployee(employeeModel);

                if (isSuccess)
                {
                    return Content("Employee updated successfully");
                }
                else
                {
                    return Content("Failed to update employee");
                }
            }
            return View("UpdateEmployee", employee);
        }

        /// <summary>
        /// Deletes an employee.
        /// </summary>
        /// <param name="id">The ID of the employee to delete.</param>
        /// <returns>A view indicating the success or failure of the operation.</returns>
        [HttpGet]
        public async Task<ActionResult> DeleteEmployee(int id)
        {
            try
            {
                ServiceClient client = new ServiceClient("http://localhost:44393/");
                bool isSuccess = await client.DeleteEmployee(id);

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
        /// <summary>
        /// Reads employee data.
        /// </summary>
        /// <returns>A view containing a message indicating the success of the operation.</returns>
        [HttpGet]
        public async Task<ActionResult> ReadEmployeeData()
        {
            ServiceClient client = new ServiceClient("http://localhost:44393/");

            try
            {
                await client.ReadEmployeeData();
                Console.WriteLine("Employee data read successfully");
                return Content("Employee data read successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while reading employee data: {ex.Message}");
                return Content($"An error occurred while reading employee data: {ex.Message}");
            }
        }
    }
}
