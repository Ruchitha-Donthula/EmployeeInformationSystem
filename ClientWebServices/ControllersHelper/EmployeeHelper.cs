using ClientWebServices.Models;
using EmployeeObjects;

namespace ClientWebServices.Controllers
{
    public class EmployeeHelper
    {
        public static ViewModelEmployee GetEmployeeViewModelFromModel(EmployeeObjects.Employee employee)
        {
            ViewModelEmployee viewModel = new ViewModelEmployee
            {
                // Map properties from employee model to view model
                EmployeeID = employee.EmployeeID,
                EmployeeName = employee.EmployeeName,
                EmployeeSalary = employee.EmployeeSalary
            };
            return viewModel;
        }

        public static EmployeeObjects.Employee GetEmployeeModelFromViewModel(ViewModelEmployee Employee)
        {
            EmployeeObjects.Employee employee = new EmployeeObjects.Employee
            {
                // Map properties from view model to employee model
                EmployeeID = Employee.EmployeeID,
                EmployeeName = Employee.EmployeeName,
                EmployeeSalary = Employee.EmployeeSalary
            };
            return employee;
        }
    }
}
