using AdminAPI;
using System.Linq;

namespace SchoolAdmin
{

    public enum EmployeeType
    {
        Teacher,
        HeadOfDeparment,
        DeputyHeadMaster,
        HeadMaster
    }

    class Program
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


    public class Teacher : EmployeeBase
    {
        public override decimal Salary { get => base.Salary + (base.Salary * 0.05m); }
    }

    public class HeadOfDeparment : EmployeeBase
    {
        public override decimal Salary { get => base.Salary + (base.Salary * 0.13m); }
    }

    public class DeputyHeadMaster : EmployeeBase
    {
        public override decimal Salary { get => base.Salary + (base.Salary * 0.75m); }
    }

    public class HeadMaster : EmployeeBase
    {
        public override decimal Salary { get => base.Salary * 5; }
    }

    public static class EmployeeFactory
    {
        public static IEmployee GetEmployeeInstance(EmployeeType employeeType, int id, string firstName, string lastName, decimal salary)
        {
            IEmployee employee = null;

            switch (employeeType)
            {
                case EmployeeType.Teacher:
                    employee = FactoryPattern<IEmployee, Teacher>.GetInstance();
                    break;

                case EmployeeType.HeadOfDeparment:
                    employee = FactoryPattern<IEmployee, HeadOfDeparment>.GetInstance();
                    break;

                case EmployeeType.DeputyHeadMaster:
                    employee = FactoryPattern<IEmployee, DeputyHeadMaster>.GetInstance();
                    break;

                case EmployeeType.HeadMaster:
                    employee = FactoryPattern<IEmployee, HeadMaster>.GetInstance();
                    break;
                default:
                    break;
            }

            if (employee != null)
            {
                employee.Id = id;
                employee.FirstName = firstName;
                employee.LastName = lastName;
                employee.Salary = salary;
            }
            else
            {
                throw new NullReferenceException();
            }

            return employee;
        }
    }
}
