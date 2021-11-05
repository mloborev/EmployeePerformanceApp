using EmployeePerformanceApp.Models;
using EmployeePerformanceApp.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeePerformanceApp.Context
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Selection> Selections { get; set; }
        public DbSet<Parameter> Parameters { get; set; }
        public DbSet<Mark> Marks { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Department>().HasOptional<UserModel>(u => u.UserId).WithOptionalPrincipal();
            modelBuilder.UseCollation("SQL_Latin1_General_CP1_CS_AS");

            Role adminRole = new Role { Id = 1, Name = "Admin" };
            Role chiefRole = new Role { Id = 2, Name = "Chief" };
            Role leadRole = new Role { Id = 3, Name = "Lead" };
            Role employeeRole = new Role { Id = 4, Name = "Employee" };

            Status workingStatus = new Status { Id = 1, Name = "Working" };
            Status firedStatus = new Status { Id = 2, Name = "Fired" };

            Department adminDepartment = new Department { Id = 1, Name = "Admin", ShowPreviousMarks = true };
            Department programmersDepartment = new Department { Id = 2, Name = "Programmers", ShowPreviousMarks = true };
            Department lawyersDepartment = new Department { Id = 3, Name = "Lawyers", ShowPreviousMarks = true };
            Department testersDepartment = new Department { Id = 4, Name = "Testers", ShowPreviousMarks = true };

            //admin
            User adminUser = new User
            {
                Id = 1,
                Name = "Admin",
                Surname = "Adminov",
                Login = "admin",
                Password = "123",
                RoleId = adminRole.Id,
                StatusId = workingStatus.Id,
                DepartmentId = adminDepartment.Id
            };

            //programmers
            User progUser1 = new User 
            { 
                Id = 2, 
                Name = "ProgUser1", 
                Surname = "ProgUserov", 
                Login = "progUser1", 
                Password = "123", 
                RoleId = employeeRole.Id, 
                StatusId = workingStatus.Id, 
                DepartmentId = programmersDepartment.Id
            };
            User progUser2 = new User 
            { 
                Id = 3, 
                Name = "ProgUser2", 
                Surname = "ProgUserov", 
                Login = "progUser2", 
                Password = "123", 
                RoleId = employeeRole.Id, 
                StatusId = workingStatus.Id, 
                DepartmentId = programmersDepartment.Id 
            };
            User progUser3 = new User 
            { 
                Id = 4, 
                Name = "ProgUser3", 
                Surname = "ProgUserov", 
                Login = "progUser3", 
                Password = "123", 
                RoleId = employeeRole.Id, 
                StatusId = workingStatus.Id, 
                DepartmentId = programmersDepartment.Id
            };
            User progLead = new User
            {
                Id = 5,
                Name = "ProgLead",
                Surname = "ProgLeadov",
                Login = "progLead",
                Password = "123",
                RoleId = leadRole.Id,
                StatusId = workingStatus.Id,
                DepartmentId = programmersDepartment.Id
            };
            User progChief = new User 
            { 
                Id = 6, 
                Name = "ProgChief", 
                Surname = "ProgChiefov", 
                Login = "progChief", 
                Password = "123", 
                RoleId = chiefRole.Id, 
                StatusId = workingStatus.Id, 
                DepartmentId = programmersDepartment.Id
            };

            //lawyers
            User lawUser1 = new User
            {
                Id = 7,
                Name = "LawUser1",
                Surname = "LawUserov",
                Login = "lawUser1",
                Password = "123",
                RoleId = employeeRole.Id,
                StatusId = workingStatus.Id,
                DepartmentId = lawyersDepartment.Id
            };
            User lawUser2 = new User
            {
                Id = 8,
                Name = "LawUser2",
                Surname = "LawUserov",
                Login = "lawUser2",
                Password = "123",
                RoleId = employeeRole.Id,
                StatusId = workingStatus.Id,
                DepartmentId = lawyersDepartment.Id
            };
            User lawUser3 = new User
            {
                Id = 9,
                Name = "LawUser3",
                Surname = "LawUserov",
                Login = "lawUser3",
                Password = "123",
                RoleId = employeeRole.Id,
                StatusId = workingStatus.Id,
                DepartmentId = lawyersDepartment.Id
            };
            User lawLead = new User
            {
                Id = 10,
                Name = "LawLead",
                Surname = "LawLeadov",
                Login = "lawLead",
                Password = "123",
                RoleId = leadRole.Id,
                StatusId = workingStatus.Id,
                DepartmentId = lawyersDepartment.Id
            };
            User lawChief = new User
            {
                Id = 11,
                Name = "LawChief",
                Surname = "LawChiefov",
                Login = "lawChief",
                Password = "123",
                RoleId = chiefRole.Id,
                StatusId = workingStatus.Id,
                DepartmentId = lawyersDepartment.Id
            };

            //testers
            User testUser1 = new User
            {
                Id = 12,
                Name = "TestUser1",
                Surname = "TestUserov",
                Login = "testUser1",
                Password = "123",
                RoleId = employeeRole.Id,
                StatusId = workingStatus.Id,
                DepartmentId = testersDepartment.Id
            };
            User testUser2 = new User
            {
                Id = 13,
                Name = "TestUser2",
                Surname = "TestUserov",
                Login = "testUser2",
                Password = "123",
                RoleId = employeeRole.Id,
                StatusId = workingStatus.Id,
                DepartmentId = testersDepartment.Id
            };
            User testUser3 = new User
            {
                Id = 14,
                Name = "TestUser3",
                Surname = "TestUserov",
                Login = "testUser3",
                Password = "123",
                RoleId = employeeRole.Id,
                StatusId = workingStatus.Id,
                DepartmentId = testersDepartment.Id
            };
            User testLead = new User
            {
                Id = 15,
                Name = "TestLead",
                Surname = "TestLeadov",
                Login = "testLead",
                Password = "123",
                RoleId = leadRole.Id,
                StatusId = workingStatus.Id,
                DepartmentId = testersDepartment.Id
            };
            User testChief = new User
            {
                Id = 16,
                Name = "TestChief",
                Surname = "TestChiefov",
                Login = "testChief",
                Password = "123",
                RoleId = chiefRole.Id,
                StatusId = workingStatus.Id,
                DepartmentId = testersDepartment.Id
            };


            Parameter parameter1 = new Parameter { Id = 1, Name = "Work quality", Coefficient = 1, DepartmentId = programmersDepartment.Id};
            Parameter parameter2 = new Parameter { Id = 2, Name = "Skills level", Coefficient = 1, DepartmentId = programmersDepartment.Id };
            Parameter parameter3 = new Parameter { Id = 3, Name = "Work speed", Coefficient = 1, DepartmentId = programmersDepartment.Id };

            modelBuilder.Entity<Selection>(x => {
                x.Navigation(s => s.Parameters).AutoInclude();
            });

            modelBuilder.Entity<Role>().HasData(new Role[] { adminRole, chiefRole, leadRole, employeeRole });
            modelBuilder.Entity<Status>().HasData(new Status[] { workingStatus, firedStatus });
            modelBuilder.Entity<Department>().HasData(new Department[] { adminDepartment, programmersDepartment, lawyersDepartment, testersDepartment });
            modelBuilder.Entity<User>().HasData(new User[] 
            { 
                adminUser,
                progUser1, progUser2, progUser3, progLead, progChief,
                lawUser1, lawUser2, lawUser3, lawLead, lawChief,
                testUser1, testUser2, testUser3, testLead, testChief
            });
            //modelBuilder.Entity<Parameter>().HasData(new Parameter[] { parameter1, parameter2, parameter3, parameter4 });
            //modelBuilder.Entity<Workplace>().HasOne(x => x.Reservation).WithOne(x => x.Workplace);

            base.OnModelCreating(modelBuilder);
        }
    }
}
