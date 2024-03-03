using System;
using System.Collections.Generic;
using System.Linq;

namespace EmployeeLibrary
{
    public class Employee
    {
        public int EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public int EmployeeSalary { get; set; }

        public static List<Employee> employees = new List<Employee>();

        public void AddEmployee(int id, string name, int salary)
        {
            Employee employee = new Employee
            {
                EmployeeID = id,
                EmployeeName = name,
                EmployeeSalary = salary,
            };
            employees.Add(employee);
        }

        public void UpdateEmployeeData(int id, string name, int salary)
        {
            Employee employee = employees.FirstOrDefault(emp => emp.EmployeeID == id);
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


        public void DeleteEmployee(int id)
        {
            Employee employee = employees.FirstOrDefault(emp => emp.EmployeeID == id);
            if (employee != null)
            {
                employees.Remove(employee);
            }
        }

        public Employee GetEmployee(int id)
        {
            return employees.FirstOrDefault(emp => emp.EmployeeID == id);
        }

        public List<Employee> GetAllEmployees()
        {
            return employees;
        }
    }
}
