using EmployeeLibrary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Configuration;
using EmployeeDataAccess;

namespace EmployeeSystem
{
    public class EmployeeSystem
    {
        static void Main(string[] args)
        {
            string employeedetailsfile = ConfigurationManager.AppSettings["employeedetailsfile"];

            Console.WriteLine("Choose one from below options 1 to 8");
            Console.WriteLine("1.Add Employee\n2.Update Employee\n3.Delete Employee\n4.Get Employee\n5.Get All Employees\n6.Read Employee data\n7.Save Employee Data\n8.Exit");

            int option;
            while (!int.TryParse(Console.ReadLine(), out option) || option < 1 || option > 8)
            {
                Console.WriteLine("Invalid input. Please choose a number between 1 and 8.");
            }

            while (true)
            {
                switch (option)
                {
                    case 1:
                        Console.WriteLine("Enter employee details: ");
                        Console.WriteLine("Enter employee ID: ");
                        int id;
                        while (!int.TryParse(Console.ReadLine(), out id))
                        {
                            Console.WriteLine("Invalid input. Please enter a valid integer for employee ID.");
                        }
                        Console.WriteLine("Enter employee Name: ");
                        string name = Console.ReadLine();
                        Console.WriteLine("Enter employee Salary: ");
                        int salary;
                        while (!int.TryParse(Console.ReadLine(), out salary))
                        {
                            Console.WriteLine("Invalid input. Please enter a valid integer for employee Salary.");
                        }

                        try
                        {
                            Employee.AddEmployee(id, name, salary);
                            Console.WriteLine("Employee added successfully.");
                        }
                        catch (FormatException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;
                    case 2:
                        Console.WriteLine("Enter ID of employee to update: ");
                        while (!int.TryParse(Console.ReadLine(), out id))
                        {
                            Console.WriteLine("Invalid input. Please enter a valid integer for employee ID.");
                        }
                        Console.WriteLine("Enter updated Name of employee: ");
                        name = Console.ReadLine();
                        Console.WriteLine("Enter updated Salary of employee: ");
                        while (!int.TryParse(Console.ReadLine(), out salary))
                        {
                            Console.WriteLine("Invalid input. Please enter a valid integer for employee Salary.");
                        }

                        try
                        {
                            Employee.UpdateEmployeeData(id, name, salary);
                            Console.WriteLine("Employee updated successfully.");
                        }
                        catch (FormatException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;
                    case 3:
                        Console.WriteLine("Enter ID of employee to be deleted: ");
                        while (!int.TryParse(Console.ReadLine(), out id))
                        {
                            Console.WriteLine("Invalid input. Please enter a valid integer for employee ID.");
                        }

                        Employee.DeleteEmployee(id);
                        Console.WriteLine("Employee deleted successfully.");
                        break;
                    case 4:
                        Console.WriteLine("Enter ID of employee: ");
                        while (!int.TryParse(Console.ReadLine(), out id))
                        {
                            Console.WriteLine("Invalid input. Please enter a valid integer for employee ID.");
                        }

                        try
                        {
                            Employee emp = new Employee().GetEmployee(id);
                            if (emp != null)
                            {
                                Console.WriteLine(emp.EmployeeID + "," + emp.EmployeeName + "," + emp.EmployeeSalary);
                            }
                            else
                            {
                                Console.WriteLine("Employee not found.");
                            }
                        }
                        catch (NullReferenceException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;
                    case 5:
                        try
                        {
                            List<Employee> employees = new Employee().GetAllEmployees();
                            foreach (Employee emp in employees)
                            {
                                Console.WriteLine(emp.EmployeeID + "," + emp.EmployeeName + "," + emp.EmployeeSalary);
                            }
                        }
                        catch (NullReferenceException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;
                    case 6:
                        try
                        {
                            AccessEmployeeData.ReadEmployeeData(employeedetailsfile);
                            Console.WriteLine("Employee data read successfully.");
                        }
                        catch (FileNotFoundException ex)
                        {
                            Console.WriteLine($"{ex.Message}");
                        }
                        catch (ConfigurationErrorsException ex)
                        {
                            Console.WriteLine($"{ex.Message}");
                        }
                        break;
                    case 7:
                        try
                        {
                            List<Employee> employees = new Employee().GetAllEmployees();
                            SavingEmployeeData.SaveEmployeeData(employees,employeedetailsfile);
                            Console.WriteLine("Employee data saved successfully.");
                        }
                        catch (ConfigurationErrorsException ex)
                        {
                            Console.WriteLine($"{ex.Message}");
                        }
                        break;
                    case 8:
                        Environment.Exit(0);
                        break;
                }

                Console.WriteLine("\nChoose one from below options 1 to 8");
                Console.WriteLine("1.Add Employee\n2.Update Employee\n3.Delete Employee\n4.Get Employee\n5.Get All Employees\n6.Read Employee data\n7.Save Employee Data\n8.Exit");

                while (!int.TryParse(Console.ReadLine(), out option) || option < 1 || option > 8)
                {
                    Console.WriteLine("Invalid input. Please choose a number between 1 and 8.");
                }
            }
        }
    }
}
