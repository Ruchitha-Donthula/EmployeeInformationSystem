using System;
using System.Collections.Generic;
using System.Linq;
using EmployeeObjects;
using EmployeeDataAccess;

namespace EmployeeBusinessLogic
{
    public class EmployeeBusiness
    {
        public void AddEmployee(int id, string name, int salary)
        {
            Employee employee = new Employee
            {
                EmployeeID = id,
                EmployeeName = name,
                EmployeeSalary = salary,
            };
            EmployeesList.Employees.Add(employee);
        }

        public void UpdateEmployeeData(int id, string name, int salary)
        {
            Employee employee = EmployeesList.Employees.FirstOrDefault(emp => emp.EmployeeID == id);
            try
            {
                if (employee != null)
                {
                    employee.EmployeeName = name;
                    employee.EmployeeSalary = salary;
                }
                else
                {
                    throw new Exception();
                }
            }
            catch
            {
                throw;
            }
        }

        public void ReadEmployees(string filepath)
        {
            try
            {
                AccessEmployees.ReadEmployees(filepath);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while reading employees from file.", ex);
            }
        }

        public void SaveEmployees(List<Employee> employees,string filepath)
        {
            try
            {
                SavingEmployees.SaveEmployees(employees, filepath);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while saving employees to file.", ex);
            }
        }
        public void DeleteEmployee(int id)
        {
            Employee employee = EmployeesList.Employees.FirstOrDefault(emp => emp.EmployeeID == id);
            if (employee != null)
            {
                EmployeesList.Employees.Remove(employee);
            }
        }

        public Employee GetEmployee(int id)
        {
            return EmployeesList.Employees.FirstOrDefault(emp => emp.EmployeeID == id);
        }

        public List<Employee> GetAllEmployees()
        {
            return EmployeesList.Employees;
        }
    }
}
