using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MemeApp.Model;
using System.Data;
namespace DataAccessLayer
{
    internal class DapperRepository<T> : IRepository<T> where T : class, IDomainObject
    {
        private readonly IDbConnection _connection;

        public DapperRepository(IDbConnection connection)
        {
            _connection = connection;
        }
        public void Add(T entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> Readall()
        {
            throw new NotImplementedException();
        }

        public T ReadById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
