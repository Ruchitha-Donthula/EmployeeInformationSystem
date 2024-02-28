using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeLibrary;

namespace EmployeeDataAccess
{
    public class AccessEmployeeData
    {
        public static void ReadEmployeeData(string filepath)
        {
            using (StreamReader reader = new StreamReader(filepath))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    string[] parts = line.Split(',');

                    int id = int.Parse(parts[0]);
                    string name = parts[1];
                    int salary = int.Parse(parts[2]);

                    Employee.employees.Add(new EmployeeLibrary.Employee
                    {
                        EmployeeID = id,
                        EmployeeName = name,
                        EmployeeSalary = salary,
                    });
                }
            }
          
        }
    }
}
