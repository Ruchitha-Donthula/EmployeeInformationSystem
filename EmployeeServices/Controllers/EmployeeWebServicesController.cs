using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Http;
using EmployeeObjects;
using EmployeeServices.Filters;
using EmployeeBusinessLogic; 

namespace EmployeeServices.Controllers
{
    public class EmployeeWebServicesController : ApiController
    {
        private static string employeeDetailsFilePath = ConfigurationManager.AppSettings["employeeDetailsFilePath"];

        // Add action filter to log requests and responses
        [HttpGet]
        [Route("api/ReadEmployees")]
        [RequestResponseLoggingFilter]
        [ExceptionHandlingFilter]
        public IHttpActionResult ReadEmployees()
        {
            try
            {
                new EmployeeBusiness().ReadEmployees(employeeDetailsFilePath);
                return Ok();
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
            List<Employee> employees = new EmployeeBusiness().GetAllEmployees();
            return Ok(employees);
        }

        [HttpGet]
        [Route("api/GetEmployeeById")]
        [RequestResponseLoggingFilter]
        public IHttpActionResult GetEmployeeById(int id)
        {
            Employee employee = new EmployeeBusiness().GetEmployee(id);
            return Json(employee);
        }

        [HttpPost]
        [Route("api/AddEmployee")]
        [RequestResponseLoggingFilter]
        public IHttpActionResult AddEmployee(Employee employee)
        {
            new EmployeeBusiness().AddEmployee(employee.EmployeeID, employee.EmployeeName, employee.EmployeeSalary);
            return Ok(employee);
        }

        [HttpPost]
        [Route("api/UpdateEmployee")]
        [RequestResponseLoggingFilter]
        public IHttpActionResult UpdateEmployee(Employee employee)
        {
            new EmployeeBusiness().UpdateEmployeeData(employee.EmployeeID, employee.EmployeeName, employee.EmployeeSalary);
            return Ok("Employee updated successfully.");
        }


        [HttpGet]
        [Route("api/DeleteEmployee")]
        [RequestResponseLoggingFilter]
        public IHttpActionResult DeleteEmployee(int id)
        {
            new EmployeeBusiness().DeleteEmployee(id);
            return Ok("Employee deleted successfully.");
        }

        [HttpGet]
        [Route("api/SaveEmployees")]
        [RequestResponseLoggingFilter]
        [ExceptionHandlingFilter]
        public IHttpActionResult SaveEmployees()
        {
            try
            {
                List<Employee> employees = new EmployeeBusiness().GetAllEmployees();
                new EmployeeBusiness().SaveEmployees(employees, employeeDetailsFilePath);
                return Ok();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
