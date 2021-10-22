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

            User adminUser = new User { Id = 1, Name = "Admin", Surname = "Adminov", Login = "admin", Password = "123", RoleId = adminRole.Id, StatusId = workingStatus.Id, DepartmentId = adminDepartment.Id};
            User user1User = new User { Id = 2, Name = "User1", Surname = "Userov", Login = "user1", Password = "123", RoleId = employeeRole.Id, StatusId = workingStatus.Id, DepartmentId = programmersDepartment.Id};
            User user2User = new User { Id = 3, Name = "User2", Surname = "Userov", Login = "user2", Password = "123", RoleId = employeeRole.Id, StatusId = workingStatus.Id, DepartmentId = programmersDepartment.Id };
            User user3User = new User { Id = 4, Name = "User3", Surname = "Userov", Login = "user3", Password = "123", RoleId = employeeRole.Id, StatusId = workingStatus.Id, DepartmentId = programmersDepartment.Id };
            User leadUser = new User { Id = 5, Name = "Lead", Surname = "Leadov", Login = "lead", Password = "123", RoleId = leadRole.Id, StatusId = workingStatus.Id, DepartmentId = programmersDepartment.Id};
            User chiefUser = new User { Id = 6, Name = "Chief", Surname = "Chiefov", Login = "chief", Password = "123", RoleId = chiefRole.Id, StatusId = workingStatus.Id , DepartmentId = programmersDepartment.Id};

            modelBuilder.Entity<Selection>(x => {
                x.Navigation(s => s.Parameters).AutoInclude();
            });

            modelBuilder.Entity<Role>().HasData(new Role[] { adminRole, chiefRole, leadRole, employeeRole });
            modelBuilder.Entity<Status>().HasData(new Status[] { workingStatus, firedStatus });
            modelBuilder.Entity<Department>().HasData(new Department[] { adminDepartment, programmersDepartment });
            modelBuilder.Entity<User>().HasData(new User[] { adminUser, user1User, user2User, user3User, leadUser, chiefUser });
            //modelBuilder.Entity<Workplace>().HasOne(x => x.Reservation).WithOne(x => x.Workplace);

            base.OnModelCreating(modelBuilder);
        }
    }
}
