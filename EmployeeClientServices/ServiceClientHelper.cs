using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using EmployeeObjects;
using System.Collections.Generic;
using System.Configuration;

namespace EmployeeServiceClient
{
    /// <summary>
    /// Helper class to handle HTTP requests to the employee service.
    /// </summary>
    class ServiceClientHelper
    {
        private readonly HttpClient _httpClient;
        private readonly string baseAddress = ConfigurationManager.AppSettings["baseAddress"];

        /// <summary>
        /// Constructor to initialize HttpClient with base address and accept header.
        /// </summary>
        public ServiceClientHelper()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(baseAddress)
            };
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        /// <summary>
        /// Adds an employee by sending an HTTP POST request to the server.
        /// </summary>
        /// <param name="employee">The employee to be added.</param>
        /// <param name="endpoint">The endpoint URI for adding an employee.</param>
        /// <returns>True if the operation was successful; otherwise, false.</returns>
        public async Task<bool> AddEmployee(Employee employee, string endpoint)
        {
            try
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(employee));
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                HttpResponseMessage response = await _httpClient.PostAsync(endpoint, content);
                response.EnsureSuccessStatusCode();
                return response.IsSuccessStatusCode;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Error adding employee: {ex.Message}");
                return false;
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
        /// <param name="endpoint">The endpoint URI for retrieving all employees.</param>
        /// <returns>A list of all employees.</returns>
        public async Task<List<Employee>> GetAllEmployees(string endpoint)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(endpoint);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                List<Employee> employees = JsonConvert.DeserializeObject<List<Employee>>(responseBody);

                return employees;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Error reading employee data: {ex.Message}");
                throw;
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
        /// <param name="endpoint">The endpoint URI for saving employee data.</param>
        /// <returns>True if the operation was successful; otherwise, false.</returns>
        public async Task<bool> SaveEmployees(string endpoint)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(endpoint);
                response.EnsureSuccessStatusCode();
                return true;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Error saving employee data: {ex.Message}");
                return false;
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
        /// <param name="endpoint">The endpoint URI for reading employee data.</param>
        /// <returns>True if the operation was successful; otherwise, false.</returns>
        public async Task<bool> ReadEmployees(string endpoint)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(endpoint);
                response.EnsureSuccessStatusCode();
                return true;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Error reading employee data: {ex.Message}");
                return false;
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
        /// <param name="endUri">The endpoint URI for updating an employee.</param>
        /// <returns>True if the operation was successful; otherwise, false.</returns>
        public async Task<bool> UpdateEmployee(Employee employee, string endUri)
        {
            try
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(employee));
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                HttpResponseMessage response = await _httpClient.PostAsync(endUri, content);
                response.EnsureSuccessStatusCode();
                return response.IsSuccessStatusCode;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Error updating employee data: {ex.Message}");
                return false;
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
        /// <param name="endpoint">The endpoint URI for getting an employee by ID.</param>
        /// <returns>The employee object if found; otherwise, null.</returns>
        public async Task<Employee> GetEmployeeById(int id, string endpoint)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(endpoint);
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
                Employee employee = JsonConvert.DeserializeObject<Employee>(responseBody);

                return employee;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Error getting employee by ID: {ex.Message}");
                throw;
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
        /// <param name="endpoint">The endpoint URI for deleting an employee.</param>
        /// <returns>True if the operation was successful; otherwise, false.</returns>
        public async Task<bool> DeleteEmployee(string endpoint)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(endpoint);
                response.EnsureSuccessStatusCode();
                return response.IsSuccessStatusCode;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Error deleting employee: {ex.Message}");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
                throw;
            }
        }
    }
}
