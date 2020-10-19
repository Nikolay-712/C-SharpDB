using Microsoft.EntityFrameworkCore;
using SoftUni.Data;
using SoftUni.Models;
using System;
using System.Linq;
using System.Text;

namespace SoftUni
{
    public class StartUp
    {

        static void Main(string[] args)
        {
            var softUniContext = new SoftUniContext();

            //03. Employees Full Information 
            // Console.WriteLine(GetEmployeesFullInformation(softUniContext));

            //04. Employees with Salary Over 50 000
            // Console.WriteLine(GetEmployeesWithSalaryOver50000(softUniContext));

            //05. Employees from Research and Development 
            //Console.WriteLine(GetEmployeesFromResearchAndDevelopment(softUniContext));

            //06. Adding a New Address and Updating Employee 
            //Console.WriteLine(AddNewAddressToEmployee(softUniContext));

            //07. Employees and Projects
            //Console.WriteLine(GetEmployeesInPeriod(softUniContext));

            //08. Addresses by Town 
            //Console.WriteLine(GetAddressesByTown(softUniContext));

            //09. Employee 147
            //Console.WriteLine(GetEmployee147(softUniContext));

            //10. Departments with More Than 5 Employees 
            // Console.WriteLine(GetDepartmentsWithMoreThan5Employees(softUniContext));

            //11. Find Latest 10 Projects
            //Console.WriteLine(GetLatestProjects(softUniContext));

            //12. Increase Salaries 
            //Console.WriteLine(IncreaseSalaries(softUniContext));

            //13. Find Employees by First Name Starting With Sa 
            //Console.WriteLine(GetEmployeesByFirstNameStartingWithSa(softUniContext));

            //14. Delete Project by Id 
            //Console.WriteLine(DeleteProjectById(softUniContext));

            //15. Remove Town 
            Console.WriteLine(RemoveTown(softUniContext));
        }

        public static string GetEmployeesFullInformation(SoftUniContext context)
        {
            StringBuilder builder = new StringBuilder();

            var result = context.Employees.OrderBy(x => x.EmployeeId)
                .Select(e => new { e.FirstName, e.LastName, e.MiddleName, e.JobTitle, e.Salary }).ToList();

            foreach (var emp in result)
            {
                builder.AppendLine($"{emp.FirstName} {emp.LastName} {emp.MiddleName} {emp.JobTitle} {emp.Salary:F2}");
            }

            return builder.ToString().TrimEnd();
        }

        public static string GetEmployeesWithSalaryOver50000(SoftUniContext context)
        {
            StringBuilder builder = new StringBuilder();

            var result = context.Employees
                .Where(e => e.Salary > 50000)
                .Select(e => new { e.FirstName, e.Salary })
                .OrderBy(e => e.FirstName).ToList();



            foreach (var emp in result)
            {
                builder.AppendLine($"{emp.FirstName} - {emp.Salary:F2}");
            }

            return builder.ToString().TrimEnd();
        }

        public static string GetEmployeesFromResearchAndDevelopment(SoftUniContext context)
        {
            StringBuilder builder = new StringBuilder();

            var result = context.Employees
                 .Where(e => e.Department.Name == "Research and Development")
                 .Select(e => new { e.FirstName, e.LastName, e.Department.Name, e.Salary })
                 .OrderBy(e => e.Salary).ThenByDescending(e => e.FirstName).ToList();


            foreach (var emp in result)
            {
                builder.AppendLine($"{emp.FirstName} {emp.LastName} from {emp.Name} - ${emp.Salary:F2}");
            }

            return builder.ToString().TrimEnd();
        }

        public static string AddNewAddressToEmployee(SoftUniContext context)
        {
            StringBuilder builder = new StringBuilder();

            Address address = new Address { AddressText = "Vitoshka 15", TownId = 4 };
            var employee = context.Employees.Where(e => e.LastName == "Nakov").FirstOrDefault();

            employee.Address = address;
            context.SaveChanges();


            var result = context.Employees
                .OrderByDescending(e => e.AddressId)
                .Take(10)
                .Select(e => e.Address.AddressText)
                .ToList();

            foreach (var txt in result)
            {
                builder.AppendLine(txt);
            }

            return builder.ToString().TrimEnd();
        }

        public static string GetEmployeesInPeriod(SoftUniContext context)
        {
            StringBuilder builder = new StringBuilder();

            var result = context.Employees
                .Select(
                    e => new
                    {
                        e.FirstName,
                        e.LastName,
                        managerFirstName = e.Manager.FirstName,
                        managerLastName = e.Manager.LastName,
                        projects = e.EmployeesProjects
                        .Select(x => x.Project)
                        .TakeWhile(x => x.StartDate.Year >= 2001 && x.StartDate.Year <= 2003)
                    }).ToList().Take(10);



            foreach (var employee in result)
            {
                builder.AppendLine($"{employee.FirstName} {employee.LastName} - " +
                    $"Manager: {employee.managerFirstName} {employee.managerLastName}");

                foreach (var project in employee.projects)
                {


                    builder.AppendLine($"--{project.Name} - " +
                        $"{project.StartDate.ToString("M/d/yyyy h:mm:ss tt")} AM - " +
                        $"{project.EndDate} AM");
                }
            }


            return builder.ToString().TrimEnd();
        }

        public static string GetAddressesByTown(SoftUniContext context)
        {
            StringBuilder builder = new StringBuilder();

            var result = context.Addresses
                .Select(a => new { a.AddressText, a.Town.Name, a.Employees.Count })
                .OrderByDescending(e => e.Count)
               .ThenBy(a => a.Name)
               .ThenBy(a => a.AddressText)
               .ToArray();


            foreach (var addr in result)
            {
                builder.AppendLine($"{addr.AddressText}, {addr.Name} - {addr.Count} employees");
            }

            return builder.ToString().TrimEnd();
        }

