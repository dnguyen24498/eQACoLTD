using System.Collections.Generic;

namespace eQACoLTD.Data.Entities
{
    public class Department
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public List<Employee> Employees { get; set; }    
    }
}