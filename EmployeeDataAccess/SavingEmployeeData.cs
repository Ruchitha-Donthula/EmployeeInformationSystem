using log4net;
using System;
using System.Collections.Generic;
using System.IO;

namespace EmployeeDataAccess
{
    public class SavingEmployeeData
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(SavingEmployeeData));

        public static void SaveEmployeeData(List<EmployeeLibrary.Employee> employees, string filepath)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(filepath))
                {
                    foreach (EmployeeLibrary.Employee emp in employees)
                    {
                        writer.WriteLine($"{emp.EmployeeID},{emp.EmployeeName},{emp.EmployeeSalary}");
                    }
                }
            }
            catch (Exception ex)
            {
               
                Log.Error("An error occurred while saving employee data.", ex);
                throw; 
            }
        }
    }
}
