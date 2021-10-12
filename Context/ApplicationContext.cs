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
        public DbSet<UserModel> Users { get; set; }
        public DbSet<RoleModel> Roles { get; set; }
        public DbSet<StatusModel> Statuses { get; set; }
        public DbSet<DepartmentModel> Departments { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Department>().HasOptional<UserModel>(u => u.UserId).WithOptionalPrincipal();

            RoleModel adminRole = new RoleModel { Id = 1, Name = "Admin" };
            RoleModel chiefRole = new RoleModel { Id = 2, Name = "Chief" };
            RoleModel leadRole = new RoleModel { Id = 3, Name = "Lead" };
            RoleModel employeeRole = new RoleModel { Id = 4, Name = "Employee" };

            StatusModel workingStatus = new StatusModel { Id = 1, Name = "Working" };
            StatusModel firedStatus = new StatusModel { Id = 2, Name = "Fired" };

            DepartmentModel programmersDepartment = new DepartmentModel { Id = 1, Name = "Programmers", ShowPreviousMarks = true };

            UserModel adminUser = new UserModel { Id = 1, Name = "Admin", Surname = "Adminov", Login = "admin", Password = "123", RoleId = adminRole.Id, StatusId = workingStatus.Id};
            UserModel user1User = new UserModel { Id = 2, Name = "User1", Surname = "Userov", Login = "user1", Password = "123", RoleId = employeeRole.Id, StatusId = workingStatus.Id, DepartmentId = programmersDepartment.Id};
            UserModel user2User = new UserModel { Id = 3, Name = "User2", Surname = "Userov", Login = "user2", Password = "123", RoleId = employeeRole.Id, StatusId = workingStatus.Id, DepartmentId = programmersDepartment.Id };
            UserModel user3User = new UserModel { Id = 4, Name = "User3", Surname = "Userov", Login = "user3", Password = "123", RoleId = employeeRole.Id, StatusId = workingStatus.Id, DepartmentId = programmersDepartment.Id };
            UserModel leadUser = new UserModel { Id = 5, Name = "Lead", Surname = "Leadov", Login = "lead", Password = "123", RoleId = leadRole.Id, StatusId = workingStatus.Id, DepartmentId = programmersDepartment.Id};
            UserModel chiefUser = new UserModel { Id = 6, Name = "Chief", Surname = "Chiefov", Login = "chief", Password = "123", RoleId = chiefRole.Id, StatusId = workingStatus.Id , DepartmentId = programmersDepartment.Id};

           

            modelBuilder.Entity<RoleModel>().HasData(new RoleModel[] { adminRole, chiefRole, leadRole, employeeRole });
            modelBuilder.Entity<StatusModel>().HasData(new StatusModel[] { workingStatus, firedStatus });
            modelBuilder.Entity<DepartmentModel>().HasData(new DepartmentModel[] { programmersDepartment });
            modelBuilder.Entity<UserModel>().HasData(new UserModel[] { adminUser, user1User, user2User, user3User, leadUser, chiefUser });
            //modelBuilder.Entity<Workplace>().HasOne(x => x.Reservation).WithOne(x => x.Workplace);

            base.OnModelCreating(modelBuilder);
        }
    }
}
