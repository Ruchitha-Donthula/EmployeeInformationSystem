using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Http;
using EmployeeDataAccess;
using EmployeeLibrary;

namespace EmployeeServices.Controllers
{
    public class EmployeeWebServicesController : ApiController
    {
        public static string employeeDetailsFilePath = ConfigurationManager.AppSettings["employeeDetailsFilePath"];

        // GET: api/EmployeeWebServices/ReadEmployeeData
        [HttpGet]
        [Route("api/ReadEmployeeData")]
        public IHttpActionResult ReadEmployeeData()
        {
            AccessEmployeeData.ReadEmployeeData(employeeDetailsFilePath);
            List<Employee> employees = new Employee().GetAllEmployees();
            return Json(employees);
        }

        // GET: api/EmployeeWebServices/GetAllEmployees
        [HttpGet]
        [Route("api/GetAllEmployees")]
        public IHttpActionResult GetAllEmployees()
        {
            List<Employee> employees = new Employee().GetAllEmployees();
            return Ok(employees);
        }

        // GET: api/EmployeeWebServices/GetEmployeeById/{id}
        [HttpGet]
        [Route("api/GetEmployeeById")]
        public IHttpActionResult GetEmployeeById(int id)
        {
            Employee employee = new Employee().GetEmployee(id);
            return Json(employee);
        }

        // POST: api/EmployeeWebServices/AddEmployee
        [HttpPost]
        [Route("api/AddEmployee")]
        public IHttpActionResult AddEmployee(Employee employee)
        {
            new Employee().AddEmployee(employee.EmployeeID, employee.EmployeeName, employee.EmployeeSalary);
            return Ok("Employee added successfully.");
        }

        // POST: api/EmployeeWebServices/UpdateEmployee
        [HttpPost]
        [Route("api/UpdateEmployee")]
        public IHttpActionResult UpdateEmployee(Employee employee)
        {
            new Employee().UpdateEmployeeData(employee.EmployeeID, employee.EmployeeName, employee.EmployeeSalary);
            return Ok("Employee updated successfully.");
        }

        // POST: api/EmployeeWebServices/DeleteEmployee/{id}
        [HttpPost]
        [Route("api/DeleteEmployee/{id}")]
        public IHttpActionResult DeleteEmployee(int id)
        {
            new Employee().DeleteEmployee(id);
            return Ok("Employee deleted successfully.");
        }

        [HttpGet]
        [Route("api/SaveEmployeeData")]
        public IHttpActionResult SaveEmployeeData()
        {
            try
            {
                List<Employee> employees = new Employee().GetAllEmployees();
                SavingEmployeeData.SaveEmployeeData(employees, employeeDetailsFilePath);
                return Ok("Employee data saved successfully.");
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

    }
}
