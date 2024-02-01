using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppDop1hw5core.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public string Name { get; set; }

        public List<ProjectEmployee> ProjectEmployees { get; set; } // Многие сотрудники могут участвовать во многих проектах
    }
}