        public static string GetEmployee147(SoftUniContext context)
        {
            StringBuilder builder = new StringBuilder();

            var result = context.Employees.Where(e => e.EmployeeId == 147)
                .Select(e => new { e.FirstName, e.LastName, e.JobTitle, Projects = e.EmployeesProjects.Select(x => x.Project).OrderBy(x => x.Name) }).ToList();


            foreach (var employee in result)
            {
                builder.AppendLine($"{employee.FirstName} {employee.LastName} - {employee.JobTitle}");

                var employeeProjects = employee.Projects;

                foreach (var project in employeeProjects)
                {
                    builder.AppendLine($"{project.Name}");
                }

            }

            return builder.ToString().TrimEnd();
        }

        public static string GetDepartmentsWithMoreThan5Employees(SoftUniContext context)
        {
            StringBuilder builder = new StringBuilder();

            var result = context.Departments
                .Where(d => d.Employees.Count > 5)
                .OrderBy(d => d.Employees.Count)
                .ThenBy(d => d.Name)
                .Select(d => new { d.Name, d.Manager.FirstName, d.Manager.LastName, d.Employees }).ToList();

            foreach (var department in result)
            {
                builder.AppendLine($"{department.Name} - {department.FirstName} {department.LastName}");

                foreach (var employee in department.Employees)
                {
                    builder.AppendLine($"{employee.FirstName} {employee.LastName} - {employee.JobTitle}");
                }
            }

            return builder.ToString().TrimEnd();
        }

        public static string GetLatestProjects(SoftUniContext context)
        {
            StringBuilder builder = new StringBuilder();

            var result = context.Projects.OrderByDescending(p => p.StartDate).Take(10).Select(s => new
            {
                ProjectName = s.Name,
                ProjectDescription = s.Description,
                ProjectStartDate = s.StartDate
            })
                .OrderBy(n => n.ProjectName).ToList();

            foreach (var project in result)
            {
                builder.AppendLine(project.ProjectName);
                builder.AppendLine(project.ProjectDescription);
                builder.AppendLine($"{project.ProjectStartDate.ToString("M/d/yyyy h:mm:ss tt")}");
            }

            return builder.ToString().TrimEnd();
        }

        public static string IncreaseSalaries(SoftUniContext context)
        {
            StringBuilder builder = new StringBuilder();

            var result = context.Employees
                .Where(d => d.Department.Name == "Engineering" || d.Department.Name == "Tool Design" ||
                d.Department.Name == "Marketing" || d.Department.Name == "Information Services")
                .OrderBy(x => x.FirstName).ThenBy(x => x.LastName).ToList();


            foreach (var employee in result)
            {
                var rate = employee.Salary * 12 / 100;
                var increaseSalary = employee.Salary + rate;

                builder.AppendLine(($"{employee.FirstName} {employee.LastName} (${increaseSalary:F2})"));

                employee.Salary = increaseSalary;
                context.SaveChanges();

            }
            return builder.ToString().TrimEnd();
        }

        public static string GetEmployeesByFirstNameStartingWithSa(SoftUniContext context)
        {
            StringBuilder builder = new StringBuilder();

            var result = context.Employees
                .Select(e => new { e.FirstName, e.LastName, e.JobTitle, e.Salary })
                .OrderBy(e => e.FirstName).ThenBy(e => e.LastName)
                .Where(e => e.FirstName.StartsWith("Sa")).ToList();


            foreach (var employee in result)
            {
                builder.AppendLine($"{employee.FirstName} {employee.LastName} - {employee.JobTitle} - (${employee.Salary:F2})");
            }

            return builder.ToString().TrimEnd();
        }

        public static string DeleteProjectById(SoftUniContext context)
        {
            StringBuilder builder = new StringBuilder();

            var targetProject = context.Projects
                .Where(p => p.ProjectId == 2)
                .FirstOrDefault();

            var employeesProjects = context.EmployeesProjects
                .Where(x => x.ProjectId == 2)
                .ToList();

            if (employeesProjects != null)
            {
                context.EmployeesProjects.RemoveRange(employeesProjects);

                context.SaveChanges();
            }

            if (targetProject != null)
            {
                context.Projects.Remove(targetProject);

                context.SaveChanges();
            }


            var result = context.Projects.Take(10).ToList();

            foreach (var project in result)
            {
                builder.AppendLine(project.Name);
            }


            return builder.ToString().TrimEnd();
        }

        public static string RemoveTown(SoftUniContext context)
        {
            var targetTown = context.Towns
                .Where(x => x.Name == "Seattle")
                .FirstOrDefault();

            var addresses = context.Addresses
                .Where(x => x.Town.Name == "Seattle")
                .ToList();

            var employees = context.Employees.
                Where(x => x.Address.Town.Name == "Seattle")
                .ToArray();



            foreach (var item in employees)
            {
                item.AddressId = null;
                context.SaveChanges();
            }


            var projects = context.EmployeesProjects
                .Where(x => x.Employee.Address.Town.Name == "Seattle")
                .ToList();

            if (projects != null)
            {
                context.EmployeesProjects.RemoveRange(projects);
                context.SaveChanges();
            }


            if (employees != null)
            {
                context.Employees.RemoveRange(employees);
                context.SaveChanges();
            }

            if (addresses != null)
            {
                context.RemoveRange(addresses);
                context.SaveChanges();
            }

            if (targetTown != null)
            {
                context.Remove(targetTown);
                context.SaveChanges();
            }

            return $"{addresses.Count} addresses in Seattle were deleted";
        }
    }
}

