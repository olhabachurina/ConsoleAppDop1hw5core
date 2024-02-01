using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using ConsoleAppDop1hw5core.Models;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore.Design;
namespace ConsoleAppDop1hw5core;

 class Program
{
    

    static void Main()
    {
        using (var db = new ApplicationContext())
        {

            // Добавляем компании
            var company1 = new Company { Name = "Meridian" };
            var company2 = new Company { Name = "Oreon" };
            var company3 = new Company { Name = "Virtual" };

            db.Companies.AddRange(company1, company2, company3);
            db.SaveChanges();

            // Добавляем сотрудников
            var employee1 = new Employee { Name = "Gorenko", ProjectEmployees = new List<ProjectEmployee>() };
            var employee2 = new Employee { Name = "Nariev", ProjectEmployees = new List<ProjectEmployee>() };
            var employee3 = new Employee { Name = "Otanenko", ProjectEmployees = new List<ProjectEmployee>() };

            db.Employees.AddRange(employee1, employee2, employee3);
            db.SaveChanges();

            // Добавляем проекты
            var project1 = new Project { Name = "Project X" };
            var project2 = new Project { Name = "Project Y" };
            var project3 = new Project { Name = "Project Z" };

            db.Projects.AddRange(project1, project2, project3);
            db.SaveChanges();

            //// Устанавливаем связи между компаниями, сотрудниками и проектами
            employee1.ProjectEmployees.Add(new ProjectEmployee { Project = project1 });
            employee2.ProjectEmployees.Add(new ProjectEmployee { Project = project1 });
            employee2.ProjectEmployees.Add(new ProjectEmployee { Project = project2 });
            employee3.ProjectEmployees.Add(new ProjectEmployee { Project = project2 });
            employee3.ProjectEmployees.Add(new ProjectEmployee { Project = project3 });

            company1.Employees.Add(employee1);
            company2.Employees.Add(employee2);
            company3.Employees.Add(employee3);

            db.SaveChanges();

            // Получение списка проектов, в которых участвуют сотрудники из компании "Oreon"
            var companyIdToQuery = 13;

            var projectsForCompany = db.Companies
                .Where(c => c.CompanyId == companyIdToQuery)
                .SelectMany(c => c.Employees)
                .SelectMany(e => e.ProjectEmployees)
                .Select(pe => pe.Project)
                .Distinct()
                .ToList();

            Debug.WriteLine("Проекты, в которых участвуют сотрудники из компании \"Oreon\":");
            foreach (var project in projectsForCompany)
            {
                Debug.WriteLine($" - {project.Name}");
            }
            
        }
    }
}
public class ApplicationContext : DbContext
{
public DbSet<Company> Companies { get; set; }
public DbSet<Employee> Employees { get; set; }
public DbSet<Project> Projects { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-4PCU5RA\\SQLEXPRESS;Database=ProjectEmpl;Trusted_Connection=True;TrustServerCertificate=True;");
            optionsBuilder.LogTo(e => Debug.WriteLine(e), new[] { RelationalEventId.CommandExecuted });
        }
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProjectEmployee>()
            .HasKey(pe => new { pe.ProjectId, pe.EmployeeId });
    }
}