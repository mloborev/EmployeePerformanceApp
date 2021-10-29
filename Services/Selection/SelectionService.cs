﻿using EmployeePerformanceApp.Models;
using EmployeePerformanceApp.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeePerformanceApp.Services
{
    public class SelectionService : ISelectionService
    {
        private readonly ISelectionRepository _selectionRepository;
        private readonly IParameterRepository _parameterRepository;
        public SelectionService(ISelectionRepository selectionRepository, IParameterRepository parameterRepository)
        {
            _selectionRepository = selectionRepository;
            _parameterRepository = parameterRepository;
        }

        public async Task AddSelection(int departmentId, string selectionName, int[] arr)
        {
            List<Parameter> parametersArray = new List<Parameter>(await _parameterRepository.GetParametersByIds(arr));

            Selection selection = new Selection { DepartmentId = departmentId, Name = selectionName, Parameters = parametersArray};
            //await _parameterService.SetParametersInUse(parametersArray);
            await _selectionRepository.AddSelection(selection);
        }
    }
}
