using EmployeeDataAccess;
using EmployeeLibrary;
using log4net;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;

namespace EmployeeSystem
{
    public class EmployeeSystem
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(EmployeeSystem));

        static void Main(string[] args)
        {
            // Configure log4net
            log4net.Config.XmlConfigurator.Configure();

            string employeedetailsfile = ConfigurationManager.AppSettings["employeedetailsfile"];

            log.Info("Employee System started.");

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
                            new Employee().AddEmployee(id, name, salary);
                            Console.WriteLine("Employee added successfully.");
                            log.Info($"Employee added: ID - {id}, Name - {name}, Salary - {salary}");
                        }
                        catch (FormatException ex)
                        {
                            Console.WriteLine(ex.Message);
                            log.Error("Error adding employee.", ex);
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
                            new Employee().UpdateEmployeeData(id, name, salary);
                            Console.WriteLine("Employee updated successfully.");
                            log.Info($"Employee updated: ID - {id}, Name - {name}, Salary - {salary}");
                        }
                        catch (FormatException ex)
                        {
                            Console.WriteLine(ex.Message);
                            log.Error("Error updating employee.", ex);
                        }
                        break;
                    case 3:
                        Console.WriteLine("Enter ID of employee to be deleted: ");
                        while (!int.TryParse(Console.ReadLine(), out id))
                        {
                            Console.WriteLine("Invalid input. Please enter a valid integer for employee ID.");
                        }

                        new Employee().DeleteEmployee(id);
                        Console.WriteLine("Employee deleted successfully.");
                        log.Info($"Employee deleted: ID - {id}");
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
                                log.Info($"Employee retrieved: ID - {emp.EmployeeID}, Name - {emp.EmployeeName}, Salary - {emp.EmployeeSalary}");
                            }
                            else
                            {
                                Console.WriteLine("Employee not found.");
                                log.Warn($"Employee not found for ID: {id}");
                            }
                        }
                        catch (NullReferenceException ex)
                        {
                            Console.WriteLine(ex.Message);
                            log.Error("Error retrieving employee.", ex);
                        }
                        break;
                    case 5:
                        try
                        {
                            List<Employee> employees = new Employee().GetAllEmployees();
                            foreach (Employee emp in employees)
                            {
                                Console.WriteLine(emp.EmployeeID + "," + emp.EmployeeName + "," + emp.EmployeeSalary);
                                log.Info($"Employee retrieved: ID - {emp.EmployeeID}, Name - {emp.EmployeeName}, Salary - {emp.EmployeeSalary}");
                            }
                        }
                        catch (NullReferenceException ex)
                        {
                            Console.WriteLine(ex.Message);
                            log.Error("Error retrieving employees.", ex);
                        }
                        break;
                    case 6:
                        try
                        {
                            AccessEmployees.ReadEmployeeData(employeedetailsfile);
                            Console.WriteLine("Employee data read successfully.");
                            log.Info("Employee data read successfully.");
                        }
                        catch (FileNotFoundException ex)
                        {
                            Console.WriteLine($"{ex.Message}");
                            log.Error("Error reading employee data.", ex);
                        }
                        catch (ConfigurationErrorsException ex)
                        {
                            Console.WriteLine($"{ex.Message}");
                            log.Error("Error reading employee data.", ex);
                        }
                        break;
                    case 7:
                        try
                        {
                            List<Employee> employees = new Employee().GetAllEmployees();
                            SavingEmployees.SaveEmployeeData(employees, employeedetailsfile);
                            Console.WriteLine("Employee data saved successfully.");
                            log.Info("Employee data saved successfully.");
                        }
                        catch (ConfigurationErrorsException ex)
                        {
                            Console.WriteLine($"{ex.Message}");
                            log.Error("Error saving employee data.", ex);
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
