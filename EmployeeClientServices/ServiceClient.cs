using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic;
using EmployeeLibrary;

namespace EmployeeServiceClient
{
    public class ServiceClient
    {
        /// <summary>
        /// Adds an employee by sending an HTTP POST request to the server.
        /// </summary>
        /// <param name="employee">The employee to be added.</param>
        /// <returns>True if the operation was successful; otherwise, false.</returns>
        public async Task<bool> AddEmployee(Employee employee)
        {
            try
            {
                // Define the endpoint URI for adding an employee
                string endUri = "/api/AddEmployee";

                // Delegate the actual HTTP request to ServiceClientHelper
                return await new ServiceClientHelper().AddEmployee(employee, endUri);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
                throw;
            }
        }


        /// <summary>
        /// Retrieves a list of all employees from the server.
        /// </summary>
        /// <returns>A list of all employees.</returns>
        public async Task<List<Employee>> GetAllEmployees()
        {
            try
            {
                // Define the endpoint URI for adding an employee
                string endUri = "/api/GetAllEmployees";
               List<Employee> EmployeesList= await new ServiceClientHelper().GetAllEmployees(endUri);
                return EmployeesList;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Saves employee data to the server.
        /// </summary>
        /// <returns>True if the operation was successful; otherwise, false.</returns>
        public async Task<bool> SaveEmployees()
        {
            try
            {
                string endUri = "/api/SaveEmployees";
                // Delegate the actual HTTP request to ServiceClientHelper
                return await new ServiceClientHelper().SaveEmployees(endUri);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Reads employee data from the server.
        /// </summary>
        /// <returns>True if the operation was successful; otherwise, false.</returns>
        public async Task<bool> ReadEmployees()
        {
            try
            {
                string endUri = "/api/ReadEmployees";
                // Delegate the actual HTTP request to ServiceClientHelper
                return await new ServiceClientHelper().ReadEmployees(endUri);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Updates an existing employee on the server.
        /// </summary>
        /// <param name="employee">The updated employee object.</param>
        /// <returns>True if the operation was successful; otherwise, false.</returns>
        public async Task<bool> UpdateEmployee(Employee employee)
        {
            try
            {
                // Define the endpoint URI for updating an employee
                string endUri = "/api/UpdateEmployee";

                // Delegate the actual HTTP request to ServiceClientHelper
                return await new ServiceClientHelper().UpdateEmployee(employee, endUri);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Retrieves an employee from the server by their ID.
        /// </summary>
        /// <param name="id">The ID of the employee to retrieve.</param>
        /// <returns>The employee object if found; otherwise, null.</returns>
        public async Task<Employee> GetEmployeeById(int id)
        {
            try
            {
                // Define the endpoint URI for getting an employee by ID GetEmployeeById
                string endUri = $"/api/GetEmployeeById?id={id}";

                // Delegate the actual HTTP request to ServiceClientHelper
                return await new ServiceClientHelper().GetEmployeeById(id,endUri);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Deletes an employee from the server by their ID.
        /// </summary>
        /// <param name="id">The ID of the employee to delete.</param>
        /// <returns>True if the operation was successful; otherwise, false.</returns>
        public async Task<bool> DeleteEmployee(int id)
        {
            try
            {
                // Define the endpoint URI for deleting an employee
                string endUri = $"/api/DeleteEmployee?id={id}";

                // Delegate the actual HTTP request to ServiceClientHelper
                return await new ServiceClientHelper().DeleteEmployee(endUri);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
                throw;
            }
        }


    }
}
