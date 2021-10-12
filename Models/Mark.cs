using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeePerformanceApp.Models
{
    public class Mark
    {
        public int Id { get; set; }
        public int ParameterId { get; set; }
        public int MarkValue { get; set; }
        public string MarkDescription { get; set; }
        public int UserId { get; set; }
        public int AssessorId { get; set; }
        public DateTime AssesmentDate { get; set; }
        public bool IsActual { get; set; }
    }
}
