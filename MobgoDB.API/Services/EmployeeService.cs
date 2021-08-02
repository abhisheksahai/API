using MobgoDB.API.Models;
using MongoDB.Bson;
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

        public void Add(Employee employee)
        {
            if (employee == null)
            {
                throw new ArgumentNullException(nameof(Employee));
            }
            _employees.InsertOne(employee);
        }

        public void Delete(string id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }
            var result = _employees.DeleteOne(emp => emp.Id == id);
        }

        public void Update(Employee employee)
        {
            if (employee == null)
            {
                throw new ArgumentNullException(nameof(Employee));
            }
            var update = Builders<Employee>.Update.Set("Name", employee.Name);
            _employees.UpdateOne(emp => emp.Id == employee.Id, update);
        }

    }
}
