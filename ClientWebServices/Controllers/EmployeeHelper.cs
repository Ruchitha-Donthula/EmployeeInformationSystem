using ClientWebServices.Models;
using EmployeeLibrary;

namespace ClientWebServices.Controllers
{
    public class EmployeeHelper
    {
        public static Models.Employee GetEmployeeViewModelFromModel(EmployeeLibrary.Employee employee)
        {
            Models.Employee viewModel = new Models.Employee
            {
                // Map properties from employee model to view model
                EmployeeID = employee.EmployeeID,
                EmployeeName = employee.EmployeeName,
                EmployeeSalary = employee.EmployeeSalary
            };
            return viewModel;
        }

        public static EmployeeLibrary.Employee GetEmployeeModelFromViewModel(Models.Employee viewModel)
        {
            EmployeeLibrary.Employee employee = new EmployeeLibrary.Employee
            {
                // Map properties from view model to employee model
                EmployeeID = viewModel.EmployeeID,
                EmployeeName = viewModel.EmployeeName,
                EmployeeSalary = viewModel.EmployeeSalary
            };
            return employee;
        }
    }
}
