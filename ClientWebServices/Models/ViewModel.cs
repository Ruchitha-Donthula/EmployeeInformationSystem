using System.ComponentModel.DataAnnotations;

namespace ClientWebServices.Models
{
    public class ViewModelEmployee
    {
        [Required(ErrorMessage = "Employee ID is required.")]
        public int EmployeeID { get; set; }

        [Required(ErrorMessage = "Employee name is required.")]
        public string EmployeeName { get; set; }

        [Required(ErrorMessage = "Employee salary is required.")]
        public int EmployeeSalary { get; set; }
    }
}
