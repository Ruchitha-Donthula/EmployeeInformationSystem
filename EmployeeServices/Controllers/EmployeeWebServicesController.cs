using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Http;
using EmployeeDataAccess;
using EmployeeLibrary;
using EmployeeServices.Filters;

namespace EmployeeServices.Controllers
{
    public class EmployeeWebServicesController : ApiController
    {
        private static string employeeDetailsFilePath = ConfigurationManager.AppSettings["employeeDetailsFilePath"];

        // Add action filter to log requests and responses
        [HttpGet]
        [Route("api/ReadEmployeeData")]
        [RequestResponseLoggingFilter]
        [ExceptionHandlingFilter] 
        public IHttpActionResult ReadEmployeeData()
        {
            try
            {
                AccessEmployeeData.ReadEmployeeData(employeeDetailsFilePath);
                List<Employee> employees = new Employee().GetAllEmployees();
                return Json(employees);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        [Route("api/GetAllEmployees")]
        [RequestResponseLoggingFilter]
        public IHttpActionResult GetAllEmployees()
        {
            List<Employee> employees = new Employee().GetAllEmployees();
            return Ok(employees);
        }

        [HttpGet]
        [Route("api/GetEmployeeById")]
        [RequestResponseLoggingFilter]
        public IHttpActionResult GetEmployeeById(int id)
        {
            Employee employee = new Employee().GetEmployee(id);
            return Json(employee);
        }

        [HttpPost]
        [Route("api/AddEmployee")]
        [RequestResponseLoggingFilter]
        public IHttpActionResult AddEmployee(Employee employee)
        {
            new Employee().AddEmployee(employee.EmployeeID, employee.EmployeeName, employee.EmployeeSalary);
            return Ok("Employee added successfully.");
        }

        [HttpPost]
        [Route("api/UpdateEmployee")]
        [RequestResponseLoggingFilter]
        public IHttpActionResult UpdateEmployee(Employee employee)
        {
            new Employee().UpdateEmployeeData(employee.EmployeeID, employee.EmployeeName, employee.EmployeeSalary);
            return Ok("Employee updated successfully.");
        }

        [HttpPost]
        [Route("api/DeleteEmployee/{id}")]
        [RequestResponseLoggingFilter]
        public IHttpActionResult DeleteEmployee(int id)
        {
            new Employee().DeleteEmployee(id);
            return Ok("Employee deleted successfully.");
        }

        [HttpGet]
        [Route("api/SaveEmployeeData")]
        [RequestResponseLoggingFilter]
        [ExceptionHandlingFilter]
        public IHttpActionResult SaveEmployeeData()
        {
            try
            {
                List<Employee> employees = new Employee().GetAllEmployees();
                SavingEmployeeData.SaveEmployeeData(employees, employeeDetailsFilePath);
                return Ok("Employee data saved successfully.");
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
