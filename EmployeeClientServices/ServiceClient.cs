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
        private readonly HttpClient _httpClient;

        /// <summary>
        /// Initializes a new instance of the ServiceClient class with the specified base address.
        /// </summary>
        /// <param name="baseAddress">The base address of the service.</param>
        public ServiceClient(string baseAddress)
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
        /// <returns>True if the operation was successful; otherwise, false.</returns>
        public async Task<bool> AddEmployee(Employee employee)
        {
            try
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(employee));
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                HttpResponseMessage response = await _httpClient.PostAsync("api/AddEmployee", content);
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
        /// Saves employee data by sending an HTTP GET request to the server.
        /// </summary>
        /// <returns>True if the operation was successful; otherwise, false.</returns>
        public async Task<bool> SaveEmployeeData()
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync("api/SaveEmployeeData");
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
        /// Retrieves all employees by sending an HTTP GET request to the server.
        /// </summary>
        /// <returns>A list of Employee objects if successful; otherwise, throws an exception.</returns>
        public async Task<List<Employee>> GetAllEmployees()
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync("api/GetAllEmployees");
                response.EnsureSuccessStatusCode(); 

                // Deserialize the response content to a list of Employee objects
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
        /// Retrieves an employee by ID by sending an HTTP GET request to the server.
        /// </summary>
        /// <param name="id">The ID of the employee to retrieve.</param>
        /// <returns>The Employee object if found; otherwise, throws an exception.</returns>
        public async Task<Employee> GetEmployeeById(int id)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"api/GetEmployeeById?id={id}");
                response.EnsureSuccessStatusCode(); 

                // Deserialize the response content to an Employee object
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
        /// Reads employee data from the server.
        /// </summary>
        public async Task ReadEmployeeData()
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync("api/ReadEmployeeData");
                response.EnsureSuccessStatusCode(); 

                // Print a success message indicating that the data was read successfully
                Console.WriteLine("Employee data read successfully");
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
        /// Updates an employee by sending an HTTP POST request to the server.
        /// </summary>
        /// <param name="employee">The employee object with updated information.</param>
        /// <returns>True if the operation was successful; otherwise, false.</returns>
        public async Task<bool> UpdateEmployee(Employee employee)
        {
            try
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(employee));
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                HttpResponseMessage response = await _httpClient.PostAsync("api/UpdateEmployee", content);
                response.EnsureSuccessStatusCode();

                return response.IsSuccessStatusCode;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Error updating employee: {ex.Message}");
                return false; 
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
                throw; 
            }
        }

        /// <summary>
        /// Deletes an employee by sending an HTTP GET request to the server.
        /// </summary>
        /// <param name="id">The ID of the employee to delete.</param>
        /// <returns>True if the operation was successful; otherwise, false.</returns>
        public async Task<bool> DeleteEmployee(int id)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"api/DeleteEmployee?id={id}");
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
