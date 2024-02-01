using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppDop1hw5core.Models
{
    public class Project
    {
        public int ProjectId { get; set; }
        public string Name { get; set; }

        public List<ProjectEmployee> ProjectEmployees { get; set; }
    }
}
