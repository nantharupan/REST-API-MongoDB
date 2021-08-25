using MongoDB.Driver;
using RestAPI_MongoDB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestAPI_MongoDB.Services
{
    public class EmployeeService
    {
        private readonly IMongoCollection<Employee> _books;

        public EmployeeService(ICompanyDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _books = database.GetCollection<Employee>(settings.EmployeeCollectionName);
        }

        public List<Employee> Get() =>
            _books.Find(book => true).ToList();

        public Employee Get(string id) =>
            _books.Find<Employee>(book => book.Id == id).FirstOrDefault();

        public Employee Create(Employee book)
        {
            _books.InsertOne(book);
            return book;
        }

        public void Update(string id, Employee bookIn) =>
            _books.ReplaceOne(book => book.Id == id, bookIn);

        public void Remove(Employee bookIn) =>
            _books.DeleteOne(book => book.Id == bookIn.Id);

        public void Remove(string id) =>
            _books.DeleteOne(book => book.Id == id);
    }
}
