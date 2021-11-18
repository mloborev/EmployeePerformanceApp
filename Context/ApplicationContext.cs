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


            //progParameters
            Parameter parameter1 = new Parameter { Id = 1, Name = "Team communication", Coefficient = 0.94, DepartmentId = programmersDepartment.Id};
            Parameter parameter2 = new Parameter { Id = 2, Name = "Corporate communication", Coefficient = 0.3, DepartmentId = programmersDepartment.Id };
            Parameter parameter3 = new Parameter { Id = 3, Name = "English language level", Coefficient = 0.92, DepartmentId = programmersDepartment.Id };
            Parameter parameter4 = new Parameter { Id = 4, Name = "3rd party communication", Coefficient = 0.21, DepartmentId = programmersDepartment.Id };
            Parameter parameter5 = new Parameter { Id = 5, Name = "Work quality", Coefficient = 0.36, DepartmentId = programmersDepartment.Id };
            Parameter parameter6 = new Parameter { Id = 6, Name = "Skills level", Coefficient = 0.91, DepartmentId = programmersDepartment.Id };
            Parameter parameter7 = new Parameter { Id = 7, Name = "Work speed", Coefficient = 0.82, DepartmentId = programmersDepartment.Id };
            Parameter parameter8 = new Parameter { Id = 8, Name = "Responsibility level", Coefficient = 0.59, DepartmentId = programmersDepartment.Id };
            Parameter parameter9 = new Parameter { Id = 9, Name = "Level of independence", Coefficient = 0.66, DepartmentId = programmersDepartment.Id };
            Parameter parameter10 = new Parameter { Id = 10, Name = "Leadership skills", Coefficient = 0.71, DepartmentId = programmersDepartment.Id };
            Parameter parameter11 = new Parameter { Id = 11, Name = "BUM evaluation", Coefficient = 0.88, DepartmentId = programmersDepartment.Id };
            Parameter parameter12 = new Parameter { Id = 12, Name = "Involment level", Coefficient = 0.93, DepartmentId = programmersDepartment.Id };


            //lawParameters
            Parameter parameter13 = new Parameter { Id = 13, Name = "Team communication", Coefficient = 0.43, DepartmentId = lawyersDepartment.Id };
            Parameter parameter14 = new Parameter { Id = 14, Name = "Corporate communication", Coefficient = 0.09, DepartmentId = lawyersDepartment.Id };
            Parameter parameter15 = new Parameter { Id = 15, Name = "English language level", Coefficient = 0.80, DepartmentId = lawyersDepartment.Id };
            Parameter parameter16 = new Parameter { Id = 16, Name = "3rd party communication", Coefficient = 0.62, DepartmentId = lawyersDepartment.Id };
            Parameter parameter17 = new Parameter { Id = 17, Name = "Work quality", Coefficient = 0.59, DepartmentId = lawyersDepartment.Id };
            Parameter parameter18 = new Parameter { Id = 18, Name = "Skills level", Coefficient = 0.85, DepartmentId = lawyersDepartment.Id };
            Parameter parameter19 = new Parameter { Id = 19, Name = "Work speed", Coefficient = 0.76, DepartmentId = lawyersDepartment.Id };
            Parameter parameter20 = new Parameter { Id = 20, Name = "Responsibility level", Coefficient = 0.21, DepartmentId = lawyersDepartment.Id };
            Parameter parameter21 = new Parameter { Id = 21, Name = "Level of independence", Coefficient = 0.84, DepartmentId = lawyersDepartment.Id };
            Parameter parameter22 = new Parameter { Id = 22, Name = "Leadership skills", Coefficient = 0.37, DepartmentId = lawyersDepartment.Id };
            Parameter parameter23 = new Parameter { Id = 23, Name = "BUM evaluation", Coefficient = 0.03, DepartmentId = lawyersDepartment.Id };
            Parameter parameter24 = new Parameter { Id = 24, Name = "Involment level", Coefficient = 0.15, DepartmentId = lawyersDepartment.Id };


            //testParameters
            Parameter parameter25 = new Parameter { Id = 25, Name = "Team communication", Coefficient = 0.64, DepartmentId = testersDepartment.Id };
            Parameter parameter26 = new Parameter { Id = 26, Name = "Corporate communication", Coefficient = 0.87, DepartmentId = testersDepartment.Id };
            Parameter parameter27 = new Parameter { Id = 27, Name = "English language level", Coefficient = 0.97, DepartmentId = testersDepartment.Id };
            Parameter parameter28 = new Parameter { Id = 28, Name = "3rd party communication", Coefficient = 0.25, DepartmentId = testersDepartment.Id };
            Parameter parameter29 = new Parameter { Id = 29, Name = "Work quality", Coefficient = 0.12, DepartmentId = testersDepartment.Id };
            Parameter parameter30 = new Parameter { Id = 30, Name = "Skills level", Coefficient = 0.84, DepartmentId = testersDepartment.Id };
            Parameter parameter31 = new Parameter { Id = 31, Name = "Work speed", Coefficient = 0.35, DepartmentId = testersDepartment.Id };
            Parameter parameter32 = new Parameter { Id = 32, Name = "Responsibility level", Coefficient = 0.22, DepartmentId = testersDepartment.Id };
            Parameter parameter33 = new Parameter { Id = 33, Name = "Level of independence", Coefficient = 0.52, DepartmentId = testersDepartment.Id };
            Parameter parameter34 = new Parameter { Id = 34, Name = "Leadership skills", Coefficient = 0.45, DepartmentId = testersDepartment.Id };
            Parameter parameter35 = new Parameter { Id = 35, Name = "BUM evaluation", Coefficient = 0.71, DepartmentId = testersDepartment.Id };
            Parameter parameter36 = new Parameter { Id = 36, Name = "Involment level", Coefficient = 0.77, DepartmentId = testersDepartment.Id };

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
            modelBuilder.Entity<Parameter>().HasData(new Parameter[] { parameter1, parameter2, parameter3, parameter4, parameter5, parameter6, parameter7, parameter8, parameter9, parameter10, parameter11, parameter12, parameter13, parameter14, parameter15, parameter16, parameter17, parameter18, parameter19, parameter20, parameter21, parameter22, parameter23, parameter24, parameter25, parameter26, parameter27, parameter28, parameter29, parameter30, parameter31, parameter32, parameter33, parameter34, parameter35, parameter36});
            //modelBuilder.Entity<Workplace>().HasOne(x => x.Reservation).WithOne(x => x.Workplace);

            base.OnModelCreating(modelBuilder);
        }
    }
}
