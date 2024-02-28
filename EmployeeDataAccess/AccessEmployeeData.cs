using log4net;
using System;
using System.IO;

namespace EmployeeDataAccess
{
    public class AccessEmployeeData
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(AccessEmployeeData));

        public static void ReadEmployeeData(string filepath)
        {
            try
            {
                if (File.Exists(filepath))
                {
                    using (StreamReader reader = new StreamReader(filepath))
                    {
                        while (!reader.EndOfStream)
                        {
                            string line = reader.ReadLine();
                            string[] parts = line.Split(',');

                            if (parts.Length == 3)
                            {
                                if (int.TryParse(parts[0], out int id) && int.TryParse(parts[2], out int salary))
                                {
                                    string name = parts[1];
                                    EmployeeLibrary.Employee.employees.Add(new EmployeeLibrary.Employee
                                    {
                                        EmployeeID = id,
                                        EmployeeName = name,
                                        EmployeeSalary = salary,
                                    });
                                }
                                else
                                {
                                    Log.Error($"Invalid data format in line: {line}");
                                }
                            }
                            else
                            {
                                Log.Error($"Invalid data format in line: {line}");
                            }
                        }
                    }
                }
                else
                {
                    Log.Error($"File not found at path: {filepath}");
                }
            }
            catch (Exception ex)
            {
                Log.Error($"An error occurred while reading data: {ex.Message}", ex);
                // Rethrow the exception to ensure it's handled by the ExceptionHandlingFilter
                throw;
            }
        }
    }
}
