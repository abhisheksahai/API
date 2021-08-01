using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobgoDB.API.Models
{
    public class EmployeeDatabaseSettings : IEmployeeDatabaseSettings
    {
        public string EmployeesCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
