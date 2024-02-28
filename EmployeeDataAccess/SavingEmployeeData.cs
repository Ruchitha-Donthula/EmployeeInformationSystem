using System.Collections.Generic;
using System.IO;

namespace EmployeeDataAccess
{
    public class SavingEmployeeData
    {
        public static void SaveEmployeeData(List<EmployeeLibrary.Employee> employees, string filepath)
        {
            using (StreamWriter writer = new StreamWriter(filepath))
            {
                foreach (EmployeeLibrary.Employee emp in employees)
                {
                    writer.WriteLine($"{emp.EmployeeID},{emp.EmployeeName},{emp.EmployeeSalary}");
                }
            }
        }

    }
}
