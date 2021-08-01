using MobgoDB.API.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobgoDB.API.Services
{
    public class EmployeeService
    {
        private readonly IMongoCollection<Employee> _employees;
        public EmployeeService(IEmployeeDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _employees = database.GetCollection<Employee>(settings.EmployeesCollectionName);
        }

        public List<Employee> Get()
        {
            List<Employee> employees = _employees.Find(emp => true).ToList<Employee>();
            return employees;
        }

        public Employee Get(string id)
        {
            Employee employee = _employees.Find<Employee>(emp => emp.Id == id).FirstOrDefault();
            return employee;
        }

    }
}
