﻿using EmployeePerformanceApp.Context;
using EmployeePerformanceApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeePerformanceApp.Repositories
{
    public class ParameterRepository : IParameterRepository
    {
        private ApplicationContext db;
        public ParameterRepository(ApplicationContext context)
        {
            db = context;
        }

        public async Task<List<Parameter>> GetAllData()
        {
            return await db.Parameters.ToListAsync();
        }

        public async Task<Parameter> GetParameterById(int id)
        {
            return await db.Parameters.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task AddParameter(Parameter parameter)
        {
            db.Parameters.Add(parameter);
            await db.SaveChangesAsync();
        }

        public async Task DeleteParameter(int id)
        {
            Parameter parameter = await GetParameterById(id);
            db.Remove(parameter);
            await db.SaveChangesAsync();
        }
    }
}