using AdminAPI;
using SchoolAdmin.Models;

namespace SchoolAdmin;



public class Program
{

    static void Main(string[] args)
    {
            List<IEmployee> employees = new List<IEmployee>();

            SeedData(employees);

            Console.WriteLine($"Total Salaries with bonus: {employees.Sum(e => e.Salary)}");

        }


        public static void SeedData(List<IEmployee> employees)
        {
            IEmployee teacher = EmployeeFactory.GetEmployeeInstance(EmployeeType.Teacher, 1, "tt", "ss", 100);
            employees.Add(teacher);

            IEmployee teacher1 = EmployeeFactory.GetEmployeeInstance(EmployeeType.Teacher, 2, "tt1", "ss1", 100);
            employees.Add(teacher1);

            IEmployee departmentHead = EmployeeFactory.GetEmployeeInstance(EmployeeType.DeputyHeadMaster, 3, "tt11", "ss1", 1000);
            employees.Add(departmentHead);

            IEmployee depHeadMaster = EmployeeFactory.GetEmployeeInstance(EmployeeType.DeputyHeadMaster, 4, "tft11", "ssf11", 4000);
            employees.Add(depHeadMaster);

            IEmployee headMaster = EmployeeFactory.GetEmployeeInstance(EmployeeType.DeputyHeadMaster, 5, "tffft11", "ssfff11", 7000);
            employees.Add(headMaster);
        }
    }

